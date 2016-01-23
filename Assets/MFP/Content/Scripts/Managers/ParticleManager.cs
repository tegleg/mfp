using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using System;

/// <summary>
/// Particle Manager (Singleton) - manages the playing of particle effects and handles performance throttling.
/// Call the PlayEffect() method, and this script decides whether to play the effect based on how many are currently active.
/// </summary>
namespace DestroyIt
{
    public class ParticleManager : MonoBehaviour
    {
        public int maxDestroyedParticles = 20; // max particles to allow during [withinSeconds].
        public int maxPerDestructible = 5;  // max particles to allow for a single Destructible object or DestructibleGroup.
        public float withinSeconds = 4f;    // remove particles from the managed list after this many seconds.
        public float updateFrequency = .5f; // The time (in seconds) this script updates its counters

        public static ParticleManager Instance { get; private set; }
        public ActiveParticle[] ActiveParticles 
        { 
            get { return activeParticles; } 
            private set { activeParticles = value;} 
        }
        public bool IsMaxActiveParticles
        {
            get { return ActiveParticles.Length >= maxDestroyedParticles; }
        }

        private float nextUpdate;
        private ActiveParticle[] activeParticles;
        private ParticleManager() { } // hide constructor

        void Awake()
        {
            ActiveParticles = new ActiveParticle[0];
            Instance = this;
            nextUpdate = Time.time + updateFrequency;
        }

        void Update()
        {
            if (!(Time.time > nextUpdate)) return;
            if (activeParticles.Length == 0) return;

            int removeIndicesCounter = 0;
            int[] removeIndices = new int[0];
            for (int i = 0; i < ActiveParticles.Length;i++ )
            {
                if (Time.time >= ActiveParticles[i].InstantiatedTime + withinSeconds)
                {
                    removeIndicesCounter++;
                    Array.Resize(ref removeIndices, removeIndicesCounter);
                    removeIndices[removeIndicesCounter - 1] = i;
                }
            }
            activeParticles = activeParticles.RemoveAllAt(removeIndices);

            // Reset the nextUpdate counter.
            nextUpdate = Time.time + updateFrequency; 
        }

        /// <summary>Plays the particle effect as-is, without replacing the material.</summary>
        public void PlayEffect(ParticleSystem effect, Vector3 pos, Quaternion rot, int parentId)
        {
            PlayEffect(effect, pos, rot, parentId, null);
        }

        /// <summary>Plays the particle effect, but replaces the material on the particle with the one specified (mat).</summary>
        public void PlayEffect(ParticleSystem effect, Vector3 pos, Quaternion rot, int parentId, Material mat)
        {
            if (effect == null) return;

            // Check if we're at the maximum active particle limit. If so, ignore the request to play the particle effect.
            if (IsMaxActiveParticles) return;

            // Check if we've reached the max particle limit per destructible object for this object already.
            int parentParticleCount = ActiveParticles.Count(x => x.ParentId == parentId);
            if (parentParticleCount > maxPerDestructible) return;

            // Instantiate and add to the ActiveParticles counter
            GameObject spawn = ObjectPool.Instance.Spawn(effect.gameObject, pos, rot);
            if (spawn == null || spawn.GetComponent<ParticleSystem>() == null) return;
            ActiveParticle aParticle = new ActiveParticle() { GameObject = spawn, InstantiatedTime = Time.time, ParentId = parentId };
            Array.Resize(ref activeParticles, activeParticles.Length + 1);
            ActiveParticles[activeParticles.Length - 1] = aParticle;

            // Replace the materials on the particle effect with the one passed in.
            if (mat != null && spawn.GetComponent<ParticleSystem>() != null)
            {
                foreach (ParticleSystemRenderer ps in spawn.GetComponentsInChildren<ParticleSystemRenderer>())
                {
                    if (ps.renderMode == ParticleSystemRenderMode.Mesh)
                        ps.sharedMaterial = mat;
                }
            }
        }
    }
}