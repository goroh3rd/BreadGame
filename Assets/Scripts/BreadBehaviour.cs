using UnityEngine;

public class BreadBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D breadRb;
    [SerializeField] BoxCollider2D breadCol;
    private BreadImageManager imageManager;
    private BreadData data = new();
    private BreadManager manager;
    public void Init(BreadData data, BreadManager manager)
    {
        this.data = data;
        this.manager = manager;
        this.imageManager = FindAnyObjectByType<BreadImageManager>();
        spriteRenderer.sprite = data.baked ? imageManager.GetBakedImage(data.type) : imageManager.GetRawImage(data.type);
        Rect rect = spriteRenderer.sprite.rect;
        float pixelWidth = rect.width;
        float pixelHeight = rect.height;
        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        float width = (pixelWidth / pixelsPerUnit) * this.transform.localScale.x;
        float height = (pixelHeight / pixelsPerUnit) * this.transform.localScale.y;
        breadCol.size = new Vector3(width / this.transform.localScale.x, height / this.transform.localScale.y, 1.0f);

        //breadRb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
    }
    public void Bake()
    {
        data.baked = true;
    }
}