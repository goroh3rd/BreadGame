using System;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalData
{
    private static int currentStageIndex;
    private static List<string> completedStage = new();
    public static List<string> CompletedStage => completedStage;
    public static void SetCurrentStageIndex(int index)
    {
        currentStageIndex = index;
    }
    public static void SetStageCompleted(string index)
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
