using UnityEngine;

public class VolumePanelShowing : MonoBehaviour
{
    [SerializeField] private GameObject scrollbars;
    [SerializeField] private CanvasGroup canvasGroup;
    private bool showing = false;
    private bool isfixed = false;
    private void Start()
    {
        // èâä˙âªèàóù
        showing = false;
        isfixed = false;
        UpdateWindow();
    }
    private void UpdateWindow()
    {
        if (isfixed)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            showing = true;
            return;
        }
        if (showing)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
        }
    }
    public void SetShowing(bool show)
    {
        showing = show;
        UpdateWindow();
    }
    public void ToggleShowing()
    {
        showing = !showing;
        UpdateWindow();
    }
    public void SetFixed(bool fixed_)
    {
        isfixed = fixed_;
        UpdateWindow();
    }
    public void ToggleFixed()
    {
        isfixed = !isfixed;
        showing = isfixed;
        UpdateWindow();
    }
}
