using UnityEngine;
using DG.Tweening;

public class TitleCamera : MonoBehaviour
{
    public static bool firstTime = true; // ����N�����ǂ���
    public static bool selecting = false; // �I�𒆂��ǂ���
    [SerializeField] VolumePanelShowing volumePanelShowing; // VolumePanelShowing�̎Q��
    private void Start()
    {
        if (firstTime)
        {
            this.transform.position = new Vector3(0, 15.8f, -10); // �J�����̏����ʒu
            Debug.Log("����N�����̏��������s");
        }
        else
        {
            // 2��ڈȍ~�̋N�����̏���
            this.transform.position = new Vector3(0, 0, -10); // �J�����̈ʒu���Đݒ�
            Debug.Log("2��ڈȍ~�̋N�����̏��������s");
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
            selecting = true; // �I�𒆂ɂ���
            Debug.Log("�J�����̈ړ�����");
        });
    }
}
