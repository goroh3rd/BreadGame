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
        EditorGUILayout.PropertyField(isReverse, new GUIContent("Is Reverse"));
        if (EditorGUI.EndChangeCheck())
        {
            if (isReverse.boolValue)
            {
                returnToStartImmediately.boolValue = false;
            }
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(returnToStartImmediately, new GUIContent("Return To Start Immediately"));
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

    //public override void OnInspectorGUI()
    //{
    //    EditorGUI.BeginChangeCheck();

    //    SerializedProperty isReverse = serializedObject.FindProperty("isReverse");
    //    EditorGUILayout.PropertyField(isReverse, new GUIContent("Is Reverse"), false);
    //    SerializedProperty returnToStartImmediately = serializedObject.FindProperty("returnToStartImmediately");
    //    EditorGUILayout.PropertyField(returnToStartImmediately, new GUIContent("Return To Start Immediately"), false);

    //    if (EditorGUI.EndChangeCheck())
    //    {
    //        bool oldisReverseValue = isReverse.boolValue;
    //        bool oldreturnToStartImmediatelyValue = returnToStartImmediately.boolValue;

    //        if (oldisReverseValue != isReverse.boolValue)
    //        {
    //            if (isReverse.boolValue)
    //            {
    //                returnToStartImmediately.boolValue = false;
    //            }
    //        }
    //        else if (oldreturnToStartImmediatelyValue != returnToStartImmediately.boolValue)
    //        {
    //            if (returnToStartImmediately.boolValue)
    //            {
    //                isReverse.boolValue = false;
    //            }
    //        }
    //    }

    //    DrawDefaultInspector();
    //    EntityMover entityMover = (EntityMover)target;
    //    // オブジェクトの現在位置をWayPointに追加するボタン
    //    if (GUILayout.Button("Add Current Position to WayPoint"))
    //    {
    //        entityMover.AddCurrentPositionToWayPoint();
    //    }
    //}
}
