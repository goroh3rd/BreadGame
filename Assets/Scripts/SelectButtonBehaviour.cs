using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectButtonBehaviour : MonoBehaviour
{
    [SerializeField] private int sceneIndex;
    [SerializeField] private Button button;
    [SerializeField] private string scene;
    public void LoadScene()
    {
        SceneManager.LoadScene(scene);
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
    }
}
