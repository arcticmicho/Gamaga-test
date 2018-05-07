using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameResources))]
public class GameResourcesEditor : Editor
{
    public GameResourcesEditor()
    {
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        EditorGUILayout.Space();
        if(GUILayout.Button("Reset Values"))
        {
            GameResources resourceIstance = (GameResources)target;
            resourceIstance.ResetValue();
        }
    }
}
