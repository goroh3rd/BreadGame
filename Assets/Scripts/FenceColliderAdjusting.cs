using UnityEngine;

public class FenceColliderAdjusting : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    private void Start()
    {
        AdjustCollider();
    }
    private void AdjustCollider() // BoxCollider2Dのサイズに合わせてCapsuleCollider2Dのサイズを調整する
    {
        if (spriteRenderer == null || capsuleCollider == null) return;
        if (spriteRenderer.size.x > spriteRenderer.size.y)
        {
            capsuleCollider.direction = CapsuleDirection2D.Horizontal;
        }
        else
        {
            capsuleCollider.direction = CapsuleDirection2D.Vertical;
        }
        capsuleCollider.enabled = true;
        capsuleCollider.size = new Vector2(spriteRenderer.size.x, spriteRenderer.size.y);
    }
}
