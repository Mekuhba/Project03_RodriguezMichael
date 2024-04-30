using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RProtocolWindow : EditorWindow
{
    private GameObject targetObject;
    public DefaultAsset folderObject;

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
            // Add your randomization logic here
            Debug.Log("Randomize button clicked.");
        }

        // Draw the "Clear Selections" button
        if (GUILayout.Button("Clear"))
        {
            // Add your logic to clear selections here
            Debug.Log("Clear Selections button clicked.");
        }

        // Draw the "Clear Selections" button
        if (GUILayout.Button("Add Root"))
        {
            // Add your logic to clear selections here
            Debug.Log("Clear Selections button clicked.");
        }

        GUILayout.EndHorizontal();
        ///////////// End the horizontal layout /////////////////////

        // Draw the first root field for dragging and dropping a target folder
        folderObject = (DefaultAsset)EditorGUILayout.ObjectField("Root Folder", folderObject, typeof(DefaultAsset), true);
    }
}
