using UnityEngine;
using System.Collections.Generic;

public class BreadManager : MonoBehaviour // パンの生成、管理を行うクラス
{
    [SerializeField] BreadImageManager imageManager;
    private List<BreadBehaviour> breads = new();
    public List<BreadBehaviour> Breads => breads;
    [SerializeField] GameObject breadPrefab;
    public void CreateBread(BreadData data)
    {
        GameObject bread = Instantiate(breadPrefab, data.pos, Quaternion.identity);
        BreadBehaviour breadBehaviour = bread.GetComponent<BreadBehaviour>();
        breads.Add(breadBehaviour);
        breadBehaviour.Init(data, this);
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
    [ContextMenu("CreateBread")]
    public void Test()
    {
        for(int i = 0; i < 1; i++) CreateBread(new BreadData((BreadType)0, Vector3.zero));
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
    public Transform transform;
    public BreadState(BreadData data, Transform transform) : base(data.type, data.pos, data.baked)
    {
        this.transform = transform;
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
