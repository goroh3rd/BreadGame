using UnityEngine;

public class BreadBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D breadRb;
    [SerializeField] CircleCollider2D breadCol;
    [SerializeField] Animator breadAnimator;
    private BreadImageManager imageManager;
    [SerializeField] private BreadData data;
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
        breadCol.radius = width / 2.5f;
        this.breadRb.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f)), ForceMode2D.Impulse);
        breadAnimator.Play("Poping", 0, Random.Range(0f, breadAnimator.GetCurrentAnimatorStateInfo(0).length) / breadAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
    public void AddForce(Vector3 force)
    {
        breadRb.AddForce(force, ForceMode2D.Impulse);
    }
    public void Bake()
    {
        data.baked = true;
        this.data.baked = true;
        spriteRenderer.sprite = imageManager.GetBakedImage(data.type);
    }
    public void Grabbed(Vector3 pos, GrabType type)
    {
        this.transform.position = pos;
        this.data.grabType = type;
        breadCol.enabled = false;
        spriteRenderer.sortingOrder = 1;
        spriteRenderer.color = new Color(0.7f, 0.7f, 0.7f, 1);
        breadAnimator.speed = 0;
    }
    public void Released()
    {
        this.data.grabType = GrabType.Released;
        breadCol.enabled = true;
        spriteRenderer.sortingOrder = 0;
        spriteRenderer.color = new Color(1, 1, 1, 1);
        breadAnimator.speed = 1;
    }
}