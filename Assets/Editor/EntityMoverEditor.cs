using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EntityMover))]

public class EntityMoverEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        SerializedProperty isReverse = serializedObject.FindProperty("isReverse");
        SerializedProperty returnToStartImmediately = serializedObject.FindProperty("returnToStartImmediately");

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(isReverse, new GUIContent("Reverse"));
        if (EditorGUI.EndChangeCheck())
        {
            if (isReverse.boolValue)
            {
                returnToStartImmediately.boolValue = false;
            }
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(returnToStartImmediately, new GUIContent("Return Immediately"));
        if (EditorGUI.EndChangeCheck())
        {
            if (returnToStartImmediately.boolValue)
            {
                isReverse.boolValue = false;
            }
        }

        serializedObject.ApplyModifiedProperties();

        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EntityMover entityMover = (EntityMover)target;
        if (GUILayout.Button("Add Current Position"))
        {
            entityMover.AddCurrentPositionToWayPoint();
        }
        if (GUILayout.Button("Set First Position"))
        {
            entityMover.SetFirstPosition();
        }
        EditorGUILayout.EndHorizontal();
        DrawDefaultInspector();

    }
}
