using UnityEngine;
using DG.Tweening;

public class TitleCamera : MonoBehaviour
{
    public static bool firstTime = true; // 初回起動かどうか
    public static bool selecting = false; // 選択中かどうか
    [SerializeField] VolumePanelShowing volumePanelShowing; // VolumePanelShowingの参照
    private void Start()
    {
        if (firstTime)
        {
            this.transform.position = new Vector3(0, 15.8f, -10); // カメラの初期位置
            Debug.Log("初回起動時の処理を実行");
        }
        else
        {
            // 2回目以降の起動時の処理
            this.transform.position = new Vector3(0, 0, -10); // カメラの位置を再設定
            Debug.Log("2回目以降の起動時の処理を実行");
        }
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !volumePanelShowing.IsFilling && !volumePanelShowing.IsShowing && firstTime)
        {
            firstTime = false;
            MoveCamera();
        }
    }
    [ContextMenu("Move Camera")]
    private void MoveCamera()
    {
        this.transform.DOMove(new Vector3(0, 0, -10), 4f).SetEase(Ease.InOutCubic).OnComplete(() => {
            selecting = true; // 選択中にする
            Debug.Log("カメラの移動完了");
        });
    }
}
