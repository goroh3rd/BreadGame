using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] TextMeshProUGUI timeText;
    private bool isPlaying = false;
    public bool IsGoalCompleted => isPlaying;
    private int clickCount = 0;
    public int ClickCount => clickCount;
    private float stageTime = 0;
    public float StageTime => stageTime;
    private void Update()
    {
        if (isPlaying) stageTime += Time.deltaTime;
        timeText.text = $"ƒ^ƒCƒ€ : {stageTime.ToString("F2")}";
        if (Input.GetKeyDown(KeyCode.R) && !isPlaying) ResetStage();
        if (Input.GetKeyDown(KeyCode.Escape) && !isPlaying) SceneManager.LoadScene("StageSelect");
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
        isPlaying = false;
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
        isPlaying = true;
    }
}