using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ClearWindowBehaviour : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private SceneAsset stageSelectScene;
    private Transform initialPlace;
    private StageManager stageManager;
    private void Start()
    {
        this.canvasGroup.alpha = 0;
        initialPlace = this.transform;
        this.stageManager = FindAnyObjectByType<StageManager>();
    }
    public void Appear()
    {
        this.canvasGroup.DOFade(1, 0.5f);
        this.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);
    }
    public void Retry()
    {
        this.transform.position = initialPlace.position;
        this.canvasGroup.alpha = 0;
        stageManager?.ResetStage();
    }
    public void StageSelect()
    {
        SceneManager.LoadScene(stageSelectScene.name);
    }
}
