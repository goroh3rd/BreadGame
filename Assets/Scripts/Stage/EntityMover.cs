using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class EntityMover : MonoBehaviour
{
    [SerializeField] private List<Vector2> wayPoint = new();
    [SerializeField] private float speed;
    [SerializeField] private int initialWayPointIndex = 0;
    [SerializeField] private bool isLoop;
    [SerializeField] private int loopStartIndex = 0;
    [SerializeField, HideInInspector] private bool isReverse;
    [SerializeField, HideInInspector] private bool returnToStartImmediately;
    private int currentWayPointIndex;
    private Vector2 currentWayPoint;
    private bool reversing = false;
    private void Start()
    {
        this.transform.position = wayPoint[initialWayPointIndex];
        currentWayPointIndex = initialWayPointIndex;
        currentWayPoint = wayPoint[currentWayPointIndex];
        Move(currentWayPoint);
        //Time.timeScale = 3;
    }
    private void Move(Vector2 point)
    {
        this.transform.DOMove(point, Vector2.Distance(this.transform.position, point) / speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            currentWayPointIndex++;
            if (currentWayPointIndex < wayPoint.Count)
            {
                Move(wayPoint[currentWayPointIndex]);
            }
            else if (isLoop)
            {
                if (isReverse)
                {
                    if (reversing)
                    {
                        currentWayPointIndex = wayPoint.Count - 1;
                        reversing = false;
                    }
                    else
                    {
                        currentWayPointIndex = 0;
                        reversing = true;
                    }
                }
                else if (returnToStartImmediately)
                {
                    currentWayPointIndex = loopStartIndex;
                    this.transform.position = wayPoint[loopStartIndex];
                    currentWayPointIndex++;
                }
                else
                {
                    if (0 <= loopStartIndex && loopStartIndex <= wayPoint.Count)
                    {
                        currentWayPointIndex = loopStartIndex;
                    }
                    else
                    {
                        Debug.LogError("Invalid loop start index");
                        currentWayPointIndex = 0;
                    }
                }
                Move(wayPoint[currentWayPointIndex]);
            }
        });
    }
    public void AddCurrentPositionToWayPoint()
    {
        wayPoint.Add(this.transform.position);
    }
    public void SetFirstPosition()
    {
        this.transform.position = wayPoint[initialWayPointIndex];
    }
}
