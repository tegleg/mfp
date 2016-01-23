using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DestroyIt
{
    public static class Check
    {
        public static bool DestructionManagerIsReady(GameObject go)
        {
            if (DestructionManager.Instance == null)
            {
                GameObject gameManager = GameObject.Find("_GameManager");
                if (gameManager == null)
                    gameManager = new GameObject("_GameManager");
                DestructionManager dm = gameManager.AddComponent<DestructionManager>();
                dm.defaultLargeParticle = Resources.Load("Default_Particles/DefaultLargeParticle", typeof(ParticleSystem)) as ParticleSystem;
                dm.defaultSmallParticle = Resources.Load("Default_Particles/DefaultSmallParticle", typeof(ParticleSystem)) as ParticleSystem;
                
                Debug.LogWarning("[" + go.name + "]: DestructionManager was not found, so it was created at runtime for you. You should add the DestructionManager script to your GameManager going forward.");
                return false;
            }
            return true;
        }

        public static bool ObjectPoolIsReady(GameObject go)
        {
            if (ObjectPool.Instance == null)
            {
                GameObject gameManager = GameObject.Find("_GameManager");
                if (gameManager == null)
                    gameManager = new GameObject("_GameManager");
                gameManager.AddComponent<ObjectPool>();

                Debug.LogWarning("[" + go.name + "]: ObjectPool was not found, so it was created at runtime for you. You should add the ObjectPool script to your GameManager going forward.");
                return false;
            }
            return true;
        }

        public static bool HasNonTriggerCollider(GameObject go)
        {
            if (DestructionManager.Instance == null) return false;

            Collider[] colls = go.GetComponentsInChildren<Collider>();
            bool colliderFound = false;
            foreach (Collider coll in colls)
            {
                if (!coll.isTrigger)
                    colliderFound = true;
            }
            if (!colliderFound)
            {
                Debug.LogWarning("[" + go.name + "]: No collider found. Each destructible object should have at least one non-trigger collider, whether on it or one of its children.");
                return false;
            }
            return true;
        }

        public static bool HasMeshRenderer(GameObject go, List<MeshRenderer> meshes)
        {
            if (DestructionManager.Instance == null) return false;

            if (meshes == null)
                meshes = go.GetComponentsInChildren<MeshRenderer>().ToList();

            if (meshes.Count == 0)
            {
                Debug.LogError("[" + go.name + "]: No Mesh Renderer found. Each destructible object needs at least one mesh.");
                return false;
            }
            return true;
        }

        public static bool HasMaterial(GameObject go, List<MeshRenderer> meshes)
        {
            if (DestructionManager.Instance == null) return false;

            if (meshes == null)
                meshes = go.GetComponentsInChildren<MeshRenderer>().ToList();

            foreach (MeshRenderer mesh in meshes)
            {
                if (mesh.sharedMaterials.Length > 0)
                    return true;
            }

            Debug.LogError("[" + go.name + "]: No mesh materials found. Each destructible object needs at least one material on its mesh(es).");
            return false;
        }

        public static bool IsDefaultLargeParticleAssigned()
        {
            if (DestructionManager.Instance == null) return false;

            if (DestructionManager.Instance.defaultLargeParticle == null)
            {
                Debug.LogError("DestructionManager: Default Large Particle is not assigned. You should assign a default large particle for simple destructible objects OVER 1m in size.");
                return false;
            }
            return true;
        }

        public static bool IsDefaultSmallParticleAssigned()
        {
            if (DestructionManager.Instance == null) return false;

            if (DestructionManager.Instance.defaultSmallParticle == null)
            {
                Debug.LogError("[DestructionManager] Default Small Particle is not assigned. You should assign a default small particle for simple destructible objects UNDER 1m in size.");
                return false;
            }
            return true;
        }

        public static bool LayerExists(string layerName, bool logMessage)
        {
            if (DestructionManager.Instance == null) return false;

            int layer = LayerMask.NameToLayer(layerName);
            if (layer == -1)
            {
                if (logMessage)
                    Debug.LogWarning(String.Format("[DestroyIt Core] Layer \"{0}\" does not exist. Please add a layer named \"{0}\" to your project.", layerName));
                return false;
            }
            return true;
        }
    }
}