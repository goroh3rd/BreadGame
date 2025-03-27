using UnityEngine;
using System.Collections.Generic;

public class BreadManager : MonoBehaviour // パンの生成、管理を行うクラス
{
    [SerializeField] BreadImageManager imageManager;
    private List<BreadBehaviour> breads = new();
    public List<BreadBehaviour> Breads => breads;
    [SerializeField] private Dictionary<BreadType, CookerBehaviour.CookerType> correctCooker = new Dictionary<BreadType, CookerBehaviour.CookerType>
    {
        { BreadType.test, CookerBehaviour.CookerType.Bake },
        { BreadType.white, CookerBehaviour.CookerType.Bake },
        { BreadType.baguette, CookerBehaviour.CookerType.Bake },
        { BreadType.croissant, CookerBehaviour.CookerType.Bake },
        { BreadType.cream, CookerBehaviour.CookerType.Bake },
        { BreadType.bagel, CookerBehaviour.CookerType.Boil },
        { BreadType.curry, CookerBehaviour.CookerType.Fry },
    };
    [SerializeField] GameObject breadPrefab;
    public void CreateBread(BreadData data)
    {
        GameObject bread = Instantiate(breadPrefab, data.pos, Quaternion.identity);
        BreadBehaviour breadBehaviour = bread.GetComponent<BreadBehaviour>();
        breads.Add(breadBehaviour);
        breadBehaviour.Init(data, this);
    }
    public void AddBread(BreadBehaviour bread)
    {
        breads.Add(bread);
    }
    public void RemoveBread(BreadBehaviour bread)
    {
        breads.Remove(bread);
        Destroy(bread.gameObject);
    }
    public void BakeBread(BreadBehaviour bread)
    {
        bread.Bake();
    }
    public bool CheckCorrectCooker(BreadType bread, CookerBehaviour.CookerType cooker)
    {
        if (correctCooker[bread] == cooker) return true;
        return false;
    }
    [ContextMenu("CreateBread")]
    public void Test()
    {
        for(int i = 0; i < 1; i++) CreateBread(new BreadData((BreadType)0, Vector3.zero, false));
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Test();
    }
}
[System.Serializable] public class BreadData
{
    public BreadType type;
    public Vector3 pos;
    public bool baked;
    public GrabType grabType;
    public bool isGoal = false;
    public BreadData(BreadType type, Vector3 pos, bool baked = true, GrabType grabType = GrabType.Released)
    {
        this.type = type;
        this.pos = pos;
        this.baked = baked;
    }
    public BreadData Bake()
    {
        this.baked = true;
        return this;
    }
    public BreadData Unbake()
    {
        this.baked = false;
        return this;
    }
    public BreadData SetGoal()
    {
        this.isGoal = true;
        return this;
    }
}
public class BreadState : BreadData
{
    public Vector3 initialPosition;
    public BreadState(BreadData data, Vector3 pos) : base(data.type, data.pos, data.baked)
    {
        this.initialPosition = pos;
    }
}
public enum BreadType
{
    test,
    white,
    baguette,
    croissant,
    cream,
    bagel,
    curry,
}
public enum GrabType
{
    Released,
    Left,
    Right
}
