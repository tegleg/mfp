using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace DestroyIt
{
    /// <summary>
    /// Creates a pool of objects to pull from, instead of using expensive Instantiate/Destroy calls.
    /// This class is a Singleton, meaning it is called using ObjectPool.Instance.SomeFunction().
    /// </summary>
    public class ObjectPool : MonoBehaviour
    {
        // Hide the default constructor (use ObjectPool.Instance instead).
        private ObjectPool() { }
        
        public List<PoolEntry> prefabsToPool;
        public bool suppressWarnings;
        
        private GameObject[][] Pool;
        private Dictionary<int, GameObject> autoPooledObjects; // Destroyed Prefabs which are auto-added to the pool on start.
        private GameObject container;

        // Here is a private reference only this class can access
        private static ObjectPool _instance;

        // This is the public reference that other classes will use
        public static ObjectPool Instance
        {
            get
            {
                // If _instance hasn't been set yet, we grab it from the scene.
                // This will only happen the first time this reference is used.
                if (_instance == null)
                    _instance = FindObjectOfType<ObjectPool>();
                return _instance;
            }
        }

        void Start()
        {
            container = new GameObject("_ObjectPool");
            if (prefabsToPool == null) return;

            // AUTOPOOL: Find all Destructible objects with DestroyedPrefabs in the scene that have Auto-Pool set to TRUE.
            Destructible[] destructibleObjectsInScene = FindObjectsOfType<Destructible>();
            autoPooledObjects = new Dictionary<int, GameObject>();
            AddDestructibleObjectsToPool(destructibleObjectsInScene);

            // Instantiate game objects from the PrefabsToPool list and add them to the Pool.
            Pool = new GameObject[prefabsToPool.Count][];
            for (int i = 0; i < prefabsToPool.Count; i++)
            {
                PoolEntry poolEntry = prefabsToPool[i];
                Pool[i] = new GameObject[poolEntry.Count];
                for (int n=0; n<poolEntry.Count; n++)
                {
                    if (poolEntry.Prefab == null) continue;
                    var newObj = Instantiate(poolEntry.Prefab);
                    newObj.name = poolEntry.Prefab.name;
                    PoolObject(newObj);
                }
            }
        }

        private void AddDestructibleObjectsToPool(IEnumerable<Destructible> destObjects)
        {
            foreach (Destructible destObj in destObjects)
            {
                if (destObj.destroyedPrefab != null && destObj.autoPoolDestroyedPrefab)
                {
                    var newObj = Instantiate(destObj.destroyedPrefab);
                    newObj.transform.parent = container.transform;
                    newObj.name = destObj.destroyedPrefab.name;
                    newObj.AddTag(Tag.Pooled);
                    DestructibleHelper.TransferMaterials(destObj, newObj);

                    // See if we will need to check for clinging debris. (Optimization)
                    ClingPoint[] clingPoints = newObj.GetComponentsInChildren<ClingPoint>();
                    if (clingPoints.Length == 0)
                        destObj.checkForClingingDebris = false;

                    // Add references to Rigidbodies and Rigidbody GameObjects to Destructible objects for better performance. (Optimization)
                    destObj.pooledRigidbodies = newObj.GetComponentsInChildren<Rigidbody>();
                    destObj.pooledRigidbodyGos = new GameObject[destObj.pooledRigidbodies.Length];
                    for (int i = 0; i < destObj.pooledRigidbodies.Length; i++)
                        destObj.pooledRigidbodyGos[i] = destObj.pooledRigidbodies[i].gameObject;

                    // Tag debris colliders
                    if (destObj.tagDebrisColliders)
                    {
                        Collider[] colls = newObj.GetComponentsInChildren<Collider>();
                        for (int c = 0; c < colls.Length; c++)
                            colls[c].gameObject.AddTag(destObj.tagDebrisCollidersWith);
                    }

                    // Initialize all destructible child objects on this object.
                    List<Destructible> childDestructibleObjects = newObj.GetComponentsInChildren<Destructible>().ToList();
                    foreach(Destructible child in childDestructibleObjects)
                        child.Initialize();

                    newObj.SetActive(false);
                    autoPooledObjects.Add(destObj.GetInstanceID(), newObj);
                }
            }
        }

        // Spawn a game object from the original prefab, not from the pool. Used for resetting destroyed objects back to their original state.
        public GameObject SpawnFromOriginal(string prefabName)
        {
            foreach (PoolEntry entry in prefabsToPool)
            {
                if (entry.Prefab != null && entry.Prefab.name == prefabName)
                {
                    GameObject obj = Instantiate(entry.Prefab);
                    obj.name = prefabName;
                    return obj;
                }
            }
            
            return null;
        }

        [PunRPC]
        private static GameObject InstantiateObject(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
        {
            GameObject obj = Instantiate(prefab, position, rotation) as GameObject;
            if (obj == null) return null;

            obj.transform.parent = parent;

            if (parent != null) // If a parent transform was specified, set the position local to the parent.
                obj.transform.localPosition = position;
            else // Otherwise, set the position in relation to world space.
                obj.transform.position = position;

            return obj;
        }

        /// <summary>Spawns an object from the object pool, setting the object's parent to what you pass in.</summary>
        [PunRPC]
        public GameObject Spawn(GameObject originalPrefab, Vector3 position, Quaternion rotation, Transform parent, int autoPoolID = 0)
        {
            // If an AutoPoolID was passed in, try to find it in the AutoPool dictionary.
            if (autoPooledObjects != null && autoPoolID != 0 && autoPooledObjects.ContainsKey(autoPoolID))
            {
                GameObject pooledObj = autoPooledObjects[autoPoolID];
                if (pooledObj != null)
                {
                    pooledObj.transform.parent = parent;

                    if (parent != null) // If a parent transform was specified, set the position local to the parent.
                        pooledObj.transform.localPosition = position;
                    else // Otherwise, set the position in relation to world space.
                        pooledObj.transform.position = position;

                    pooledObj.transform.rotation = rotation;
                    pooledObj.SetActive(true);
                    return pooledObj;
                }
            }

            for (int i = 0; i < prefabsToPool.Count; i++)
            {
                GameObject prefab = prefabsToPool[i].Prefab;
                
                if (prefab == null) continue;
                if (prefab.name != originalPrefab.name) continue;
                
                if (Pool != null && Pool[i].Length > 0)
                {
                    // Find the first available object to spawn from the pool.
                    for (int j = 0; j < Pool[i].Length; j++)
                    { 
                        if (Pool[i][j] != null)
                        {
                            GameObject pooledObj = Pool[i][j];
                            Pool[i][j] = null;
                            pooledObj.transform.parent = parent;

                            if (parent != null) // If a parent transform was specified, set the position local to the parent.
                                pooledObj.transform.localPosition = position;
                            else // Otherwise, set the position in relation to world space.
                                pooledObj.transform.position = position;

                            pooledObj.transform.rotation = rotation;
                            pooledObj.SetActive(true);
                            return pooledObj;
                        }
                    }
                }

                if (Pool == null)
                {
                    GameObject pooledObj = InstantiateObject(prefabsToPool[i].Prefab, position, rotation, parent);
                    Debug.LogWarning("[" + originalPrefab.name + " was instantiated instead of spawned from pool. Reason: Pool is null.");
                    return pooledObj;
                }

                if (!prefabsToPool[i].OnlyPooled)
                {
                    GameObject pooledObj = InstantiateObject(prefabsToPool[i].Prefab, position, rotation, parent);
                    pooledObj.name = prefabsToPool[i].Prefab.name;
                    pooledObj.AddTag(Tag.Pooled);
                    if (!suppressWarnings)
                        Debug.LogWarning("[" + originalPrefab.name + " was instantiated instead of spawned from pool. Reason: No objects remaining in the pool (size: " + Pool[i].Length + "). Consider increasing the pool size.");
                    return pooledObj;
                }
                return null;
            }
            if (!suppressWarnings)
                Debug.LogWarning("[" + originalPrefab.name + "] was instantiated instead of spawned from pool. Reason: Prefab not found in pool.");
            return InstantiateObject(originalPrefab, position, rotation, parent);
        }

        /// <summary>Spawns an object from the object pool. The object will not be a child of any other object.</summary>
        /// 
        //[PunRPC]
        public GameObject Spawn(GameObject originalPrefab, Vector3 position, Quaternion rotation, int autoPoolID = 0)
        {
            return Spawn(originalPrefab, position, rotation, null, autoPoolID);
        }

        /// <summary>Put object back in the pool. By default, disabled children will not be re-enabled.</summary>
        public void PoolObject(GameObject obj)
        {
            PoolObject(obj, false);
        }

        /// <summary>Put object back in the pool. You can force children to be reenabled, if desired.</summary>
        public void PoolObject(GameObject obj, bool reenableChildren)
        {
            for (int i = 0; i < prefabsToPool.Count; i++)
            {
                if (prefabsToPool[i].Prefab == null) continue;
                if (prefabsToPool[i].Prefab.name != obj.name) continue;

                // Object was found. Deactivate it, stop/clear particle effects, and put it in the pool.
                obj.transform.parent = container.transform;
                ParticleSystem[] particleSystems = obj.GetComponentsInChildren<ParticleSystem>();
                for (int j = 0; j < particleSystems.Length; j++)
                {
                    particleSystems[j].Stop();
                    particleSystems[j].Clear();
                    particleSystems[j].enableEmission = true;
                }
                if (reenableChildren)
                {
                    Transform[] trans = obj.GetComponentsInChildren<Transform>(true);
                    for (int j = 0; j < trans.Length; j++)
                        trans[j].gameObject.SetActive(true);
                }

                obj.AddTag(Tag.Pooled);
                obj.SetActive(false);

                // Try to find an empty spot for the object to be placed in.
                for (int j=0; j<Pool[i].Length; j++)
                {
                    if (Pool[i][j] == null)
                    {
                        Pool[i][j] = obj;
                        return;
                    }
                }
                Destroy(obj);
                if (!suppressWarnings)
                    Debug.LogWarning("[" + obj.name + "] was destroyed instead of pooled. Reason: The pool size for this prefab was too small (" + Pool[i].Length + "). Consider increasing the pool size.");
                
                return;
            }

            Destroy(obj);
            if (!suppressWarnings)
                Debug.LogWarning("[" + obj.name + "] was destroyed instead of pooled. Reason: Prefab not found in pool.");
        }
    }
}