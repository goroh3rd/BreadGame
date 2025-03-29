using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] TextMeshProUGUI timeText;
    private bool isGoalCompleted = false;
    public bool IsGoalCompleted => isGoalCompleted;
    private int clickCount = 0;
    public int ClickCount => clickCount;
    private float stageTime = 0;
    public float StaegTime => stageTime;
    private void Update()
    {
        if (!isGoalCompleted) stageTime += Time.deltaTime;
        timeText.text = $"ƒ^ƒCƒ€ : {stageTime.ToString("F2")}";
        if (Input.GetKeyDown(KeyCode.R) && !isGoalCompleted) ResetStage();
        if (Input.GetKeyDown(KeyCode.Escape) && !isGoalCompleted) SceneManager.LoadScene("StageSelect");
    }
    public void AddClickCount()
    {
        clickCount++;
    }
    private void Start()
    {
        this.stageTime = 0;
        this.clickCount = 0;
        GetInitialState();
    }
    public void ResetStage()
    {
        this.stageTime = 0;
        this.clickCount = 0;
        ResetAllBreads();
        isGoalCompleted = false;
    }
    private void GetInitialState()
    {
        goalManager = FindAnyObjectByType<GoalManager>();
    }
    private void ResetAllBreads()
    {
        breadManager.Breads.ForEach(bread => bread.ResetBread());
    }
    public void GoalCompleted()
    {
        isGoalCompleted = true;
    }
}