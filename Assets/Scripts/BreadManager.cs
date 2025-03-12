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
        for(int i = 0; i < 10; i++) CreateBread(new BreadData((BreadType)Random.Range(0, 2), Vector3.zero, false));
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
}
public enum GrabType
{
    Released,
    Left,
    Right
}
