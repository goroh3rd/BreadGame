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
    private bool newRecord = false;
    private void Start()
    {
        this.canvasGroup.alpha = 0;
        initialPlace = this.transform.localPosition;
        retryButton.SetActive(false);
        stageSelectButton.SetActive(false);
        this.stageManager = FindAnyObjectByType<StageManager>();
        newRecord = false;
    }
    public void Appear()
    {
        resultText.text = $"�N���A�^�C�� : {stageManager.StageTime.ToString("F2")}�b\n�N���b�N�� : {stageManager.ClickCount}��\n�ő��^�C�� : {PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name).ToString("F2")}�b";
        if (newRecord)
        {
            resultText.text += "<color=red><size=24> �V�L�^�I</size></color>";
        }
        retryButton.SetActive(true);
        stageSelectButton.SetActive(true);
        this.canvasGroup.DOFade(1, 0.5f);
        this.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);
    }
    public void Retry()
    {
        StartCoroutine(SoundManager.PlaySE(7, 1f)); // SE��炷
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
        StartCoroutine(SoundManager.PlaySE(7, 1f)); // SE��炷
        stageSelectAnimation.LoadScene(stageSelectScene);
    }
    public void SetNewRecord()
    {
        newRecord = true;
    }
}
