using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class StageManager : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] TextMeshProUGUI timeText;
    private List<StageSelectAnimation> stageSelectAnimation;
    private bool isPlaying = false;
    public bool IsPlaying => isPlaying;
    private int clickCount = 0;
    private bool isGoalCompleted = false;
    public int ClickCount => clickCount;
    private float stageTime = 0;
    public float StageTime => stageTime;
    private bool start = false;
    private void Start()
    {
        stageSelectAnimation = FindObjectsByType<StageSelectAnimation>(FindObjectsSortMode.None)
            .Where(obj => obj.gameObject.scene.name != null && obj.gameObject.scene.name == "DontDestroyOnLoad")
            .ToList();
        this.stageTime = 0;
        this.clickCount = 0;
        this.isGoalCompleted = false;
        GetInitialState();
    }
    private void Update()
    {
        if (!isGoalCompleted && !start && stageSelectAnimation.Count == 0) // ステージから直接playモードに移行するとstageSelectAnimationが取得できず下のif文でエラーが返ってくる
        {
            isPlaying = true;
            start = true;
        }
        if (!isGoalCompleted && !start && !stageSelectAnimation[0].IsAnimating)
        {
            isPlaying = true;
            start = true;
        }
        if (isPlaying) stageTime += Time.deltaTime;
        timeText.text = $"タイム : {stageTime.ToString("F2")}";
        if (Input.GetKeyDown(KeyCode.R) && isPlaying) ResetStage();
        if (Input.GetKeyDown(KeyCode.Escape) && isPlaying)
        {
            StageSelectAnimation stageSelectAnimation = FindAnyObjectByType<StageSelectAnimation>();
            stageSelectAnimation.LoadScene("StageSelect");
        }
    }
    public void AddClickCount()
    {
        clickCount++;
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
        isGoalCompleted = true;
    }
}