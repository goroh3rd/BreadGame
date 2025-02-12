using UnityEngine;

public class BreadBehaviour : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    BreadType type;
    bool baked;
    public void Init(BreadType type, Vector3 pos, bool baked = true)
    {
        this.type = type;
        transform.position = pos;
        this.baked = baked;
    }
}