using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using System;

public class MyWindow : EditorWindow
{
    private static int clickSum = 0;
    // Add menu item named "My Window" to the Window menu
    [MenuItem("Window/My Window")]
    public static void ShowWindow()
    {
        //Show existing window instance. If one doesn't exist, make one.
        GetWindow(typeof(MyWindow));
    }

    void OnGUI()
    {
        EditorGUILayout.LabelField("Count clear: "+ clickSum);
        EditorGUILayout.Space(15);
        if (EditorGUILayout.Toggle("Cahce clear", false))
        {
            GC.Collect();
            clickSum++;
        }
    }
}

