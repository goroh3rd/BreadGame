using UnityEngine;
using System.Collections.Generic;

public class BreadManager : MonoBehaviour // パンの生成、管理を行うクラス
{
    private Dictionary<GameObject, BreadBehaviour> breads = new();
    public Dictionary<GameObject, BreadBehaviour> Breads => breads;
    [SerializeField] GameObject breadPrefab;
    [SerializeField] List<Sprite> breadImages = new();
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
