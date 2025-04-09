using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;

public class ClearWindowBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private string stageSelectScene;
    [SerializeField] private GameObject retryButton;
    [SerializeField] private GameObject stageSelectButton;
    [SerializeField] private TextMeshProUGUI resultText;
    [SerializeField] private StageSelectAnimation stageSelectAnimation;
    private Vector3 initialPlace;
    private StageManager stageManager;
    private void Start()
    {
        this.canvasGroup.alpha = 0;
        initialPlace = this.transform.localPosition;
        retryButton.SetActive(false);
        stageSelectButton.SetActive(false);
        this.stageManager = FindAnyObjectByType<StageManager>();
    }
    public void Appear()
    {
        resultText.text = $"クリアタイム : {stageManager.StageTime.ToString("F2")}秒\nクリック数 : {stageManager.ClickCount}回";
        retryButton.SetActive(true);
        stageSelectButton.SetActive(true);
        this.canvasGroup.DOFade(1, 0.5f);
        this.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);
    }
    public void Retry()
    {
        this.transform.localPosition = initialPlace;
        retryButton.SetActive(false);
        stageSelectButton.SetActive(false);
        this.canvasGroup.alpha = 0;
        stageManager?.ResetStage();
    }
    public void StageSelect()
    {
        SceneManager.LoadScene(stageSelectScene);
    }
    public void Return()
    {
        stageSelectAnimation.LoadScene(stageSelectScene);
    }
}
