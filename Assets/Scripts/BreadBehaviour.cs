using UnityEngine;

public class BreadBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D breadRb;
    [SerializeField] CircleCollider2D breadCol;
    private BreadImageManager imageManager;
    private BreadData data;
    public BreadData Data => data;
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
        breadCol.radius = width / 3;
        this.breadRb.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f)), ForceMode2D.Impulse);

        //breadRb.AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
    }
    public void AddForce(Vector3 force)
    {
        breadRb.AddForce(force, ForceMode2D.Impulse);
    }
    public void Bake()
    {
        data.baked = true;
    }
    public void Grabbed(Vector3 pos, GrabType type)
    {
        this.transform.position = pos;
        this.data.grabType = type;
    }
    public void Released()
    {
        this.data.grabType = GrabType.Released;
    }
}