using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class WindowButtonBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject button;
    public void OnMouseEnter()
    {
        button.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }
    public void OnMouseExit()
    {
        button.transform.localScale = new Vector3(1f, 1f, 1f);
    }

}
