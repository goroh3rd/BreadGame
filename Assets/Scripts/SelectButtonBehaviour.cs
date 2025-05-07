using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButtonBehaviour : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private string scene;
    [SerializeField] private Image image;
    [SerializeField] private Color completedColor;
    [SerializeField] private Material completedMaterial;
    [SerializeField] private StageSelectAnimation stageSelectAnimation;
    private void Start()
    {
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
    }
    public void Deselect()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
    }
    public void Click()
    {
        this.transform.localScale = new Vector3(1f, 1f, 1f);
        StartCoroutine(SoundManager.PlaySE(7, 1f)); // SE‚ð–Â‚ç‚·
    }
}
