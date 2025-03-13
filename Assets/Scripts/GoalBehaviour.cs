using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GoalBehaviour : MonoBehaviour
{
    [SerializeField] private BreadManager manager;
    [SerializeField] GoalData data;
    public GoalData Data => data;
    private List<Collider2D> enteredBreads = new();
    private void Start()
    {
        this.manager = FindAnyObjectByType<BreadManager>();
        this.data = new GoalData();
    }
    private void OnTriggerEnter2D (Collider2D collision)
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
[System.Serializable] public class GoalData
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