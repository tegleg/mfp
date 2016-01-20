using System;
using System.Collections.Generic;
using UnityEngine;

namespace DestroyIt
{
    public class DamageLevel
    {
        public int Level { get; set; }
        public int MaxHitPoints { get; set; }
        public int MinHitPoints { get; set; }
    }

    [Serializable]
    public class DamageEffect
    {
        public int TriggeredAt;
        public Vector3 Offset;
        public Vector3 Rotation;
        public GameObject Effect;
        public bool HasStarted;
        public bool UseDependency;
        public Tag TagDependency;
    }

    public class Debris
    {
        public Rigidbody Rigidbody { get; set; }
        public GameObject GameObject { get; set; }
        public bool IsActive
        {
            get {return (Rigidbody != null && GameObject.activeSelf);}
        }

        public void Disable()
        {
            if (Rigidbody != null)
                GameObject.SetActive(false);
        }
    }

    public class ExplosionInfo
    {
        public float BlastForce { get; set; }
        public Vector3 Position { get; set; }
        public float Radius { get; set; }
        public float UpwardModifier { get; set; }

        public ExplosionInfo() { }

        public ExplosionInfo(float blastForce, Vector3 position, float radius, float upwardModifier)
        {
            BlastForce = blastForce;
            Position = position;
            Radius = radius;
            UpwardModifier = upwardModifier;
        }

        public int Damage
        {
            get { return Convert.ToInt32(BlastForce); }
        }
    }

    /// <summary>Contains information about the object that collided with a destructible object.</summary>
    public class ImpactInfo
    {
        /// <summary>The amount of damage done by the impact force.</summary>
        public int Damage { get; set; }

        /// <summary>A reference to the object that collided into the destructible object.</summary>
        public Rigidbody ImpactObject { get; set; }

        public Vector3 ImpactObjectVelocityFrom { get; set; }

        public Vector3 ImpactObjectVelocityTo
        {
            get { return ImpactObjectVelocityFrom * -1; }
        }

        public float AdditionalForce { get; set; }

        public Vector3 AdditionalForcePosition { get; set; }

        public float AdditionalForceRadius { get; set; }
    }

    public class ActiveParticle
    {
        public GameObject GameObject { get; set; }
        public float InstantiatedTime { get; set; }
        public int ParentId { get; set; }
    }

    public class MaterialLookup
    {
        public List<Material> DamagedMaterials { get; set; }
        public Material DestroyedMaterial { get; set; }
    }

    [Serializable]
    public class DamageLevelMaterials
    {
        public Material[] Materials;
    }

    [Serializable]
    public class MaterialMapping
    {
        public Material SourceMaterial;
        public Material ReplacementMaterial;
    }

    [Serializable]
    public class PoolEntry
    {
        public int DestructibleInstanceID;
        public GameObject Prefab;
        public int Count;
        public bool OnlyPooled;
    }
}