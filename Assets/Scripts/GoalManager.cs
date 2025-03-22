using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GoalManager : MonoBehaviour
{
    [SerializeField] private BreadManager breadManager;
    public List<GoalBehaviour> goalBehaviours = new();
    private void Start()
    {
        goalBehaviours = FindObjectsByType<GoalBehaviour>(FindObjectsSortMode.None).ToList();
    }
}

