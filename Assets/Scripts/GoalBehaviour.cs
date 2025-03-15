using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GoalBehaviour : MonoBehaviour
{
    [SerializeField] private BreadManager manager;
    [SerializeField] private BoxCollider2D goalCol;
    [SerializeField] GoalData data;
    [System.Serializable]
    public class GoalData
    {
        public BreadType type;
        public int count;
        public bool isCompleted;
        public GoalData(BreadType type)
        {
            this.type = type;
        }
        public GoalData()
        {
            this.type = BreadType.test;
        }
    }
    [SerializeField] private GoalOpenDirection openDirection;
    private enum GoalOpenDirection { Top, Bottom, Left, Right }
    [SerializeField] List<GoalFenceAdjusting> fences;
    public GoalData Data => data;
    private List<Collider2D> enteredBreads = new();
    private void Start()
    {
        this.manager = FindAnyObjectByType<BreadManager>();
        this.data = new GoalData();
        fences.ForEach(f => f.Adjust(this.transform.position, goalCol.size.x, goalCol.size.y));
        fences.Single(f => (int)f.placement == (int)openDirection).gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Pan")) return;
        BreadBehaviour bread = collision.GetComponent<BreadBehaviour>();
        if (!bread.Data.baked) return;
        enteredBreads.Add(collision);
        if (bread.Data.type == this.data.type)
        {
            bread.SetGoal();
        }
        if (CheckAllConteins(this.data.type))
        {
            Debug.Log("Goal Completed");
        }
    }
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
    private bool CheckAllConteins(BreadType type)
    {
        return manager.Breads.Where(b => b.Data.type == type).All(m => enteredBreads.Select(e => e.gameObject).Contains(m.gameObject));
    }
    [ContextMenu("Test")]
    public void Test()
    {
        this.data = new GoalData((BreadType)1);
    }
}