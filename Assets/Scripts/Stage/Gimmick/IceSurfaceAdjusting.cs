using UnityEngine;

public class IceSurfaceAdjusting : MonoBehaviour
{
    [SerializeField] private BoxCollider2D col;
    [SerializeField] private SpriteRenderer spriteRenderer; // Ç±Ç±Ç≈SpriteRendererÇéQè∆Ç∑ÇÈ
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Adjust();
    }
    //private void OnValidate()
    //{
    //    if (iceTransform == null || surfaceRenderer == null)
    //    {
    //        return;
    //    }
    //    Adjust();
    //}
    private void Adjust()
    {
        col.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }
}
