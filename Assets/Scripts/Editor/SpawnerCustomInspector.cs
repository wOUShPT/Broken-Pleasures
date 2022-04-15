using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Spawner), true)]
public class SpawnerCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("pool"), new GUIContent("Pool Component"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("horizontalSize"),
            new GUIContent("Horizontal Spawn Spread"));
        serializedObject.ApplyModifiedProperties();
    }
}
