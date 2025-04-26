using System;
using System.Collections.Generic;

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

}
