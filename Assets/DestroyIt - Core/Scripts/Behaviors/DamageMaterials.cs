using UnityEngine;

namespace DestroyIt
{
    /// <summary>
    /// This script was designed to be placed automatically on game objects with meshes.
    /// It will keep up with the damage-level materials so you can simply call SetDamageLevel() from your calling process.
    /// </summary>
    public class DamageMaterials : MonoBehaviour
    {
        public DamageLevelMaterials[] damageLevelMaterials;
        private MeshRenderer meshRenderer;
        private bool isInitialized;

        void Start()
        {
            if (!isInitialized)
                Initialize();
        }

        void Initialize()
        {
            damageLevelMaterials = new DamageLevelMaterials[4];
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                Debug.Log(gameObject.name + ": No MeshRenderer found on this gameobject. This script needs a MeshRenderer to function.");
                Destroy(this);
                return;
            }

            AssignMaterials();
            isInitialized = true;
        }

        private void AssignMaterials()
        {
            if (meshRenderer.sharedMaterials.Length <= 0)
            {
                Debug.Log(gameObject.name + ": No Materials found on the MeshRenderer for this gameobject. This script needs Materials to function.");
                Destroy(this);
                return;
            }

            for (int i=0; i<4; i++)
            {
                damageLevelMaterials[i] = new DamageLevelMaterials {Materials = new Material[meshRenderer.sharedMaterials.Length]};
                for (int j = 0; j < meshRenderer.sharedMaterials.Length; j++)
                {
                    damageLevelMaterials[i].Materials[j] = GetDamagedMaterial(meshRenderer.sharedMaterials[j], i + 1);
                }
            }
        }

        public void SetDamageLevel(int damageLevel)
        {
            if (!isInitialized)
                Initialize();

            meshRenderer.materials = damageLevelMaterials[damageLevel-1].Materials;    
        }

        private static Material GetDamagedMaterial(Material mat, int damageLevel)
        {
            if (mat == null) return null;
            if (damageLevel < 1 || damageLevel > 4) return null;

            if (MaterialPreloader.Instance == null) return mat;

            MaterialLookup matLookup = MaterialPreloader.Instance.materialLookup[mat] as MaterialLookup;
            if (matLookup != null)
                return matLookup.DamagedMaterials[damageLevel - 1];

            return mat; // no better destroyed material was found.
        }
    }
}