using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RProtocolWindow : EditorWindow
{
    private GameObject targetObject;
    public DefaultAsset[] folderObjects;

    // Add a menu item named "Custom Tool" to the Window menu
    [MenuItem("Window/R-Protocol Tool")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, create one.
        EditorWindow.GetWindow(typeof(RProtocolWindow));
    }

    // Called when the GUI needs to be drawn
    private void OnGUI()
    {
        // Draw the field for dragging and dropping the target object
        targetObject = (GameObject)EditorGUILayout.ObjectField("Randomize Target Object", targetObject, typeof(GameObject), true);

        ////////////// Begin a horizontal layout ////////////////////
        // LABEL: RANDOMIZE AND CLEAR SELECTIONS BUTTONS
        GUILayout.BeginHorizontal();

        // Draw the "Randomize" button
        if (GUILayout.Button("Randomize"))
        {
            RandomizeSelections();
        }

        // Draw the "Clear Selections" button
        if (GUILayout.Button("Clear"))
        {
            ClearFolders();
        }

        // Draw the "Add Root" button
        if (GUILayout.Button("Add Root"))
        {
            AddRootFolder();
        }

        GUILayout.EndHorizontal();
        ///////////// End the horizontal layout /////////////////////

        // Draw the root folder fields
        for (int i = 0; i < folderObjects.Length; i++)
        {
            folderObjects[i] = (DefaultAsset)EditorGUILayout.ObjectField("Root Folder", folderObjects[i], typeof(DefaultAsset), true);
        }
    }

    private void ClearFolders()
    {
        // Clear all root folder fields except the first one
        if (folderObjects.Length > 1)
        {
            folderObjects = new DefaultAsset[] { folderObjects[0] };
        }
    }

    private void AddRootFolder()
    {
        // Add a new root folder field
        DefaultAsset[] newArray = new DefaultAsset[folderObjects.Length + 1];
        folderObjects.CopyTo(newArray, 0);
        folderObjects = newArray;
    }

    private void RandomizeSelections()
    {
        // Clear existing components on the target object
        foreach (var component in targetObject.GetComponents<MonoBehaviour>())
        {
            DestroyImmediate(component);
        }

        // Iterate over each folder object
        foreach (var folderObject in folderObjects)
        {
            // Get the path of the folder
            string folderPath = AssetDatabase.GetAssetPath(folderObject);

            // Find all script files within the folder
            string[] scriptGUIDs = AssetDatabase.FindAssets("t:MonoScript", new[] { folderPath });

            // If there are script files in the folder, select one randomly and add it as a component
            if (scriptGUIDs.Length > 0)
            {
                string randomScriptGUID = scriptGUIDs[Random.Range(0, scriptGUIDs.Length)];
                string randomScriptPath = AssetDatabase.GUIDToAssetPath(randomScriptGUID);
                var randomScript = AssetDatabase.LoadAssetAtPath<MonoScript>(randomScriptPath);
                targetObject.AddComponent(randomScript.GetClass());
            }

            // Find all material files within the folder
            string[] materialGUIDs = AssetDatabase.FindAssets("t:Material", new[] { folderPath });

            // If there are material files in the folder, select one randomly and apply it to the target object
            if (materialGUIDs.Length > 0)
            {
                string randomMaterialGUID = materialGUIDs[Random.Range(0, materialGUIDs.Length)];
                string randomMaterialPath = AssetDatabase.GUIDToAssetPath(randomMaterialGUID);
                var randomMaterial = AssetDatabase.LoadAssetAtPath<Material>(randomMaterialPath);
                var renderer = targetObject.GetComponent<Renderer>();

                if (renderer != null)
                {
                    renderer.sharedMaterial = randomMaterial;
                }
                else
                {
                    Debug.LogWarning("Target object does not have a Renderer component.");
                }
            }
        }
    }
}