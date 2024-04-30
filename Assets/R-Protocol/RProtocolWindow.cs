using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RProtocolWindow : EditorWindow
{
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
            // Draw the window content
            GUILayout.Label("This is a custom window.");
            GUILayout.Label("You can add more GUI elements here.");
    }
}
