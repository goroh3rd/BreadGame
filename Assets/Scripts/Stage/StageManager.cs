using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] TextMeshProUGUI timeText;
    private StageSelectAnimation stageSelectAnimation;
    private bool isPlaying = false;
    public bool IsPlaying => isPlaying;
    private int clickCount = 0;
    public int ClickCount => clickCount;
    private float stageTime = 0;
    public float StageTime => stageTime;
    private bool start = false;
    private void Update()
    {
        if (!stageSelectAnimation.IsAnimating && !start)
        {
            isPlaying = true;
            start = true;
        }
        if (isPlaying) stageTime += Time.deltaTime;
        timeText.text = $"ƒ^ƒCƒ€ : {stageTime.ToString("F2")}";
        if (Input.GetKeyDown(KeyCode.R) && isPlaying) ResetStage();
        if (Input.GetKeyDown(KeyCode.Escape) && isPlaying) SceneManager.LoadScene("StageSelect");
    }
    public void AddClickCount()
    {
        clickCount++;
    }
    private void Start()
    {
        stageSelectAnimation = FindAnyObjectByType<StageSelectAnimation>();
        this.stageTime = 0;
        this.clickCount = 0;
        GetInitialState();
    }
    public void ResetStage()
    {
        this.stageTime = 0;
        this.clickCount = 0;
        ResetAllBreads();
        isPlaying = true;
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
        isPlaying = false;
    }
}