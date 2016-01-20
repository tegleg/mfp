using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace DestroyIt
{
    [CustomEditor(typeof(Destructible)), CanEditMultipleObjects]
    public class DestructibleEditor : Editor
    {
        private GameObject previousDestroyedPrefab;
        private SerializedProperty fallbackParticle;
        private SerializedProperty totalHitPoints;
        private SerializedProperty currentHitPoints;
        private SerializedProperty canBeDestroyed;
        private SerializedProperty canBeObliterated;
        private SerializedProperty isDebrisChipAway;
        private SerializedProperty chipAwayDebrisMass;
        private SerializedProperty chipAwayDebrisDrag;
        private SerializedProperty chipAwayDebrisAngularDrag;
        private SerializedProperty tagDebrisColliders;
        private SerializedProperty tagDebrisCollidersWith;
        private SerializedProperty autoPoolDestroyedPrefab;
        private SerializedProperty useFallbackParticle;
        private SerializedProperty sinkWhenDestroyed;

        private void OnEnable()
        {
            fallbackParticle = serializedObject.FindProperty("fallbackParticle");
            totalHitPoints = serializedObject.FindProperty("totalHitPoints");
            currentHitPoints = serializedObject.FindProperty("currentHitPoints");
            canBeDestroyed = serializedObject.FindProperty("canBeDestroyed");
            canBeObliterated = serializedObject.FindProperty("canBeObliterated");
            isDebrisChipAway = serializedObject.FindProperty("isDebrisChipAway");
            chipAwayDebrisMass = serializedObject.FindProperty("chipAwayDebrisMass");
            chipAwayDebrisDrag = serializedObject.FindProperty("chipAwayDebrisDrag");
            chipAwayDebrisAngularDrag = serializedObject.FindProperty("chipAwayDebrisAngularDrag");
            tagDebrisColliders = serializedObject.FindProperty("tagDebrisColliders");
            tagDebrisCollidersWith = serializedObject.FindProperty("tagDebrisCollidersWith");
            autoPoolDestroyedPrefab = serializedObject.FindProperty("autoPoolDestroyedPrefab");
            useFallbackParticle = serializedObject.FindProperty("useFallbackParticle");
            sinkWhenDestroyed = serializedObject.FindProperty("sinkWhenDestroyed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Destructible destructible = target as Destructible;
            List<string> materialOptions = new List<string>();
            List<string> debrisOptions = new List<string>();
            List<string> destructibleChildrenOptions = new List<string>();

            // TOTAL HIT POINTS
            EditorGUILayout.PropertyField(totalHitPoints, new GUIContent("Total Hit Points"));
            if (totalHitPoints.intValue < 0)
                totalHitPoints.intValue = 0;
            if (totalHitPoints.intValue != destructible.totalHitPoints) // value has changed, so change the current hit points to match.
                currentHitPoints.intValue = totalHitPoints.intValue;
            destructible.totalHitPoints = totalHitPoints.intValue;

            // CURRENT HIT POINTS
            EditorGUILayout.PropertyField(currentHitPoints, new GUIContent("Current Hit Points"));
            if (currentHitPoints.intValue > destructible.totalHitPoints)
                currentHitPoints.intValue = destructible.totalHitPoints;
            if (currentHitPoints.intValue < 0)
                currentHitPoints.intValue = 0;
            destructible.currentHitPoints = currentHitPoints.intValue;

            // VELOCITY REDUCTION
            destructible.velocityReduction = EditorGUILayout.Slider("Velocity Reduction", destructible.velocityReduction, 0f, 1f);

            #region DESTROYED PREFAB
            // DESTROYED PREFAB
            EditorGUILayout.Separator();
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Separator();
            destructible.destroyedPrefab = EditorGUILayout.ObjectField("Destroyed Prefab", destructible.destroyedPrefab, typeof(GameObject), false) as GameObject;

            if (previousDestroyedPrefab == null)
                previousDestroyedPrefab = destructible.destroyedPrefab;

            if (destructible.destroyedPrefab != null)
            {
                #region REPLACE MATERIALS ON DESTROYED PREFAB
                // REPLACE MATERIALS ON DESTROYED PREFAB
                //Get string array of material names on destroyed prefab
                List<MeshRenderer> destroyedMeshes = destructible.destroyedPrefab.GetComponentsInChildren<MeshRenderer>(true).ToList();
                List<Material> destroyedMaterials = new List<Material>();
                foreach (MeshRenderer mesh in destroyedMeshes)
                {
                    foreach (Material mat in mesh.sharedMaterials)
                    {
                        if (mat != null && !destroyedMaterials.Contains(mat))
                        {
                            destroyedMaterials.Add(mat);
                            materialOptions.Add(mat.name);
                        }
                    }
                }

                // Initialize replaceMaterials if null
                if (destructible.replaceMaterials == null)
                    destructible.replaceMaterials = new List<MaterialMapping>();

                // Clean up replaceMaterials
                if (destructible.replaceMaterials.Count > 0)
                    destructible.replaceMaterials.RemoveAll(x => x.SourceMaterial != null && !materialOptions.Contains(x.SourceMaterial.name));

                EditorGUILayout.LabelField("Replace Materials:");
                if (destroyedMaterials.Count > 0)
                {
                    destroyedMaterials = destroyedMaterials.OrderBy(x => x.name).ToList();
                    materialOptions.Sort();
                    foreach (MaterialMapping mapping in destructible.replaceMaterials)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.Width(15));
                        EditorGUILayout.LabelField("Replace", GUILayout.Width(50));
                        int selectedMaterialIndex = mapping.SourceMaterial == null ? 0 : destroyedMaterials.IndexOf(mapping.SourceMaterial);

                        selectedMaterialIndex = EditorGUILayout.Popup(selectedMaterialIndex, materialOptions.ToArray());
                        if (selectedMaterialIndex >= 0 && selectedMaterialIndex < materialOptions.Count)
                        {
                            string materialName = materialOptions[selectedMaterialIndex];
                            mapping.SourceMaterial = destroyedMaterials.First(x => x.name == materialName);
                        }
                        else
                            mapping.SourceMaterial = null;

                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("", GUILayout.Width(15));
                        EditorGUILayout.LabelField("With", GUILayout.Width(50));
                        mapping.ReplacementMaterial = EditorGUILayout.ObjectField(mapping.ReplacementMaterial, typeof(Material), false) as Material;
                        EditorGUILayout.EndHorizontal();
                    }
                    // Add/Remove buttons
                    bool showAddButton = destructible.replaceMaterials.Count < materialOptions.Count;
                    bool showRemoveButton = destructible.replaceMaterials.Count > 0;
                    CreateButtons(destructible.replaceMaterials, showAddButton, showRemoveButton);
                }
                else
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(15));
                    EditorGUILayout.LabelField("No materials found on prefab!", GUILayout.Width(200), GUILayout.Height(16));
                    EditorGUILayout.EndHorizontal();
                }
                EditorGUILayout.Separator();
                #endregion

                #region DEBRIS TO RE-PARENT
                // DEBRIS TO RE-ATTACH TO PARENT
                List<Transform> debrisObjects = destructible.destroyedPrefab.GetComponentsInChildrenOnly<Transform>(true);
                if (debrisObjects.Count > 0)
                {
                    EditorGUILayout.LabelField("Debris to Re-Parent:");

                    foreach (Transform trans in debrisObjects)
                        debrisOptions.Add(trans.name);

                    debrisOptions = debrisOptions.Distinct().ToList();

                    // Initialize DebrisToReparentByName
                    if (destructible.debrisToReParentByName == null)
                        destructible.debrisToReParentByName = new List<string>();

                    if (destructible.debrisToReParentByName.Count > 0)
                    {
                        debrisOptions.Sort();
                        for (int i = 0; i < destructible.debrisToReParentByName.Count; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("", GUILayout.Width(15));
                            EditorGUILayout.LabelField("Name", GUILayout.Width(50));
                            int currentSelectedIndex = debrisOptions.FindIndex(m => m == destructible.debrisToReParentByName[i]);
                            int selectedDebrisIndex = EditorGUILayout.Popup(currentSelectedIndex, debrisOptions.ToArray());
                            if (selectedDebrisIndex >= 0 && selectedDebrisIndex < debrisOptions.Count)
                                destructible.debrisToReParentByName[i] = debrisOptions[selectedDebrisIndex];
                            else
                                destructible.debrisToReParentByName[i] = debrisOptions[0];

                            EditorGUILayout.EndHorizontal();
                        }
                    }
                    // Add/Remove buttons
                    bool showReparentAddButton = destructible.debrisToReParentByName.Count < debrisOptions.Count;
                    bool showReparentRemoveButton = destructible.debrisToReParentByName.Count > 0;
                    CreateButtons(destructible.debrisToReParentByName, showReparentAddButton, showReparentRemoveButton);
                }
                #endregion

                #region RE-PARENT CHILDREN TO DESTROYED PREFAB
                //RE-PARENT CHILDREN TO DESTROYED PREFAB
                List<Transform> destructibleChildren = destructible.gameObject.GetComponentsInChildrenOnly<Transform>(true);
                if (destructibleChildren.Count > 0)
                {
                    EditorGUILayout.LabelField("Re-Parent to Destroyed Prefab:");

                    foreach (Transform trans in destructibleChildren)
                        destructibleChildrenOptions.Add(trans.name);

                    destructibleChildrenOptions = destructibleChildrenOptions.Distinct().ToList();

                    // Initialize ChildrenToReparentByName
                    if (destructible.childrenToReParentByName == null)
                        destructible.childrenToReParentByName = new List<string>();

                    if (destructible.childrenToReParentByName.Count > 0)
                    {
                        destructibleChildrenOptions.Sort();
                        for (int i = 0; i < destructible.childrenToReParentByName.Count; i++)
                        {
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField("", GUILayout.Width(15));
                            EditorGUILayout.LabelField("Name", GUILayout.Width(50));
                            int currentSelectedIndex = destructibleChildrenOptions.FindIndex(m => m == destructible.childrenToReParentByName[i]);
                            int selectedChildIndex = EditorGUILayout.Popup(currentSelectedIndex, destructibleChildrenOptions.ToArray());
                            if (selectedChildIndex >= 0 && selectedChildIndex < destructibleChildrenOptions.Count)
                                destructible.childrenToReParentByName[i] = destructibleChildrenOptions[selectedChildIndex];
                            else
                                destructible.childrenToReParentByName[i] = destructibleChildrenOptions[0];

                            EditorGUILayout.EndHorizontal();
                        }
                    }

                    // Add/Remove buttons
                    bool showChildReparentAddButton = destructible.childrenToReParentByName.Count < destructibleChildrenOptions.Count;
                    bool showChildReparentRemoveButton = destructible.childrenToReParentByName.Count > 0;
                    CreateButtons(destructible.childrenToReParentByName, showChildReparentAddButton, showChildReparentRemoveButton);
                }
                #endregion

                #region CHIP-AWAY DEBRIS
                //CHIP-AWAY DEBRIS
                isDebrisChipAway.boolValue = EditorGUILayout.Toggle("Chip-Away Debris", isDebrisChipAway.boolValue);
                destructible.isDebrisChipAway = isDebrisChipAway.boolValue;
                if (destructible.isDebrisChipAway)
                {
                    // CHIP-AWAY DEBRIS MASS
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(15));
                    chipAwayDebrisMass.floatValue = EditorGUILayout.FloatField("Debris Mass", chipAwayDebrisMass.floatValue);
                    destructible.chipAwayDebrisMass = chipAwayDebrisMass.floatValue;
                    EditorGUILayout.EndHorizontal();

                    // CHIP-AWAY DEBRIS DRAG
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(15));
                    chipAwayDebrisDrag.floatValue = EditorGUILayout.FloatField("Debris Drag", chipAwayDebrisDrag.floatValue);
                    destructible.chipAwayDebrisDrag = chipAwayDebrisDrag.floatValue;
                    EditorGUILayout.EndHorizontal();

                    // CHIP-AWAY DEBRIS ANGULARDRAG
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(15));
                    chipAwayDebrisAngularDrag.floatValue = EditorGUILayout.FloatField("Debris Angular Drag", chipAwayDebrisAngularDrag.floatValue);
                    destructible.chipAwayDebrisAngularDrag = chipAwayDebrisAngularDrag.floatValue;
                    EditorGUILayout.EndHorizontal();
                }
                #endregion

                // AUTO-POOL DESTROYED PREFAB
                autoPoolDestroyedPrefab.boolValue = EditorGUILayout.Toggle("Auto Pool", autoPoolDestroyedPrefab.boolValue);
                destructible.autoPoolDestroyedPrefab = autoPoolDestroyedPrefab.boolValue;

                // TAG DEBRIS COLLIDERS
                EditorGUILayout.Separator();
                EditorGUILayout.BeginHorizontal();
                tagDebrisColliders.boolValue = EditorGUILayout.Toggle(tagDebrisColliders.boolValue, GUILayout.Width(15));
                EditorGUILayout.LabelField("Tag All Debris As:", GUILayout.Width(100));
                List<string> tagOptions = Enum.GetNames(typeof(Tag)).ToList();
                tagOptions.Sort();
                int selectedIndex = tagOptions.IndexOf(Enum.GetName(typeof(Tag), tagDebrisCollidersWith.intValue));
                selectedIndex = EditorGUILayout.Popup(selectedIndex, tagOptions.ToArray());
                Tag newTag = (Tag)Enum.Parse(typeof(Tag), tagOptions[selectedIndex]);
                tagDebrisCollidersWith.intValue = (int)newTag;
                EditorGUILayout.EndHorizontal();

                previousDestroyedPrefab = destructible.destroyedPrefab;
            }
            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();
            #endregion

            #region PARTICLES
            EditorGUILayout.BeginVertical("Box");
            EditorGUILayout.Separator();

            // FALLBACK PARTICLE
            EditorGUILayout.BeginHorizontal();
            useFallbackParticle.boolValue = EditorGUILayout.Toggle("", useFallbackParticle.boolValue, GUILayout.Width(15));
            EditorGUILayout.LabelField("Use Fallback Particle:");
            destructible.useFallbackParticle = useFallbackParticle.boolValue;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(15));
            EditorGUILayout.PropertyField(fallbackParticle, new GUIContent(""));
            destructible.fallbackParticle = fallbackParticle.objectReferenceValue as ParticleSystem;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();

            // DAMAGE-LEVEL PARTICLES
            if (destructible.damageLevelParticles == null)
                destructible.damageLevelParticles = new List<DamageEffect>();

            EditorGUILayout.LabelField("Damage Effects:");
            foreach (DamageEffect damageParticle in destructible.damageLevelParticles)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Effect", GUILayout.Width(60));
                damageParticle.Effect = EditorGUILayout.ObjectField(damageParticle.Effect, typeof(GameObject), false) as GameObject;
                EditorGUILayout.EndHorizontal();
               
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("At Damage Level", GUILayout.Width(110));
                if (damageParticle.TriggeredAt < 1 || damageParticle.TriggeredAt > 5)
                    damageParticle.TriggeredAt = 5;
                damageParticle.TriggeredAt = EditorGUILayout.Popup(damageParticle.TriggeredAt - 1, new[] { "20%", "40%", "60%", "80%", "Destroyed" }) + 1;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Offset", GUILayout.Width(40));
                damageParticle.Offset = EditorGUILayout.Vector3Field("", damageParticle.Offset, GUILayout.Width(142), GUILayout.Height(18));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Rotate", GUILayout.Width(40));
                damageParticle.Rotation = EditorGUILayout.Vector3Field("", damageParticle.Rotation, GUILayout.Width(142), GUILayout.Height(18));
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                damageParticle.UseDependency = EditorGUILayout.Toggle(damageParticle.UseDependency, GUILayout.Width(15));
                EditorGUILayout.LabelField("Only If Tagged", GUILayout.Width(95));

                damageParticle.TagDependency = (Tag)EditorGUILayout.EnumPopup(damageParticle.TagDependency);
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space();
                EditorGUILayout.EndVertical();
            }
            // Add/Remove buttons
            EditorGUILayout.Space();
            bool showRemoveParticleButton = destructible.damageLevelParticles.Count > 0;
            CreateButtons(destructible.damageLevelParticles, true, showRemoveParticleButton);

            EditorGUILayout.Separator();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Separator();
            #endregion

            #region UNPARENT ON DESTROY
            // UNPARENT ON DESTROY
            List<Transform> children = destructible.gameObject.GetComponentsInChildrenOnly<Transform>();
            List<string> childrenOptions = children.Select(x => String.Format("{0} [{1}]", x.name, x.GetInstanceID())).ToList();

            if (children.Count > 0)
            {
                EditorGUILayout.BeginVertical("Box");
                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Children To Un-Parent When Destroyed:");
                List<GameObject> tempList = new List<GameObject>();
                if (destructible.unparentOnDestroy == null)
                    destructible.unparentOnDestroy = new List<GameObject>();

                foreach (GameObject go in destructible.unparentOnDestroy)
                {
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField("", GUILayout.Width(15));
                    EditorGUILayout.LabelField("Child", GUILayout.Width(50));

                    int selectedchildIndex;
                    if (go == null)
                        selectedchildIndex = 0;
                    else
                        selectedchildIndex = children.IndexOf(go.transform);

                    selectedchildIndex = EditorGUILayout.Popup(selectedchildIndex, childrenOptions.ToArray());
                    GameObject selectedChild = null;
                    if (selectedchildIndex >= 0 && selectedchildIndex < childrenOptions.Count)
                    {
                        string childName = childrenOptions[selectedchildIndex];
                        string pattern = @"\[-*[0-9]+\]";
                        string match = Regex.Match(childName, pattern).Value;
                        int instanceId = Convert.ToInt32(match.Trim('[', ']'));
                        selectedChild = children.First(x => x.GetInstanceID() == instanceId).gameObject;
                    }
                    
                    if (selectedChild != null)
                        tempList.Add(selectedChild);
                    
                    EditorGUILayout.EndHorizontal();
                }
                destructible.unparentOnDestroy = tempList;

                // Add/Remove buttons
                bool showAddButton = destructible.unparentOnDestroy.Count < children.Count;
                bool showRemoveButton = destructible.unparentOnDestroy.Count > 0;
                CreateButtons(destructible.unparentOnDestroy, showAddButton, showRemoveButton);
                EditorGUILayout.Separator();
                EditorGUILayout.EndVertical();
            }
            #endregion

            // DEBUG MONITOR
            destructible.debugMonitor = EditorGUILayout.Toggle("Debug Monitor", destructible.debugMonitor);

            // CAN BE DESTROYED
            canBeDestroyed.boolValue = EditorGUILayout.Toggle("Can Be Destroyed", canBeDestroyed.boolValue);
            destructible.canBeDestroyed = canBeDestroyed.boolValue;

            // CAN BE OBLITERATED (should only be available when CanBeDestroyed is true.)
            if (destructible.canBeDestroyed)
            {
                canBeObliterated.boolValue = EditorGUILayout.Toggle("Can Be Obliterated", canBeObliterated.boolValue);
                destructible.canBeObliterated = canBeObliterated.boolValue;
            }

            // SINK INTO GROUND INSTEAD OF DESTROYING INTO DEBRIS
            sinkWhenDestroyed.boolValue = EditorGUILayout.Toggle("Sink Into Ground", sinkWhenDestroyed.boolValue);
            destructible.sinkWhenDestroyed = sinkWhenDestroyed.boolValue;

            if (GUI.changed)
                EditorUtility.SetDirty(destructible);
            serializedObject.ApplyModifiedProperties();
        }

        private void CreateButtons<T>(List<T> itemList, bool showAddButton, bool showRemoveButton)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("", GUILayout.Width(15));

            if (showAddButton && GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(30)))
                itemList.Add(default(T));

            if (showRemoveButton && GUILayout.Button("-", EditorStyles.toolbarButton, GUILayout.Width(30)))
                itemList.RemoveAt(itemList.Count - 1);

            EditorGUILayout.EndHorizontal();
        }

        
    }
}