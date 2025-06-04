using UnityEngine;
using UnityEngine.UI;

public class FinalStage : MonoBehaviour
{
    [SerializeField] private Button finalButton;
    [SerializeField] private Image finalImage;
    [SerializeField] private GameObject Text;
    [SerializeField] private GameObject record;

    void Start()
    {
        finalButton.interactable = false;
        finalImage.enabled = false;
        Text.SetActive(false);
        record.SetActive(false);
        for (int i = 1; i < 15; i++)
        {
            if (!PlayerPrefs.HasKey((string)("Stage" + i))) return;
        }
        finalButton.interactable = true;
        finalImage.enabled = true;
        Text.SetActive(true);
        record.SetActive(true);
    }
}
