using UnityEngine;
using UnityEngine.UI;

public class VolumePanelShowing : MonoBehaviour
{
    [SerializeField] private GameObject scrollbars;
    [SerializeField] private Image fillImage;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float appearDuration = 0.5f;
    private float appearTime = 0f;
    private bool showing = false;
    public bool IsShowing => showing;
    private bool filling = false;
    public bool IsFilling => filling;
    public bool intracting = false;
    private void Start()
    {
        // èâä˙âªèàóù
        showing = false;
        UpdateWindow();
    }
    private void Update()
    {
        if (filling)
        {
            Fill();
        }
        if (showing)
        {
            scrollbars.SetActive(true);
            UpdateWindow();
        }
        else
        {
            scrollbars.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && showing && !intracting)
        {
            HidePanel();
        }
    }
    public void PointerDown()
    {
        if (showing) HidePanel();
        else StartFill();
    }
    public void PointerUp()
    {
        StopFill();
    }
    public void StartIntract()
    {
        intracting = true;
    }
    public void StopIntract()
    {
        intracting = false;
    }
    private void UpdateWindow()
    {
        if (showing)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            showing = true;
            return;
        }
        else
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            showing = false;
            return;
        }
    }
    private void HidePanel()
    {
        if (!showing) return;
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        showing = false;
        appearTime = 0f;
        fillImage.fillAmount = 0f;
        UpdateWindow();
    }
    private void ShowPanel()
    {
        if (showing) return;
        showing = true;
        appearTime = 0f;
        UpdateWindow();
    }
    private void StartFill()
    {
        filling = true;
    }
    private void StopFill()
    {
        filling = false;
        appearTime = 0f;
        fillImage.fillAmount = 0f;
    }
    private void Fill()
    {
        if (showing) return;
        if (appearTime < appearDuration)
        {
            appearTime += Time.deltaTime;
            fillImage.fillAmount = appearTime / appearDuration;
        }
        else
        {
            ShowPanel();
            appearTime = 0f;
        }
    }
}
