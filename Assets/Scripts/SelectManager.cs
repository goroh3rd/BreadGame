using UnityEngine;
using UnityEngine.SceneManagement;

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
            reset = 0;
            GlobalData.ResetAllRecords();
            Debug.Log("Reset all records");
            SceneManager.LoadScene("StageSelect");
        }
    }
}
