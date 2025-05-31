using UnityEngine;

public class FenceColliderAdjusting : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private CapsuleCollider2D capsuleCollider;
    [SerializeField] private SpriteRenderer shadow; // �e��GameObject
    private void Start()
    {
        AdjustCollider();
        AdjustShadow();
    }
    private void AdjustCollider() // BoxCollider2D�̃T�C�Y�ɍ��킹��CapsuleCollider2D�̃T�C�Y�𒲐�����
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
    public void AdjustShadow()
    {
        shadow.enabled = true;
        shadow.size = spriteRenderer.size;
    }
    //private void OnValidate()
    //{
    //    if (spriteRenderer == null || capsuleCollider == null || shadow == null) return;
    //    AdjustShadow();
    //}
}
