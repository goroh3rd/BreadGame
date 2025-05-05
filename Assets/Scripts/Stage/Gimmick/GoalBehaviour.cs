using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GoalBehaviour : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] private BreadManager manager;
    [SerializeField] private BreadImageManager imageManager;
    [SerializeField] private BoxCollider2D goalCol;
    [SerializeField] private ClearWindowBehaviour clearWindow;
    [SerializeField] GoalData data = new();
    public GoalData Data => data;
    [SerializeField] private GoalOpenDirection openDirection;
    [SerializeField] List<GoalFenceAdjusting> fences;
    [SerializeField] private Vector3 fenceImageScale;
    [SerializeField] private GameObject breadImage;
    [SerializeField] private Vector3 breadImageScale;
    [SerializeField] private SpriteRenderer breadImageRenderer;
    private enum GoalOpenDirection { Top, Bottom, Left, Right }
    private List<Collider2D> enteredBreads = new();
    public List<Collider2D> EnteredBreads => enteredBreads;
    [System.Serializable]
    public class GoalData
    {
        public BreadType type;
        public int count;
        public GoalData(BreadType type)
        {
            this.type = type;
        }
        public GoalData()
        {
            this.type = BreadType.test;
        }
    }
    private void Start()
    {
        this.Init();
    }
    public void Init()
    {
        this.stageManager = FindAnyObjectByType<StageManager>();
        this.manager = FindAnyObjectByType<BreadManager>();
        this.clearWindow = FindAnyObjectByType<ClearWindowBehaviour>();
        //fences.ForEach(f => f.Adjust(this.transform.position));
        //fences.Single(f => (int)f.placement == (int)openDirection).gameObject.SetActive(false);
        AdjusBreadImage();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!collision.CompareTag("Pan")) return;
    //    BreadBehaviour bread = collision.GetComponent<BreadBehaviour>();
    //    if (!bread.Data.baked) return;
    //    enteredBreads.Add(collision);
    //    if (bread.Data.type == this.data.type)
    //    {
    //        bread.SetGoal();
    //    }
    //    if (stageManager.CheckAllConteins(this))
    //    {
    //        Debug.Log("Goal Completed");
    //        if (manager.Breads.All(b => b.Data.isGoal))
    //        {
    //            stageManager.StageCompleted();
    //            StartCoroutine(stageManager.GoalCompleted(0.8f));
    //        }
    //    }
    //}
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Pan")) return;
        enteredBreads.Remove(collision);
        BreadBehaviour bread = collision.GetComponent<BreadBehaviour>();
        if (bread.Data.type == this.data.type)
        {
            bread.UnsetGoal();
        }
    }
    private void AdjusBreadImage()
    {
        this.imageManager = FindAnyObjectByType<BreadImageManager>();
        Vector3 scale = Vector3.zero;
        scale.x = breadImageScale.x / this.breadImage.transform.parent.lossyScale.x;
        scale.y = breadImageScale.y / this.breadImage.transform.parent.lossyScale.y;
        scale.z = 1;
        this.breadImage.transform.localScale = scale;
        this.breadImageRenderer.sprite = this.imageManager.GetBakedImage(this.data.type);
    }
    [ContextMenu("Test")]
    public void Test()
    {
        this.data = new GoalData((BreadType)1);
    }
}