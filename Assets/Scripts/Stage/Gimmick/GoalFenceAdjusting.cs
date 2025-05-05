using UnityEngine;

public class GoalFenceAdjusting : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GoalBehaviour parent;
    [SerializeField] public GoalFencePlacement placement;
    [SerializeField] private Vector3 scale = new Vector3(1, 1, 1);
    public enum GoalFencePlacement { Top, Bottom, Left, Right }
    public void Adjust(Vector3 place)
    {
        Vector3 scale;
        scale.x = this.scale.x / parent.transform.localScale.x;
        scale.y = this.scale.y / parent.transform.localScale.y;
        scale.z = 1;
        this.transform.localScale = scale;
        switch (placement)
        {
            case GoalFencePlacement.Top:
                this.transform.position = new Vector3(place.x, place.y + parent.transform.localScale.y / 2, 0);
                spriteRenderer.size = new Vector2(1 / this.transform.localScale.x, 0.15f);
                boxCollider.size = new Vector2(1 / this.transform.localScale.x, 0.01f * parent.transform.localScale.y);
                break;
            case GoalFencePlacement.Bottom:
                this.transform.position = new Vector3(place.x, place.y - parent.transform.localScale.y / 2, 0);
                spriteRenderer.size = new Vector2(1 / this.transform.localScale.x, 0.15f);
                boxCollider.size = new Vector2(1 / this.transform.localScale.x, 0.01f * parent.transform.localScale.y);
                break;
            case GoalFencePlacement.Left:
                this.transform.position = new Vector3(place.x - parent.transform.localScale.x / 2, place.y, 0);
                spriteRenderer.size = new Vector2(0.15f, 1 / this.transform.localScale.y);
                boxCollider.size = new Vector2(0.01f * parent.transform.localScale.x, 1 / this.transform.localScale.y);
                break;
            case GoalFencePlacement.Right:
                this.transform.position = new Vector3(place.x + parent.transform.localScale.x / 2, place.y, 0);
                spriteRenderer.size = new Vector2(0.15f, 1 / this.transform.localScale.y);
                boxCollider.size = new Vector2(0.01f * parent.transform.localScale.x, 1 / this.transform.localScale.y);
                break;
            default:
                Debug.LogError("Invalid GoalFencePlacement");
                break;
        }
    }
}
