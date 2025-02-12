using UnityEngine;
using System.Collections.Generic;

public class BreadManager : MonoBehaviour // �p���̐����A�Ǘ����s���N���X
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
