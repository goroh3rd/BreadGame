using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SelectButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string scene;
    [SerializeField] private Image image;
    [SerializeField] private Color completedColor;
    [SerializeField] private Material completedMaterial;
    [SerializeField] private StageSelectAnimation stageSelectAnimation;
    [SerializeField] private TextMeshProUGUI recordText;
    private void Start()
    {
        recordText.text = "";
        if (PlayerPrefs.GetFloat(scene, -1) != -1)
        {
            image.material = completedMaterial;
            image.color = completedColor;
        }
        else
        {
            image.material = null;
            image.color = Color.white;
        }
    }
    public void LoadScene()
    {
        stageSelectAnimation.LoadScene(scene);
    }
    public void Select()
    {
        this.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        SetRecordText();
    }
    public void Deselect()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        recordText.text = "";
    }
    public void Click()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine(SoundManager.PlaySE(7, 1f)); // SE‚ð–Â‚ç‚·
    }
    public void SetRecordText()
    {
        if (PlayerPrefs.GetFloat(scene, -1) == -1)
        {
            recordText.text = "";
        }
        else
        {
            recordText.text = $"Best : {PlayerPrefs.GetFloat(scene, -1).ToString("F2")}";
        }
    }
}
