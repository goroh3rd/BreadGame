using UnityEngine;

public class GoalFenceAdjusting : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private GoalBehaviour parent;
    [SerializeField] public GoalFencePlacement placement;
    public enum GoalFencePlacement { Top, Bottom, Left, Right }
    public void Adjust(Vector3 place, float width, float height)
    {
        switch (placement)
        {
            case GoalFencePlacement.Top:
                this.transform.position = new Vector3(place.x, place.y + parent.transform.localScale.y / 2, 0);
                boxCollider.size = new Vector2(width, 0.1f / parent.transform.localScale.y);
                break;
            case GoalFencePlacement.Bottom:
                this.transform.position = new Vector3(place.x, place.y - parent.transform.localScale.y / 2, 0);
                boxCollider.size = new Vector2(width, 0.1f / parent.transform.localScale.y);
                break;
            case GoalFencePlacement.Left:
                this.transform.position = new Vector3(place.x - parent.transform.localScale.x / 2, place.y, 0);
                boxCollider.size = new Vector2(0.1f / parent.transform.localScale.x, height);
                break;
            case GoalFencePlacement.Right:
                this.transform.position = new Vector3(place.x + parent.transform.localScale.x / 2, place.y, 0);
                boxCollider.size = new Vector2(0.1f / parent.transform.localScale.x, height);
                break;
            default:
                Debug.LogError("Invalid GoalFencePlacement");
                break;
        }
    }
}
