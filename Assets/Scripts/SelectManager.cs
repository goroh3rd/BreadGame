using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SelectManager : MonoBehaviour
{
    [SerializeField] private float resetTime = 5f;
    private float reset = 0;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Backspace))
        {
            reset += Time.deltaTime;
        }
        else
        {
            reset = 0;
        }
        if (reset > resetTime)
        {
            DOTween.KillAll();
            reset = 0;
            GlobalData.ResetAllRecords();
            Debug.Log("Reset all records");
            SceneManager.LoadScene("StageSelect");
        }
    }
}
