using UnityEngine;

public class BreadBehaviour : MonoBehaviour
{
    [SerializeField] StageManager stageManager;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D breadRb;
    [SerializeField] CircleCollider2D breadCol;
    [SerializeField] Animator breadAnimator;
    [SerializeField] Sprite smoke;
    [SerializeField] Sprite star;
    [SerializeField] private BreadImageManager imageManager;
    [SerializeField] GameObject panticle;
    [SerializeField] private BreadData data;
    [SerializeField] private float linearDrag = 4.5f;
    private bool onIce = false;
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
        this.stageManager = FindAnyObjectByType<StageManager>();
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
        if (collision.CompareTag("Ice"))
        {
            onIce = true;
            breadRb.linearDamping = 0.1f;
        }
        if (collision.CompareTag("Cooker") && manager.CheckCorrectCooker(this.data.type, collision.GetComponent<CookerBehaviour>().type))
        {
            manager.BakeBread(this);
        }
        if (collision.CompareTag("Goal"))
        {
            GoalBehaviour goal = collision.GetComponent<GoalBehaviour>();
            if (goal.Data.type == this.data.type)
            {
                goal.EnteredBreads.Add(this.breadCol);
                this.data.isGoal = true;
                if (this.data.baked) SetGoal();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // ‹““®‚ÌŠÄŽ‹‚ª•K—v
    {
        if (collision.CompareTag("Ice"))
        {
            onIce = false;
            breadRb.linearDamping = linearDrag;
        }
        if (collision.CompareTag("Goal"))
        {
            GoalBehaviour goal = collision.GetComponent<GoalBehaviour>();
            if (goal.Data.type == this.data.type)
            {
                goal.EnteredBreads.Remove(this.breadCol);
                this.data.isGoal = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!onIce)
        {
            Collider2D[] colliders = Physics2D.OverlapPointAll(this.transform.position);
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Ice"))
                {
                    onIce = true;
                    breadRb.linearDamping = 0.1f;
                    break;
                }
            }
        }
    }
    public void SetGoal()
    {
        this.data.isGoal = true;
        spriteRenderer.color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
        GameObject particle = Instantiate(this.panticle, this.transform.position, Quaternion.identity);
        particle.transform.rotation = Quaternion.Euler(-90, 0, 0);
        particle.SetActive(true);
        ParticleSystem particleSystem = particle.GetComponent<ParticleSystem>();
        particleSystem.textureSheetAnimation.SetSprite(0, star);
        particleSystem.Play();
        stageManager.CheckAllGoalCompleted();
    }
    public void UnsetGoal()
    {
        this.data.isGoal = false;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
    public void AddForce(Vector3 force)
    {
        breadRb.totalForce = Vector2.zero;
        breadRb.AddForce(force, ForceMode2D.Impulse);
    }
    public void Stop()
    {
        breadRb.linearVelocity = Vector2.zero;
        breadRb.angularVelocity = 0;
        breadRb.totalForce = Vector2.zero;
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
        if (this.data.isGoal) SetGoal();
        stageManager.CheckAllGoalCompleted();
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