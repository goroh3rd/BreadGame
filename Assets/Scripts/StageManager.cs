using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField] GoalManager goalManager;
    private void Start()
    {
        GetInitialState();
    }
    public void ResetStage()
    {
        ResetAllBreads();
    }
    private void GetInitialState()
    {
        breadManager = FindAnyObjectByType<BreadManager>();
        goalManager = FindAnyObjectByType<GoalManager>();
    }
    private void ResetAllBreads()
    {
        breadManager.Breads.ForEach(bread => bread.ResetBread());
    }
}