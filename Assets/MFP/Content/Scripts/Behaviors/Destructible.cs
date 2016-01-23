using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DestroyIt
{
    /// <summary>
    /// Put this script on any rigidbody object you want to be "destructible". The script should go on the same GameObject as the RigidBody.
    /// The rigidbody should also have attached colliders, either on the same game object or one or more children under it.
    /// </summary>
    public class Destructible : MonoBehaviour
    {
        [HideInInspector]
        public int totalHitPoints = 50;
        [HideInInspector]
        public int currentHitPoints = 50;
        [HideInInspector]
        public GameObject destroyedPrefab;
        [HideInInspector]
        public ParticleSystem fallbackParticle;         // The custom particle effect to use for this object. If blank, a default particle effect from DestructionManager will be used.
        [HideInInspector]
        public Material fallbackParticleMaterial;
        [HideInInspector]
        public List<DamageEffect> damageLevelParticles;
        [HideInInspector]
        public float velocityReduction = .5f;           // How much this object reduces the velocity of fast-moving objects (those with ForceCollisions scripts) when it is destroyed.
        [HideInInspector]
        public List<GameObject> unparentOnDestroy;      // A list of game objects that are children under the destructible object, which will get un-parented when the object is destroyed.
        [HideInInspector]
        public List<MaterialMapping> replaceMaterials;  // Specifies the replacement of one material on your destroyed prefab with another, at the time of destruction.
        [HideInInspector]
        public bool canBeDestroyed = true;              // If true, object will be destroyed when its hit points reach zero. Set to false if you want visible damage but never want the object to go away.
        [HideInInspector]
        public bool canBeObliterated = true;            // If true, object will be converted to low detail model if the damage is more than twice its hit points. If damage is more than three times its hit points, it'll be converted into a particle effect. This is good for performance when large bombs do massive damage to many destructible objects at once. 
        [HideInInspector]
        public bool debugMonitor = false;               // If true, provides detailed information about the destructible object, such as hit points, etc. in the HeadsUpDisplay.
        [HideInInspector]
        public List<string> debrisToReParentByName;
        [HideInInspector]
        public List<string> childrenToReParentByName;
        [HideInInspector]
        public int destructibleGroupId = 0;
        [HideInInspector]
        public bool isDebrisChipAway = false;
        [HideInInspector]
        public float chipAwayDebrisMass = 1f;
        [HideInInspector]
        public float chipAwayDebrisDrag = 0f;
        [HideInInspector]
        public float chipAwayDebrisAngularDrag = 0.05f;
        [HideInInspector]
        public bool tagDebrisColliders = false;
        [HideInInspector]
        public Tag tagDebrisCollidersWith = Tag.Untagged;
        [HideInInspector]
        public bool autoPoolDestroyedPrefab = true;
        [HideInInspector]
        public bool checkForClingingDebris = true; // This is an added optimization used when we are auto-pooling destroyed prefabs. It allows us to avoid a GetComponentsInChildren() check for ClingPoints destruction time.
        [HideInInspector]
        public Rigidbody[] pooledRigidbodies; // This is an added optimization used when we are auto-pooling destroyed prefabs. It allows us to avoid multiple GetComponentsInChildren() checks for Rigidbodies at destruction time.
        [HideInInspector]
        public GameObject[] pooledRigidbodyGos; // This is an added optimization used when we are auto-pooling destroyed prefabs. It allows us to avoid multiple GetComponentsInChildren() checks for the GameObjects on Rigidibodies at destruction time.
        [HideInInspector] 
        public bool useFallbackParticle = true;
        [HideInInspector]
        public bool sinkWhenDestroyed = false;  // If true, object will sink into the ground instead of being destroyed into debris.

        private const float invulnerableTimer = 0.5f; // How long (in seconds) the destructible object is invulnerable after instantiation.
        private List<DamageLevel> damageLevels;
        private DamageLevel currentDamageLevel;

        // Properties
        public float VelocityReduction { get { return Mathf.Abs(velocityReduction - 1f); /* invert the velocity reduction value (so it makes sense in the UI) */ } }
        public bool IsObliterated { get; set; }
        public Quaternion RotationFixedUpdate { get; private set; }
        public Vector3 PositionFixedUpdate { get; set; }
        public Vector3 VelocityFixedUpdate { get; set; }
        public bool IsDestroyed { get; set; }
        public bool IsInvulnerable { get; set; } // Determines whether the destructible object starts with a short period of invulnerability. Prevents destructible debris from being immediately destroyed by the same forces that destroyed the original object.
        public Hashtable MaterialReplacements {get; set;}    // Same as the replaceMaterials list, but this one can't be serialized by Unity, so we sew it together at runtime.
        public Vector3 MeshCenterPoint { get; set; }
        public bool IsInitialized { get; private set; }
        /// <summary>Stores a reference to this destructible object's rigidbody, so we don't have to GetComponent() at runtime.</summary>
        public Rigidbody Rigidbody { get; set; }

        private void Start()
        {
            if (!IsInitialized)
                Initialize();
        }

        public void Initialize()
        {
            List<MeshRenderer> meshes = GetComponentsInChildren<MeshRenderer>().ToList();

            // Checks
            Check.HasNonTriggerCollider(gameObject);
            Check.HasMeshRenderer(gameObject, meshes);
            Check.HasMaterial(gameObject, meshes);

            // Set fallbackParticle material (destroyed material version)
            if (fallbackParticleMaterial == null)
            {
                MeshRenderer meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
                Material particleMat = null;
                if (meshRenderer != null && meshRenderer.sharedMaterials.Length > 0)
                    particleMat = meshRenderer.sharedMaterials[0];
                fallbackParticleMaterial = particleMat.GetDestroyedMaterial();
            }

            // Store a reference to this object's rigidbody, for performance.
            Rigidbody = GetComponent<Rigidbody>();

            // Reset Hit Points to max if it's beyond valid values.
            if (currentHitPoints > totalHitPoints || currentHitPoints < 0)
                currentHitPoints = totalHitPoints;
            
            damageLevels = DestructibleHelper.CalculateDamageLevels(totalHitPoints);

            if (debugMonitor)
                DestructionManager.Instance.MonitoredObjects.Add(this);
            
            // Setup material replacements hashtable
            MaterialReplacements = new Hashtable();
            foreach (MaterialMapping matMap in replaceMaterials)
            {
                if (MaterialReplacements.ContainsKey(matMap.SourceMaterial))
                {
                    Debug.LogWarning("[Destructible]: " + gameObject.name + " contains multiple replacements for material " + matMap.SourceMaterial + ".");
                    continue;
                }
                MaterialReplacements.Add(matMap.SourceMaterial, matMap.ReplacementMaterial);
            }

            SetDamageLevelMaterials();

            // Advance the damage state if this destructible object starts with less than 100% hit points
            if (currentHitPoints < totalHitPoints)
                SetDamageLevel();

            IsInvulnerable = true;

            Invoke("RemoveInvulnerability", invulnerableTimer);

            IsInitialized = true;
        }

        private void RemoveInvulnerability()
        {
            IsInvulnerable = false;

            // Check if destructible object is damaged or destroyed.
            if (currentHitPoints > 0 && currentHitPoints < totalHitPoints)
                SetDamageLevel();

            if (canBeDestroyed && currentHitPoints <= 0)
            {
                IsDestroyed = true;
                PlayDamageLevelEffects();
                DestructionManager.Instance.ProcessDestruction(this, destroyedPrefab, new ExplosionInfo(), IsObliterated);
            }
        }

        private void FixedUpdate()
        {
            if (!IsInitialized) return;

            // Use the fixed update position/rotation for placement of the destroyed prefab.
            PositionFixedUpdate = transform.position;
            RotationFixedUpdate = transform.rotation;
            if (Rigidbody != null)
                VelocityFixedUpdate = Rigidbody.velocity;
        }

        private void SetDamageLevelMaterials()
        {
            MeshRenderer[] meshRenderers = gameObject.GetComponentsInChildren<MeshRenderer>();
            for (int i = 0; i < meshRenderers.Length; i++)
            {
                if (meshRenderers[i].gameObject.GetComponent<DamageMaterials>() == null)
                    meshRenderers[i].gameObject.AddComponent<DamageMaterials>();
            }

            MeshCenterPoint = GetMeshCenterPoint(this);
        }

        private static Vector3 GetMeshCenterPoint(Destructible destructibleObj)
        {
            Bounds combinedBounds = new Bounds();

            MeshFilter[] meshFilters = destructibleObj.GetComponentsInChildren<MeshFilter>();
            foreach (MeshFilter meshFilter in meshFilters)
                combinedBounds.Encapsulate(meshFilter.mesh.bounds);

            return combinedBounds.center;
        }

        /// <summary>Applies a generic amount of damage, with no special impact or explosive force./// </summary>
        public void ApplyDamage(int damageAmount)
        {
            //TODO: This method should call to ProcessDestruction with a general, non-specific DamageInfo.
            //HACK: Use ImpactInfo with no impact force for now.
            ImpactInfo generalDamage = new ImpactInfo() { Damage = damageAmount };
            ApplyImpactDamage(generalDamage);
        }

        //TODO: Combine these methods into one ApplyDamage overload, and make ExplosionInfo and ImpactInfo inherit from an interface or base class.
        public void ApplyExplosiveDamage(ExplosionInfo explosionInfo)
        {
            if (this.IsDestroyed || this.IsInvulnerable) return; // don't try to apply damage to an already-destroyed object.
            //Debug.Log("Current hit points: " + CurrentHitPoints + "; blast force: " + explosionInfo.BlastForce);
            currentHitPoints -= explosionInfo.Damage;

            CheckForObliterate(explosionInfo.Damage);

            SetDamageLevel();
            if (currentHitPoints <= 0)
            {
                this.IsDestroyed = true;
                PlayDamageLevelEffects();
                if (canBeDestroyed)
                    DestructionManager.Instance.ProcessDestruction(this, destroyedPrefab, explosionInfo, IsObliterated);
            }
        }

        //TODO: Combine these methods into one ApplyDamage overload, and make ExplosionInfo and ImpactInfo inherit from an interface or base class.
        public void ApplyImpactDamage(ImpactInfo impactInfo)
        {
            if (IsDestroyed || IsInvulnerable) return; // don't try to apply damage to an already-destroyed object.

            currentHitPoints -= impactInfo.Damage;

            CheckForObliterate(impactInfo.Damage);

            SetDamageLevel();
            if (currentHitPoints <= 0)
            {
                IsDestroyed = true;
                PlayDamageLevelEffects();
                // Advance to the next destructible object, passing in the leftover damage to apply to the next model and 
                // the projectile, which may have force added to it so it can punch through the final (destroyed) object.
                if (canBeDestroyed)
                    DestructionManager.Instance.ProcessDestruction(this, destroyedPrefab, impactInfo, IsObliterated);
            }
        }

        /// <summary>Check to see if the destructible object has been obliterated from taking excessive damage. If so, set the ObliteratedLevel on the object.</summary>
        /// <param name="damage">The amount of damage applied to the object from a single source.</param>
        private void CheckForObliterate(int damage)
        {
            if (IsInvulnerable || !canBeDestroyed || !canBeObliterated) return;

            if (damage >= (DestructionManager.Instance.obliterateMultiplier * totalHitPoints))
                IsObliterated = true;
        }

        /// <summary>Advances the damage state, applies damage-level materials as needed, and plays particle effects.</summary>
        private void SetDamageLevel()
        {
            if (damageLevels == null) return;

            DamageLevel newDamageLevel;
            if (currentHitPoints <= 0)
                newDamageLevel = damageLevels[damageLevels.Count - 1];
            else
                newDamageLevel = damageLevels.FirstOrDefault(
                        x => x.MaxHitPoints >= currentHitPoints && x.MinHitPoints <= currentHitPoints);

            if (newDamageLevel == null) return;
            
            if (currentDamageLevel == null || newDamageLevel.Level != currentDamageLevel.Level)
            {
                currentDamageLevel = newDamageLevel;
                //Debug.Log(String.Format("{0} - curr: {1}, new: {2}", this.name, currentDamageLevel.Level, newDamageLevel.Level));
                this.ApplyDamageToMaterials(currentDamageLevel.Level);

                PlayDamageLevelEffects();
            }
        }

        private void PlayDamageLevelEffects()
        {
            // Check if we should play a particle effect for this damage level
            if (damageLevelParticles != null)
            {
                for (int i = 0; i < damageLevelParticles.Count; i++)
                {
                    if (damageLevelParticles[i] != null && damageLevelParticles[i].Effect != null)
                    {
                        // get rotation
                        Quaternion rotation = transform.rotation;
                        if (damageLevelParticles[i].Rotation != Vector3.zero)
                            rotation = Quaternion.Euler(damageLevelParticles[i].Rotation);
                        if (damageLevelParticles[i].UseDependency && !gameObject.HasTag(damageLevelParticles[i].TagDependency))
                            continue;

                        if (currentDamageLevel != null && currentDamageLevel.Level >= damageLevelParticles[i].TriggeredAt && !IsDestroyed && !damageLevelParticles[i].HasStarted)
                        {
                            // set parent to this destructible object and play
                            ObjectPool.Instance.Spawn(damageLevelParticles[i].Effect.gameObject, damageLevelParticles[i].Offset, rotation, transform);
                            damageLevelParticles[i].HasStarted = true;
                        }
                        if (IsDestroyed && !damageLevelParticles[i].HasStarted)
                        {
                            //TODO: REFACTOR. Reason: This is weird, having to check for canBeDestroyed when we've established that IsDestroyed is true at this point.
                            if (canBeDestroyed)
                                ObjectPool.Instance.Spawn(damageLevelParticles[i].Effect.gameObject, transform.TransformPoint(damageLevelParticles[i].Offset), rotation);
                            else
                                ObjectPool.Instance.Spawn(damageLevelParticles[i].Effect.gameObject, damageLevelParticles[i].Offset, rotation, transform);
                        }
                    }
                }
            }
        }

        // NOTE: OnCollisionEnter will only fire if a rigidbody is attached to this object!
        private void OnCollisionEnter(Collision collision)
        {
            this.ProcessDestructibleCollision(collision, GetComponent<Rigidbody>());

            if (collision.contacts.Length > 0)
            { 
                Destructible destructibleObj = collision.contacts[0].otherCollider.gameObject.GetComponentInParent<Destructible>();
                if (destructibleObj != null && collision.contacts[0].otherCollider.attachedRigidbody == null)
                    destructibleObj.ProcessDestructibleCollision(collision, GetComponent<Rigidbody>());
            }
        }

        void OnDrawGizmos()
        {
            if (damageLevelParticles != null)
            {
                foreach (DamageEffect dlp in damageLevelParticles)
                {
                    if (dlp != null)
                    {
                        Gizmos.color = Color.cyan;
                        Gizmos.DrawWireCube(transform.TransformPoint(dlp.Offset), new Vector3(0.1f, 0.1f, 0.1f));
                        Vector3 rotatedVector = Quaternion.Euler(dlp.Rotation) * Vector3.forward;
                        Gizmos.DrawRay(transform.TransformPoint(dlp.Offset), rotatedVector * .5f);
                    }
                }
            }
        }
    }
}