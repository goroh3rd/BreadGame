using UnityEngine;

public class BreadBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D breadRb;
    [SerializeField] CircleCollider2D breadCol;
    [SerializeField] Animator breadAnimator;
    [SerializeField] Sprite smoke;
    [SerializeField] Sprite star;
    [SerializeField] private BreadImageManager imageManager;
    [SerializeField] GameObject panticle;
    [SerializeField] private BreadData data;
    public BreadData Data => data;
    private BreadState initialState;
    private BreadManager manager;
    public void Init(BreadData data, BreadManager manager)
    {
        this.data = data;
        this.data.pos = this.transform.position;
        this.initialState = new BreadState(data, this.transform.position);
        this.manager = manager;
        this.imageManager = FindAnyObjectByType<BreadImageManager>();
        spriteRenderer.sprite = data.baked ? imageManager.GetBakedImage(data.type) : imageManager.GetRawImage(data.type);
        Rect rect = spriteRenderer.sprite.rect;
        float pixelWidth = rect.width;
        float pixelHeight = rect.height;
        float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
        float width = (pixelWidth / pixelsPerUnit) * this.transform.localScale.x;
        float height = (pixelHeight / pixelsPerUnit) * this.transform.localScale.y;
        breadCol.radius = Mathf.Max(width, height) / 2.5f;
        this.breadRb.AddForce(new Vector2(Random.Range(-0.01f, 0.01f), Random.Range(-0.01f, 0.01f)), ForceMode2D.Impulse);
        breadAnimator.Play("Poping", 0, Random.Range(0f, breadAnimator.GetCurrentAnimatorStateInfo(0).length) / breadAnimator.GetCurrentAnimatorStateInfo(0).length);
    }
    private void Start()
    {
        this.Init(this.Data, FindAnyObjectByType<BreadManager>());
        manager.AddBread(this);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cooker") && manager.CheckCorrectCooker(this.data.type, collision.GetComponent<CookerBehaviour>().type))
        {
            manager.BakeBread(this);
        }
    }
    public void SetGoal()
    {
        this.data.isGoal = true;
        GameObject particle = Instantiate(this.panticle, this.transform.position, Quaternion.identity);
        particle.transform.rotation = Quaternion.Euler(-90, 0, 0);
        particle.SetActive(true);
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.textureSheetAnimation.SetSprite(0, star);
        particleSystem.Play();
    }
    public void UnsetGoal()
    {
        this.data.isGoal = false;
    }
    public void AddForce(Vector3 force)
    {
        breadRb.AddForce(force, ForceMode2D.Impulse);
    }
    public void Bake()
    {
        if (this.data.baked) return;
        data.Bake();
        spriteRenderer.sprite = imageManager.GetBakedImage(data.type);
        GameObject panticle = Instantiate(this.panticle, this.transform.position, Quaternion.identity);
        panticle.transform.rotation = Quaternion.Euler(-90, 0, 0);
        panticle.SetActive(true);
        ParticleSystem particleSystem = panticle.GetComponent<ParticleSystem>();
        particleSystem.textureSheetAnimation.SetSprite(0, smoke);
        particleSystem.Play();
    }
    public void Unbake() // ‘½•ªŽg‚í‚È‚¢
    {
        data.Unbake();
        spriteRenderer.sprite = imageManager.GetRawImage(data.type);
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
    public void ResetBread()
    {
        this.data = initialState;
        this.spriteRenderer.sprite = data.baked ? imageManager.GetBakedImage(data.type) : imageManager.GetRawImage(data.type);
        this.transform.position = initialState.pos;
        this.breadRb.linearVelocity = Vector2.zero;
        this.Init(this.Data, FindAnyObjectByType<BreadManager>());
    }
}