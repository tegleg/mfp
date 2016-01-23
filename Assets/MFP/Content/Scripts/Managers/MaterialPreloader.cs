using System.Collections.Generic;
using System.Collections;
using System.Text;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Linq;

namespace DestroyIt
{
	/// <summary>
	/// This script pre-loads materials so you don't get the "blue flash" when an object is instantiated with a material the camera hasn't seen yet.
	/// This script executes every time a file is added/modified in the Unity editor.
	/// </summary>
	[ExecuteInEditMode]
	public class MaterialPreloader : MonoBehaviour
	{
		public List<Material> materials;
		public List<Texture> detailMasks; 
		public Hashtable materialLookup;
		private MaterialPreloader() { }
		
		// Here is a private reference only this class can access
		private static MaterialPreloader _instance;
	    private bool isInitialized;
		
		// This is the public reference that other classes will use
		public static MaterialPreloader Instance
		{
			get
			{
				// If _instance hasn't been set yet, we grab it from the scene.
				// This will only happen the first time this reference is used.
			    if (_instance == null)
			        CreateInstance();

			    if (!_instance.isInitialized)
			        _instance.Start();

				return _instance;
			}
		}

	    private static void CreateInstance()
	    {
            MaterialPreloader[] matPreloaders = FindObjectsOfType<MaterialPreloader>();
            if (matPreloaders.Length > 1)
                Debug.LogError("DestroyIt: Multiple MaterialPreloader scripts found in scene. There can be only one.");
            if (matPreloaders.Length == 0)
                Debug.LogError("DestroyIt: MaterialPreloader script not found in scene. This is required for DestroyIt to work properly.");

            _instance = matPreloaders[0];
	    }

		void Start()
		{
            if (isInitialized) return;

            materialLookup = new Hashtable();
			
            if (Application.isPlaying)
			{
                ReloadMaterials();
				// The maximum damage levels for materials. Default is 4. 
				// NOTE: You would have to modify the damage level shaders (as well as some DestroyIt code) to get more.
				int maxDamageLevel = 4;

				// Create damage levels of all materials in Resources folder.
				foreach (Material material in materials)
				{
					MaterialLookup matLookup = new MaterialLookup {DamagedMaterials = new List<Material>()};
					for (int i = 1; i <= maxDamageLevel; i++)
					{
						Material damagedMat = GetDamagedVersion(material, i);
						matLookup.DamagedMaterials.Add(damagedMat);
						if (i == maxDamageLevel)
							matLookup.DestroyedMaterial = damagedMat;
					}
					if(material != null)
						materialLookup.Add(material, matLookup);
				}
			}

		    isInitialized = true;
		}
		
		void Update()
		{
			if (!Application.isPlaying)
				ReloadMaterials();
		}
		
		public void ReloadMaterials()
		{
			materials = new List<Material>();
			detailMasks = new List<Texture>();
			
			#if !UNITY_WEBGL
			Object[] pMats = Resources.LoadAll("Material_Preload", typeof(ProceduralMaterial));
			foreach (Object obj in pMats)
				materials.Add((ProceduralMaterial)obj);
			#endif
			
			Object[] rMats = Resources.LoadAll("Material_Preload", typeof(Material));
			foreach (Object obj in rMats)
				materials.Add((Material)obj);
			
			Object[] dMasks = Resources.LoadAll("Material_Preload", typeof(Texture));
			foreach (Object obj in dMasks)
				detailMasks.Add((Texture)obj);
		}

        /// <summary>Creates a damage-level version of a material that uses a shader with the _DamageLevel property.</summary>
        private Material GetDamagedVersion(Material sourceMat, int version)
        {
            if (sourceMat == null)
                return null;
            Material newMaterial = null;
            if (sourceMat.HasProperty("_DamageLevel"))
            {
                newMaterial = new Material(sourceMat);
                // Legacy Pre-Unity5 DestroyIt shader
                switch (version)
                {
                    case 1:
                        newMaterial.SetFloat("_DamageLevel", 0.2f);
                        newMaterial.name = sourceMat.name + "_D1";
                        break;
                    case 2:
                        newMaterial.SetFloat("_DamageLevel", 0.4f);
                        newMaterial.name = sourceMat.name + "_D2";
                        break;
                    case 3:
                        newMaterial.SetFloat("_DamageLevel", 0.6f);
                        newMaterial.name = sourceMat.name + "_D3";
                        break;
                    case 4:
                        newMaterial.SetFloat("_DamageLevel", 0.8f);
                        newMaterial.name = sourceMat.name + "_D4";
                        break;
                    default:
                        return null;
                }
            }
            else if ((sourceMat.shader.name.ToLower() == "standard" || sourceMat.shader.name.ToLower() == "standard (specular setup)") && sourceMat.HasProperty("_DetailMask"))
            {
                // Unity 5 PBR material
                newMaterial = Object.Instantiate(sourceMat);

                // Get the name of the detail mask this shader is using.
                Texture detailMask = sourceMat.GetTexture("_DetailMask");
                if (detailMask == null)
                {
                    //Debug.LogWarning("No DetailMask texture found on shader for Progressive Damage material \"" + sourceMat.name + "\". You need to supply a DetailMask texture for progressive damage to work.");
                    return newMaterial;
                }
                List<string> stringsToRemove = new[] { "_D0", "_D1", "_D2", "_D3", "_D4" }.ToList();
                var sb = new StringBuilder(detailMask.name);
                foreach (string unwanted in stringsToRemove)
                    sb = sb.Replace(unwanted, string.Empty);

                string detailMaskBaseName = sb.ToString().Trim();
                Texture tex;

                switch (version)
                {
                    case 1:
                        tex = detailMasks.Find(x => x.name == detailMaskBaseName + "_D1");
                        if (tex == null)
                            Debug.LogWarning("[DestroyIt Progressive Damage] Could not find Detail Mask \"" + detailMaskBaseName + "_D1\" in Material Preloader. You need to put a detail mask for each damage level (0-4) in the Material Preloader component.");
                        newMaterial.SetTexture("_DetailMask", tex);
                        newMaterial.name = sourceMat.name + "_D1";
                        break;
                    case 2:
                        tex = detailMasks.Find(x => x.name == detailMaskBaseName + "_D2");
                        if (tex == null)
                            Debug.LogWarning("[DestroyIt Progressive Damage] Could not find Detail Mask \"" + detailMaskBaseName + "_D2\" in Material Preloader. You need to put a detail mask for each damage level (0-4) in the Material Preloader component.");
                        newMaterial.SetTexture("_DetailMask", tex);
                        newMaterial.name = sourceMat.name + "_D2";
                        break;
                    case 3:
                        tex = detailMasks.Find(x => x.name == detailMaskBaseName + "_D3");
                        if (tex == null)
                            Debug.LogWarning("[DestroyIt Progressive Damage] Could not find Detail Mask \"" + detailMaskBaseName + "_D3\" in Material Preloader. You need to put a detail mask for each damage level (0-4) in the Material Preloader component.");
                        newMaterial.SetTexture("_DetailMask", tex);
                        newMaterial.name = sourceMat.name + "_D3";
                        break;
                    case 4:
                        tex = detailMasks.Find(x => x.name == detailMaskBaseName + "_D4");
                        if (tex == null)
                            Debug.LogWarning("[DestroyIt Progressive Damage] Could not find Detail Mask \"" + detailMaskBaseName + "_D4\" in Material Preloader. You need to put a detail mask for each damage level (0-4) in the Material Preloader component.");
                        newMaterial.SetTexture("_DetailMask", tex);
                        newMaterial.name = sourceMat.name + "_D4";
                        break;
                    default:
                        return null;
                }
            }
            return newMaterial;
        }
	}
}