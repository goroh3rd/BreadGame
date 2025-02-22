using UnityEngine;
using System.Collections.Generic;

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
public class BreadImageManager : MonoBehaviour
{
    [SerializeField] List<Sprite> rawImages = new();
    private Dictionary<BreadType, Sprite> rawImageDict;
    [SerializeField] List<Sprite> bakedImages = new();
    private Dictionary<BreadType, Sprite> bakedImageDict;
    private void Awake()
    {
        rawImageDict = new Dictionary<BreadType, Sprite>
        {
            { BreadType.test, rawImages[0] },
            { BreadType.white, rawImages[1] },
            { BreadType.baguette, rawImages[2] },
            { BreadType.croissant, rawImages[3] },
            { BreadType.cream, rawImages[4] },
            { BreadType.bagel, rawImages[5] },
            { BreadType.curry, rawImages[6] },
        };
        bakedImageDict = new Dictionary<BreadType, Sprite>
        {
            { BreadType.test, bakedImages[0] },
            { BreadType.white, bakedImages[1] },
            { BreadType.baguette, bakedImages[2] },
            { BreadType.croissant, bakedImages[3] },
            { BreadType.cream, bakedImages[4] },
            { BreadType.bagel, bakedImages[5] },
            { BreadType.curry, bakedImages[6] },
        };
    }
    public Sprite GetRawImage(BreadType type)
    {
        return rawImageDict[type];
    }
    public Sprite GetBakedImage(BreadType type)
    {
        return bakedImageDict[type];
    }
}
