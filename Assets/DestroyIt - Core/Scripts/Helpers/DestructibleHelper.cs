using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DestroyIt
{
    public static class DestructibleHelper
    {
        /// <summary>Calculates the damage states for a destructible object, based on number of states and hitpoints.
        /// Hitpoints are evenly divided.</summary>
        public static List<DamageLevel> CalculateDamageLevels(int maxHitPoints)
        {
            const int nbrStates = 4; // Set to four progressive damage states. NOTE: if you change this, you will need to also change the damage shaders, textures, and masks.
            if (maxHitPoints <= 0) { return null; }

            int hitPointsPerDamageLevel = (maxHitPoints / (nbrStates + 1));
            List<DamageLevel> damageStages = new List<DamageLevel>();

            int hpMax = maxHitPoints;
            int hpMin = maxHitPoints - hitPointsPerDamageLevel + 1;

            for (int i = 0; i < nbrStates + 1; i++)
            {
                damageStages.Add(new DamageLevel { Level = i, MaxHitPoints = hpMax, MinHitPoints = hpMin });
                hpMax -= hitPointsPerDamageLevel;
                hpMin -= hitPointsPerDamageLevel;
            }
            damageStages[nbrStates].MinHitPoints = 0; //Make last damage stage zero for MinHitPoints to fix rounding errors.
            return damageStages;
        }

        /// <summary>When a Destructible Object is DESTROYED, this script will attempt to find and transfer the appropriate damaged materials over to the new prefab.</summary>
        public static void TransferMaterials(Destructible oldObj, GameObject newObj)
        {
            if (oldObj == null) return;

            if (!oldObj.IsInitialized)
                oldObj.Initialize();

            MeshRenderer[] oldMeshes = oldObj.GetComponentsInChildren<MeshRenderer>();
            MeshRenderer[] newMeshes = newObj.GetComponentsInChildren<MeshRenderer>();

            // If either object has no meshes, then there's nothing to transfer, so exit.
            if (oldMeshes.Length == 0 || newMeshes.Length == 0) return;

            // Set reflection anchor overrides on new meshes if any exist on the old meshes, but only if it is the same type of material (ie, tags match)
            for (int i = 0; i < oldMeshes.Length; i++)
            {
                TagIt oldMeshTag = oldMeshes[i].GetComponent<TagIt>();
                if (oldMeshTag == null) continue;
                if (oldMeshes[i].probeAnchor != null)
                {
                    for (int j = 0; j < newMeshes.Length; j++)
                    {
                        TagIt newMeshTag = newMeshes[j].GetComponent<TagIt>();
                        if (newMeshTag != null && oldMeshTag.tags.Intersect(newMeshTag.tags).Any()) // make sure TagIt tags have at least one match.
                            newMeshes[j].probeAnchor = oldMeshes[i].probeAnchor;
                    }
                }
            }

            // Get new materials for each destroyed mesh
            for (int i=0; i<newMeshes.Length; i++)
                newMeshes[i].sharedMaterials = GetNewMaterialsForDestroyedMesh(newMeshes[i], oldObj);
        }

        private static Material[] GetNewMaterialsForDestroyedMesh(MeshRenderer destMesh, Destructible destructibleObj)
        {
            if (destructibleObj == null) return null;

            Material[] curMats = destMesh.sharedMaterials;
            Material[] newMats = new Material[curMats.Length];
            for (int i = 0; i < curMats.Length; i++)
            {
                Material currentMat = curMats[i];
                if (currentMat == null) continue;

                if (destructibleObj.MaterialReplacements == null) continue;

                Material replacementMat = destructibleObj.MaterialReplacements[currentMat] as Material;
                if (replacementMat != null)
                    newMats[i] = replacementMat.GetDestroyedMaterial();
                else
                    newMats[i] = currentMat.GetDestroyedMaterial();
            }
            return newMats;
        }

        public static Material GetDestroyedMaterial(this Material mat)
        {
            if (mat == null) return null;

            if (MaterialPreloader.Instance == null) return mat;
            
            MaterialLookup matLookup = MaterialPreloader.Instance.materialLookup[mat] as MaterialLookup;
            if (matLookup != null)
                return matLookup.DestroyedMaterial;

            return mat; // no better destroyed material was found.
        }

        /// <summary>Reapply force to the impact object (if any) so it punches through the destroyed object.</summary>
        public static void ReapplyImpactForce(ImpactInfo info, float velocityReduction)
        {
            if (info.ImpactObject != null && !info.ImpactObject.isKinematic)
            {
                info.ImpactObject.velocity = Vector3.zero; //zero out the velocity
                info.ImpactObject.AddForce(info.ImpactObjectVelocityTo * velocityReduction, ForceMode.Impulse);
            }
        }
    }
}