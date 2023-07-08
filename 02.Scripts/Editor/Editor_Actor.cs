using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Building))]
public class Editor_Actor : Editor
{
    Building baseScript;
    

    private void OnEnable()
    {
        baseScript=base.target as Building;
        SceneView.duringSceneGui += OnSceneGUI;
    }
    
    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnSceneGUI(SceneView sv)
    {
        Handles.color = Color.red;
        Handles.DrawWireCube(baseScript.transform.position+Vector3.up,
            Vector3.one*5);

        Handles.BeginGUI();
        {
            if (GUILayout.Button("Move Right"))
            {
                baseScript.transform.position += new Vector3(1, 0, 0);
            }

            if (GUILayout.Button("Move Reft"))
            {
                baseScript.transform.position += new Vector3(-1, 0, 0);
            }
        }
        Handles.EndGUI();
    }
}
