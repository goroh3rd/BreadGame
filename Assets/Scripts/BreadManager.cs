using UnityEngine;
using System.Collections.Generic;

public class BreadManager : MonoBehaviour // パンの生成、管理を行うクラス
{
    [SerializeField] BreadImageManager imageManager;
    private Dictionary<GameObject, BreadBehaviour> breads = new();
    public Dictionary<GameObject, BreadBehaviour> Breads => breads;
    [SerializeField] GameObject breadPrefab;
    public void CreateBread(BreadData data)
    {
        GameObject bread = Instantiate(breadPrefab, data.pos, Quaternion.identity);
        breads.Add(bread, bread.GetComponent<BreadBehaviour>());
        breads[bread].Init(data, this);
    }
    public void RemoveBread(GameObject bread)
    {
        breads.Remove(bread);
        Destroy(bread);
    }
    public void BakeBread(GameObject bread)
    {
        breads[bread].Bake();
    }
    [ContextMenu("CreateBread")]
    public void Test()
    {
        for(int i = 0; i < 10; i++) CreateBread(new BreadData((BreadType)Random.Range(0, 2), Vector3.zero, true));
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) Test();
    }
}
public class BreadData
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
}
public enum GrabType
{
    Released,
    Left,
    Right
}
