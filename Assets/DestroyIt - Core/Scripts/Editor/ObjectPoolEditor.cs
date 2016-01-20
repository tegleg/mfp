using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace DestroyIt
{
    [CustomEditor(typeof(ObjectPool))]
    public class ObjectPoolEditor : Editor
    {
        private Texture deleteButton;
        private Texture lockOff;
        private Texture lockOn;
        private readonly string[] delimiter = {":|:"}; // The complex delimiter for loading/saving Object Pool to file. Should be something you won't use in your prefab names.

        private void OnEnable()
        {
            deleteButton = Resources.Load("UI_Textures/delete-16x16") as Texture;
            lockOff = Resources.Load("UI_Textures/lock-off-16x16") as Texture;
            lockOn = Resources.Load("UI_Textures/lock-on-16x16") as Texture;
        }

        public override void OnInspectorGUI()
        {
            ObjectPool objectPool = target as ObjectPool;

            List<PoolEntry> changeEntries = new List<PoolEntry>();
            if (objectPool != null && objectPool.prefabsToPool != null)
                changeEntries = objectPool.prefabsToPool.ToList();
            List<PoolEntry> removeEntries = new List<PoolEntry>();
            GUIStyle style = new GUIStyle();
            style.padding.top = 2;

            if (changeEntries.Count > 0)
            {
                EditorGUILayout.LabelField("Prefab | Count | Pooled Only");
                List<string> previouslyUsedNames = new List<string>();

                foreach(PoolEntry entry in changeEntries)
                {
                    // Remove duplicate entries
                    if (entry != null && entry.Prefab != null)
                    {
                        if (previouslyUsedNames.Contains(entry.Prefab.name))
                        {
                            Debug.LogWarning("Prefab \"" + entry.Prefab.name + "\" already exists in Object Pool (item #" + (previouslyUsedNames.IndexOf(entry.Prefab.name) + 1) + ").");
                            removeEntries.Add(entry);
                            continue;
                        }
                        previouslyUsedNames.Add(entry.Prefab.name);
                    }

                    EditorGUILayout.BeginHorizontal();

                    entry.Prefab = EditorGUILayout.ObjectField(entry.Prefab, typeof(GameObject), false) as GameObject;
                    entry.Count = EditorGUILayout.IntField(entry.Count, GUILayout.Width(20));
                    
                    Texture currentLock = lockOff;
                    if (entry.OnlyPooled)
                        currentLock = lockOn;

                    if (GUILayout.Button(currentLock, style, GUILayout.Width(16)))
                        entry.OnlyPooled = !entry.OnlyPooled;
                    
                    if (GUILayout.Button(deleteButton, style, GUILayout.Width(16)))
                        removeEntries.Add(entry); // flag for removal
                    
                    EditorGUILayout.EndHorizontal();
                }

                // Remove entries flagged for removal
                foreach (PoolEntry entry in removeEntries)
                    changeEntries.Remove(entry);
            }

            // Add entries button
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("+", EditorStyles.toolbarButton, GUILayout.Width(30)))
                changeEntries.Add(new PoolEntry{ Prefab = null, Count = 1 }); 
            EditorGUILayout.LabelField(" Add a prefab to the pool.");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            // Suppress Warnings checkbox
            EditorGUILayout.BeginHorizontal();
            objectPool.suppressWarnings = EditorGUILayout.Toggle(objectPool.suppressWarnings, GUILayout.Width(16));
            EditorGUILayout.LabelField("Suppress Warnings");
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();

            #region LOAD and SAVE
            EditorGUILayout.BeginHorizontal();
            // LOAD from file
            if (GUILayout.Button("LOAD From File", EditorStyles.toolbarButton, GUILayout.Width(110)) &&
                EditorUtility.DisplayDialog("Load Object Pool from File?", "Are you sure you want to replace the " + objectPool.prefabsToPool.Count + 
				" objects in the Object Pool with the ones from the last saved file?", "Yes", "Cancel"))
            {
                string saveFilePath = EditorApplication.currentScene.SceneFolder() + "/ObjectPool-SaveFile.txt"; //"Assets/DestroyIt - Core/ObjectPool-SaveFile.txt";
                if (File.Exists(saveFilePath))
                {
                    string[] lines = File.ReadAllLines(saveFilePath);
                    if (lines.Length > 0)
                    {
                        changeEntries = new List<PoolEntry>();
                        for (int i = 0; i < lines.Length; i++)
                        {
                            string[] parts = lines[i].Split(delimiter, StringSplitOptions.None);
                            
                            string assetPath = AssetDatabase.GUIDToAssetPath(parts[1]);
                            GameObject prefab = AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)) as GameObject;
                            int count = Convert.ToInt32(parts[2]);
                            bool onlyPooled = Convert.ToBoolean(parts[3]);
                            changeEntries.Add(new PoolEntry { Prefab = prefab, Count = count, OnlyPooled = onlyPooled});
                        }
                        Debug.Log("Object Pool loaded with " + lines.Length + " objects from file.");
                    }
                }
                else
                    Debug.Log("Object Pool file does not exist: " + saveFilePath);
            }
            EditorGUILayout.Space();
            // SAVE to file
            if (GUILayout.Button("SAVE To File", EditorStyles.toolbarButton, GUILayout.Width(110)))
            {
                string saveFilePath = EditorApplication.currentScene.SceneFolder() + "/ObjectPool-SaveFile.txt"; //"Assets/DestroyIt - Core/ObjectPool-SaveFile.txt";
                if (objectPool.prefabsToPool != null && objectPool.prefabsToPool.Count > 0)
                {
                    string[] lines = new string[objectPool.prefabsToPool.Count];
                    for (int i = 0; i < objectPool.prefabsToPool.Count; i++)
                    {
                        string assetPath = AssetDatabase.GetAssetPath(objectPool.prefabsToPool[i].Prefab.GetInstanceID());
                        string assetId = AssetDatabase.AssetPathToGUID(assetPath);
                        lines[i] = String.Format("{1}{0}{2}{0}{3}{0}{4}", string.Join("", delimiter), objectPool.prefabsToPool[i].Prefab.name, assetId, objectPool.prefabsToPool[i].Count, objectPool.prefabsToPool[i].OnlyPooled);
                    }
                    File.WriteAllLines(saveFilePath, lines);
                    Debug.Log(lines.Length + " object Pool entries saved to: " + saveFilePath + ".");
                }
                else
                    Debug.Log("Object Pool is empty. Nothing to save.");
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Separator();
            #endregion

            // Save changes back to object pool
            objectPool.prefabsToPool = changeEntries;
            if (GUI.changed)
                EditorUtility.SetDirty(objectPool);
        }
    }
}