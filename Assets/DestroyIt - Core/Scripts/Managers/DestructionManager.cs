using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DestroyIt
{
    /// <summary>
    /// Destruction Manager (Singleton) - manages all destructible objects.
    /// Put this script on an empty game object named "_GameManager" (for example).
    /// </summary>
    public class DestructionManager : MonoBehaviour
    {

        // Editor Variables
        public int maxPersistentDebris = 400;       // Max number of debris pieces to keep in the world at once.
        public int destroyedPrefabLimit = 15;       // Max number of destroyed prefabs to instantiate within [withinSeconds] seconds. When this limit is reached, particle effects will be used.
        public int withinSeconds = 4;               // Number of seconds within which no more than [destroyedPrefabLimit] destructions will be instantiated.
        public ParticleSystem defaultLargeParticle; // The default particle effect to use for large object destructions.
        public ParticleSystem defaultSmallParticle; // The default particle effect to use for small object destructions.
        public float smallObjectMaxSize = 1f;       // Anything larger than this (in meters) is considered a large object, and will get the default large particle.
        public int obliterateMultiplier = 3;        // If damage is done to an object in excess of (damage * obliterateMultiplier), then it will be reduced to a particle effect only.
        public bool removeVisibleDebris = true;     // If true, persistent debris can be removed from the game even if the camera is rendering it.

        [HideInInspector]
        public bool useCameraDistanceLimit = true;  // If true, things beyond the specified distance from the main camera will be destroyed in a more limiting (ie, higher performance) way.
        [HideInInspector]
        public int cameraDistanceLimit = 100;       // Specified game units (usually meters) from camera, where destruction limiting will occur.
        [HideInInspector]
        public List<Debris> debrisPieces;

        // Properties
        public List<Destructible> MonitoredObjects { get; set; }
        public List<float> DestroyedPrefabCounter { get; private set; }
        public bool IsDestroyedPrefabLimitReached { get { return DestroyedPrefabCounter.Count >= destroyedPrefabLimit; } }

        public int ActiveDebrisCount
        {
            get
            {
                int count = 0;
                for (int i = 0; i < debrisPieces.Count; i++)
                {
                    if (debrisPieces[i].IsActive)
                        count ++;
                }
                return count;
            }
        }

        // Private Variables
        private float nextUpdate;
        private GameObject debrisContainer;
        private int debrisLayer = -1;
        private List<Destructible> destroyedObjects;
        public float updateFrequency = .5f;         // The time (in seconds) this script updates itself.

        // Hide the default constructor (use DestructionManager.Instance instead).
        private DestructionManager() { }

        // Here is a private reference only this class can access
        private static DestructionManager _instance;

        // This is the public reference that other classes will use
        public static DestructionManager Instance
        {
            get
            {
                // If _instance hasn't been set yet, we grab it from the scene.
                // This will only happen the first time this reference is used.
                if (_instance == null)
                    _instance = FindObjectOfType<DestructionManager>();
                return _instance;
            }
        }

        void Start()
        {
            // Initialize variables
            DestroyedPrefabCounter = new List<float>();

            debrisLayer = LayerMask.NameToLayer("DestroyItDebris");
            debrisPieces = new List<Debris>();
            destroyedObjects = new List<Destructible>();
            MonitoredObjects = new List<Destructible>();
            nextUpdate = Time.time + updateFrequency;

            // Create a Debris container game object if one doesn't exist.
            debrisContainer = GameObject.Find("_Debris");
            if (debrisContainer == null)
                debrisContainer = new GameObject("_Debris");

            // If the default particles haven't been assigned, try to get them from the Resources folder.
            if (defaultLargeParticle == null)
                defaultLargeParticle = Resources.Load<ParticleSystem>("Default_Particles/DefaultLargeParticle");
            if (defaultSmallParticle == null)
                defaultSmallParticle = Resources.Load<ParticleSystem>("Default_Particles/DefaultSmallParticle");
            
            // Checks
            Check.IsDefaultLargeParticleAssigned();
            Check.IsDefaultSmallParticleAssigned();
            if (Check.LayerExists("DestroyItDebris", false) == false)
                Debug.LogWarning("DestroyItDebris layer not found. Add a layer named 'DestroyItDebris' to your project if you want debris to ignore other debris when using Cling Points.");
        }

        void Update()
        {
            if (Time.time > nextUpdate)
            {
                // Manage Destroyed Prefab counter
                DestroyedPrefabCounter.Update(withinSeconds);

                // Manage Debris Queue
                if (debrisPieces.Count > 0)
                {
                    // Cleanup references to debris no longer in the game
                    debrisPieces.RemoveAll(x => x == null || !x.IsActive);
                    //TODO: Debris is getting removed from the list, but not destroyed from the game. Debris parent objects should probably check their children periodically for enabled meshes.

                    // Disable debris until the Max Debris limit is satisfied.
                    if (ActiveDebrisCount > maxPersistentDebris)
                    {
                        int overBy = ActiveDebrisCount - maxPersistentDebris;
                        
                        foreach (Debris debris in debrisPieces)
                        {
                            if (overBy <= 0) break;
                            if (!debris.IsActive) continue;
                            if (!removeVisibleDebris)
                            {
                                if (debris.Rigidbody.GetComponent<Renderer>() == null) continue;
                                if (debris.Rigidbody.GetComponent<Renderer>().isVisible) continue;
                            }
                            // Disable the debris.
                            debris.Disable();
                            overBy -= 1;
                        }
                    }
                }

                // Manage Destroyed Objects list (ie, we're spacing out the Destroy() calls for performance)
                if (destroyedObjects.Count > 0)
                {
                    // Destroy a maximum of 5 gameobjects per update, to space it out a little.
                    int nbrObjects = destroyedObjects.Count > 5 ? 5 : destroyedObjects.Count;
                    for (int i=0; i<nbrObjects; i++)
                    {
                        // Destroy the gameobject and remove it from the list.
                        if (destroyedObjects[i] != null && destroyedObjects[i].gameObject != null)
                            Destroy(destroyedObjects[i].gameObject);
                    }
                    destroyedObjects.RemoveRange(0, nbrObjects);
                }

                // Cleanup references to monitored objects no longer in the game
                if (MonitoredObjects.Count > 0)
                    MonitoredObjects.RemoveAll(x => x == null);

                nextUpdate = Time.time + updateFrequency; // reset the next update time.
            }
        }

        /// <summary>Swaps the current destructible object with a new one and applies the correct materials to the new object.</summary>
        public void ProcessDestruction<T>(Destructible oldObj, GameObject destroyedPrefab, T damageInfo, bool isObliterated)
        {
            // If there is no destructible object to create debris from, exit. (This should never happen, it's just thrown in here as a safety.)
            if (oldObj == null) return;

            // If for some reason, ProcessDestruction has been called on an object that is set to not be destroyed, exit.
            if (!oldObj.canBeDestroyed) return;
            
            // Look for any debris objects clinging to the old object and un-parent them before destroying the old object.
            oldObj.ReleaseClingingDebris(debrisContainer);

            // Remove any Joints from the destroyed object
            Joint[] joints = oldObj.GetComponentsInChildren<Joint>();
            for (int i=0; i<joints.Length; i++)
                Destroy(joints[i]);

            // Stop emitting ALL particles, even in children of each particle system.
            ParticleSystem[] allParticleEffects = oldObj.gameObject.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < allParticleEffects.Length; i++)
                allParticleEffects[i].enableEmission = false;

            // Detach any particle effects that may be attached to this object.
            // If the particle effect is tagged as Pooled, add the PooledAfter script to it.
            //TODO: Tidy this up in a method call or extension method, maybe?
            ParticleSystem[] particleEffects = oldObj.gameObject.GetComponentsInImmediateChildren<ParticleSystem>();
            for (int i = 0; i < particleEffects.Length; i++)
            {
                // Unparent the particle effect.
                particleEffects[i].gameObject.transform.parent = null;

                // If the particle came from the Object Pool...
                if (particleEffects[i].gameObject.HasTag(Tag.Pooled))
                {
                    // And doesn't already have a PoolAfter script on it...
                    if (particleEffects[i].gameObject.GetComponent<PoolAfter>() == null)
                    {
                        PoolAfter poolAfter = particleEffects[i].gameObject.AddComponent<PoolAfter>();
                        poolAfter.seconds = 5f;
                        poolAfter.removeWhenPooled = true; // remove the PoolAfter script before putting the object back in the pool.
                    }
                }
                else // If the particle didn't come from the Object Pool, queue it up for destruction.
                {
                    if (particleEffects[i].gameObject.GetComponent<DestroyAfter>() == null)
                    {
                        DestroyAfter destroyAfter = particleEffects[i].gameObject.AddComponent<DestroyAfter>();
                        destroyAfter.seconds = 5f;
                    }
                }
            }

            
            // OPTION: DESTRUCTIBLE OBJECT SINKS THROUGH TERRAIN WHEN DESTROYED.
            if (oldObj.sinkWhenDestroyed)
            {
                // First, turn Kinematic off for all rigidbodies under this object.
                Rigidbody[] rbodies = oldObj.GetComponentsInChildren<Rigidbody>();
                for (int i = 0; i < rbodies.Length; i++)
                {
                    rbodies[i].isKinematic = false;
                    rbodies[i].WakeUp();
                }
                // Next, strip off all colliders so it falls through the terrain.
                Collider[] colliders = oldObj.GetComponentsInChildren<Collider>();
                for (int i = 0; i < colliders.Length; i++)
                    colliders[i].enabled = false;
                // Attach the DestroyAfter script to the object so it will get removed from the game.
                DestroyAfter destAfter = oldObj.gameObject.AddComponent<DestroyAfter>();
                destAfter.seconds = 5f;
                // Exit immediately - don't do any more destruction processing.
                return;
            }

            // If there is a destructible object to destroy, but no debris to create, pop a particle effect and exit.
            // Likewise, if the object was obliterated, use only a particle effect.
            if (destroyedPrefab == null || (IsDestroyedPrefabLimitReached && oldObj.canBeObliterated) || isObliterated)
            {
                DestroyWithParticleEffect(oldObj, oldObj.fallbackParticle, damageInfo);
                return;
            }
            
            if (useCameraDistanceLimit && oldObj.canBeObliterated)
            {
                // Find the distance between the camera and the destroyed object
                float distance = Vector3.Distance(oldObj.transform.position, Camera.main.transform.position);
                if (distance > cameraDistanceLimit)
                {
                    DestroyWithParticleEffect(oldObj, oldObj.fallbackParticle, damageInfo);
                    return;
                }
            }

            // If we've passed the checks above, then we are creating debris.
            DestroyedPrefabCounter.Add(Time.time);

            // Unparent any specified child objects before destroying
            UnparentSpecifiedChildren(oldObj);

            // Put the destroyed object in the Debris layer to keep new debris from clinging to it
            if (debrisLayer != -1)
                oldObj.gameObject.layer = debrisLayer;

            // Try to get the object from the pool
            GameObject newObj = ObjectPool.Instance.Spawn(destroyedPrefab, oldObj.PositionFixedUpdate, oldObj.RotationFixedUpdate, oldObj.GetInstanceID());
            if (oldObj.tagDebrisColliders && !oldObj.autoPoolDestroyedPrefab)
            {
                TagAfter tagAfter = newObj.AddComponent<TagAfter>();
                tagAfter.tagWith = oldObj.tagDebrisCollidersWith;
            }
            InstantiateDebris(newObj, oldObj, damageInfo);

            oldObj.gameObject.SetActive(false);
            destroyedObjects.Add(oldObj);
        }

        private void DestroyWithParticleEffect<T>(Destructible oldObj, ParticleSystem customParticle, T damageInfo)
        {
            if (oldObj.useFallbackParticle)
            {
                // Use the DestructibleGroup instance ID if it exists, otherwise use the Destructible object's parent's instance ID.
                GameObject parentObj = oldObj.gameObject.GetHighestParentWithTag(Tag.DestructibleGroup);
                if (parentObj == null)
                    parentObj = oldObj.gameObject;
                int instanceId = parentObj.GetInstanceID();

                Vector3 position = oldObj.transform.TransformPoint(oldObj.MeshCenterPoint);

                // If the ParticleManager is available, use it to play the particle. Otherwise, just spawn it directly.
                if (customParticle != null)
                {
                    // If the ParticleManager is available, use it.
                    if (ParticleManager.Instance == null)
                        ObjectPool.Instance.Spawn(customParticle.gameObject, position, oldObj.transform.rotation);
                    else
                        ParticleManager.Instance.PlayEffect(customParticle, position, oldObj.transform.rotation,
                            instanceId);
                }
                else //Otherwise, use a default effect attached to the DestructionManager.
                {
                    // Find out how large the object is by adding the bounds extents of all its meshes.
                    ParticleSystem defaultParticle = defaultSmallParticle;
                    //TODO: Should probably switch this to use total COLLIDER bounds instead of MESH bounds to determine the object's overall size.
                    float totalMeshSize = 0f;
                    foreach (MeshFilter mesh in oldObj.GetComponentsInChildren<MeshFilter>())
                    {
                        if (mesh.sharedMesh == null) continue;
                        Bounds bounds = mesh.sharedMesh.bounds;
                        float maxMeshSize = Mathf.Max(bounds.size.x*mesh.transform.localScale.x,
                            bounds.size.y*mesh.transform.localScale.y, bounds.size.z*mesh.transform.localScale.z);
                        totalMeshSize += maxMeshSize;
                    }

                    if (totalMeshSize > smallObjectMaxSize)
                        defaultParticle = defaultLargeParticle;

                    // If the ParticleManager is available, use it to play the particle. Otherwise, just spawn it directly.
                    if (ParticleManager.Instance == null)
                        ObjectPool.Instance.Spawn(defaultParticle.gameObject, position, oldObj.transform.rotation);
                    else
                        ParticleManager.Instance.PlayEffect(defaultParticle, position, oldObj.transform.rotation,
                            instanceId, oldObj.fallbackParticleMaterial);
                }
            }

            UnparentSpecifiedChildren(oldObj);
            oldObj.gameObject.SetActive(false);
            destroyedObjects.Add(oldObj);

            // Reapply impact force to impact object so it punches through the destroyed object along its original path. 
            // If you turn this off, impact objects will be deflected even though the impacted object was destroyed.
            if (damageInfo.GetType() == typeof(ImpactInfo))
                DestructibleHelper.ReapplyImpactForce(damageInfo as ImpactInfo, oldObj.VelocityReduction);
        }

        private void UnparentSpecifiedChildren(Destructible obj)
        {
            foreach (GameObject child in obj.unparentOnDestroy)
            {
                if (child == null)
                    continue;

                // Unparent the child object from the destructible object.
                child.transform.parent = null;

                // Initialize any DelayedRigidbody scripts on the object.
                DelayedRigidbody[] delayedRigidbodies = child.GetComponentsInChildren<DelayedRigidbody>();
                foreach (DelayedRigidbody dr in delayedRigidbodies)
                    dr.Initialize();

                // Turn off Kinematic on child objects, so they will fall freely.
                List<Rigidbody> rigidbodies = child.GetComponentsInChildren<Rigidbody>().ToList();
                foreach (Rigidbody rbody in rigidbodies)
                    rbody.isKinematic = false;

                // Turn off any animations
                Animation[] animations = child.GetComponentsInChildren<Animation>();
                foreach (Animation anim in animations)
                    anim.enabled = false;
            }
        }

        private void InstantiateDebris<T>(GameObject newObj, Destructible oldObj, T damageInfo)
        {
            // Apply new materials derived from previous object's materials
            if (!oldObj.autoPoolDestroyedPrefab)
                DestructibleHelper.TransferMaterials(oldObj, newObj);

            // Re-scale destroyed version if original destructible object has been scaled. (Scaling rigidbodies in general is bad, but this is put here for convenience.)
            if (oldObj.transform.lossyScale != new Vector3(1f, 1f, 1f)) // if destructible object has been scaled in the scene
                newObj.transform.localScale = oldObj.transform.lossyScale;

            if (oldObj.isDebrisChipAway)
            {
                // If we are doing chip-away debris, attach the ChipAwayDebris script to each piece of debris and exit.
                Collider[] debrisColliders = newObj.GetComponentsInChildren<Collider>();
                for (int i=0; i<debrisColliders.Length; i++)
                {
                    if (debrisColliders[i].gameObject.GetComponent<ChipAwayDebris>() != null) continue;

                    if (debrisColliders[i].attachedRigidbody != null) continue;

                    ChipAwayDebris chipAwayDebris = debrisColliders[i].gameObject.AddComponent<ChipAwayDebris>();
                    chipAwayDebris.debrisMass = oldObj.chipAwayDebrisMass;
                    chipAwayDebris.debrisDrag = oldObj.chipAwayDebrisDrag;
                    chipAwayDebris.debrisAngularDrag = oldObj.chipAwayDebrisAngularDrag;
                }
                return;
            }

            if (oldObj.childrenToReParentByName.Count > 0)
            {
                for (int i = 0; i < oldObj.childrenToReParentByName.Count; i++)
                {
                    Transform child = oldObj.transform.Find(oldObj.childrenToReParentByName[i]);
                    if (child != null)
                        child.parent = newObj.transform;
                }
            }

            // Attempt to get the debris rigidbodies from auto-pooled rigidbodies first.
            Rigidbody[] debrisRigidbodies = oldObj.pooledRigidbodies;
            GameObject[] debrisRigidbodyGos = oldObj.pooledRigidbodyGos;
            if (debrisRigidbodies.Length == 0) // If none exist, do it the more expensive way...
            {
                debrisRigidbodies = newObj.GetComponentsInChildren<Rigidbody>();
                debrisRigidbodyGos = new GameObject[debrisRigidbodies.Length];
                for (int i = 0; i < debrisRigidbodies.Count(); i++)
                    debrisRigidbodyGos[i] = debrisRigidbodies[i].gameObject;
            }

            for (int i = 0; i < debrisRigidbodies.Length; i++)
            {
                // Assign each piece of debris to the Debris layer if it exists.
                if (debrisLayer != -1)
                    debrisRigidbodies[i].gameObject.layer = debrisLayer;

                // Reparent any debris tagged for reparenting.
                if (oldObj.debrisToReParentByName.Contains(debrisRigidbodies[i].name))
                {
                    debrisRigidbodies[i].gameObject.transform.parent = oldObj.transform.parent;
                    debrisRigidbodies[i].isKinematic = true;
                }

                // Add leftover velocity from destroyed object
                if (!debrisRigidbodies[i].isKinematic)
                    debrisRigidbodies[i].velocity = oldObj.VelocityFixedUpdate;

                // Add debris to the debris queue.
                Debris debris = new Debris() { Rigidbody = debrisRigidbodies[i], GameObject = debrisRigidbodyGos[i]};
                debrisPieces.Add(debris);
            }

            // Attempt to make some of the debris cling to adjacent rigidbodies
            if (oldObj.checkForClingingDebris)
                newObj.MakeDebrisCling();

            // Reapply impact force to impact object so it punches through the destroyed object along its original path. 
            // If you turn this off, impact objects will be deflected even though the impacted object was destroyed.
            if (damageInfo.GetType() == typeof(ImpactInfo))
                DestructibleHelper.ReapplyImpactForce(damageInfo as ImpactInfo, oldObj.VelocityReduction);

            if (damageInfo.GetType() == typeof(ExplosionInfo) || damageInfo.GetType() == typeof(ImpactInfo))
                ExplosionHelper.ApplyForcesToDebris(newObj, 1f, damageInfo);
        }
    }
}