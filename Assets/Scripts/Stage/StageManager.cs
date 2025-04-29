using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;
using System.Collections;
using DG.Tweening;

public class StageManager : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField] GoalManager goalManager;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] ClearWindowBehaviour clearWindow;
    private List<StageSelectAnimation> stageSelectAnimation;
    private bool isPlaying = false;
    public bool IsPlaying => isPlaying;
    private int clickCount = 0;
    private bool isGoalCompleted = false;
    public int ClickCount => clickCount;
    private float stageTime = 0;
    public float StageTime => stageTime;
    private bool start = false;
    private Coroutine coroutine;
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
            DOTween.KillAll();
            stageSelectAnimation.LoadScene("StageSelect");
        }
    }
    public bool CheckAllConteins(GoalBehaviour goal)
    {
        return breadManager.Breads.Where(b => b.Data.type == goal.Data.type).All(m => goal.EnteredBreads.Select(e => e.gameObject).Contains(m.gameObject));
    }
    public void CheckAllGoalCompleted()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(ExecuteWithDelay(0.5f, () =>
        {
            if (breadManager.Breads.All(b => b.Data.isGoal && b.Data.baked))
            {
                StageCompleted();
                StartCoroutine(GoalCompleted(0.5f));
                coroutine = null;
            }
        }));
    }
    public IEnumerator ExecuteWithDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }
    public IEnumerator GoalCompleted(float wait)
    {
        Debug.Log("All Goal Completed");
        yield return new WaitForSeconds(wait);
        clearWindow.Appear();
    }
    public void AddClickCount()
    {
        clickCount++;
    }
    public void ResetStage()
    {
        DOTween.KillAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
    public void StageCompleted()
    {
        DOTween.KillAll();
        GlobalData.SetStageCompleted(SceneManager.GetActiveScene().name);
        breadManager.Breads.ForEach(bread =>
        {
            bread.Stop();
        });
        isPlaying = false;
        isGoalCompleted = true;
        if (stageTime < PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name)) // 最速タイムを保存
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, stageTime);
            clearWindow.SetNewRecord();
        }
        else if (PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name, -1) == -1) // 初回プレイ時に保存
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, stageTime);
            clearWindow.SetNewRecord();
        }
    }
}