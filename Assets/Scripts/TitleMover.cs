using UnityEngine;
using DG.Tweening;

public class TitleMover : MonoBehaviour
{
    [SerializeField] CanvasGroup titleCanvasGroup;
    [SerializeField] float titleMoveDistance;
    [SerializeField] float titleMoveDuration;
    [SerializeField] CanvasGroup subtitleCanvasGroup;
    [SerializeField] float subtitleFadeAlpha;
    [SerializeField] float subtitleFadeDuration;
    private void Start()
    {
        UpDown();
        Blink();
    }
    private void UpDown()
    {
        titleCanvasGroup.transform.DOLocalMoveY(titleCanvasGroup.transform.position.y - titleMoveDistance, titleMoveDuration).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
    private void Blink()
    {
        subtitleCanvasGroup.DOFade(subtitleFadeAlpha, subtitleFadeDuration).SetEase(Ease.InQuart).SetLoops(-1, LoopType.Yoyo);
    }
}
