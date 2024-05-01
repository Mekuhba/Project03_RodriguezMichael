using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RProtocolWindow : EditorWindow
{
    private GameObject targetObject;
    public DefaultAsset[] folderObjects;

    // Add a tool to the Window menu
    [MenuItem("Window/R-Protocol Tool")]
    public static void ShowWindow()
    {
        // Show existing window instance. If one doesn't exist, create one.
        RProtocolWindow window = (RProtocolWindow)EditorWindow.GetWindow(typeof(RProtocolWindow));

        // Initialize the folderObjects array
        window.folderObjects = new DefaultAsset[1];
    }

    // Called when the GUI needs to be drawn
    private void OnGUI()
    {
        // Draw the field for dragging and dropping the target object
        targetObject = (GameObject)EditorGUILayout.ObjectField("Randomize Target Object", targetObject, typeof(GameObject), true);

        ////////////// Begin a horizontal layout ////////////////////
        // LABEL: RANDOMIZE AND CLEAR AND ROOT BUTTONS
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

    // Clear all root folder fields except the first one
    private void ClearFolders()
    {
        if (folderObjects.Length > 1)
        {
            folderObjects = new DefaultAsset[] { folderObjects[0] };
        }
    }

    // Add a new root folder field
    private void AddRootFolder()
    {
        DefaultAsset[] newArray = new DefaultAsset[folderObjects.Length + 1];
        folderObjects.CopyTo(newArray, 0);
        folderObjects = newArray;
    }

    // Clear existing components on the target object
    private void RandomizeSelections()
    {
        foreach (var component in targetObject.GetComponents<MonoBehaviour>())
        {
            DestroyImmediate(component);
        }

        // Iterate over each folder object
        foreach (var folderObject in folderObjects)
        {
            // Get the path of the folder
            string folderPath = AssetDatabase.GetAssetPath(folderObject);

            ///////////////////////// SECTION FOR ADDING SCRIPTS //////////////////////////////
            // Find all script files within a folder
            string[] scriptGUIDs = AssetDatabase.FindAssets("t:MonoScript", new[] { folderPath });

            //select one script randomly and add it as a component to target object
            if (scriptGUIDs.Length > 0)
            {
                string randomScriptGUID = scriptGUIDs[Random.Range(0, scriptGUIDs.Length)];
                string randomScriptPath = AssetDatabase.GUIDToAssetPath(randomScriptGUID);
                var randomScript = AssetDatabase.LoadAssetAtPath<MonoScript>(randomScriptPath);
                targetObject.AddComponent(randomScript.GetClass());
            }
            ///////////////////////// SECTION END ////////////////////////////////////////////

            //////////////////////// SECTION FOR ADDING MATERIALS ////////////////////////////
            // Find all material files within a folder
            string[] materialGUIDs = AssetDatabase.FindAssets("t:Material", new[] { folderPath });

            // If there are material files in a folder, select one randomly and apply it to the target object
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
            /////////////////////// SECTION END ///////////////////////////////////////////////
            
            ////////////////////// ADD ADDITIONAL SECTION FOR OTHER ITEM TYPES ///////////////
            
            ///////////////////// SECTION END ////////////////////////////////////////////////
        }
    }
}