using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FenceColliderAdjusting))]
public class ShadowAdjusterEditor : Editor
{
    private SerializedProperty spriteRendererProp;
    private SerializedProperty capsuleColliderProp;
    private SerializedProperty shadowProp;

    private Vector2 lastSize;

    private void OnEnable()
    {
        spriteRendererProp = serializedObject.FindProperty("spriteRenderer");
        capsuleColliderProp = serializedObject.FindProperty("capsuleCollider");
        shadowProp = serializedObject.FindProperty("shadow");

        if (spriteRendererProp.objectReferenceValue is SpriteRenderer sr)
        {
            lastSize = sr.size;
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        if (spriteRendererProp.objectReferenceValue is SpriteRenderer sr)
        {
            if (sr.size != lastSize)
            {
                lastSize = sr.size;
                ((FenceColliderAdjusting)target).AdjustShadow();
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}
