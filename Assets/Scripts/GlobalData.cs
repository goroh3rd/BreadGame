using System;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    private static int currentStageIndex;
    private static List<String> completedStage = new();
    public static List<String> CompletedStage => completedStage;
    public static void SetCurrentStageIndex(int index)
    {
        currentStageIndex = index;
    }
    public static void SetStageCompleted(String index)
    {
        if (!completedStage.Contains(index))
        {
            completedStage.Add(index);
        }
    }
    public static void ResetAllRecords()
    {
        PlayerPrefs.DeleteAll();
        completedStage.Clear();
    }
}
