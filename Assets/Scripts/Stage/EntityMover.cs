using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;

public class EntityMover : MonoBehaviour
{
    [SerializeField] private List<Vector2> wayPoint;
    [SerializeField] private float speed;
    [SerializeField] private bool isLoop;
    [SerializeField] private int loopStartIndex = 0;
    private int currentWayPointIndex;
    private Vector2 currentWayPoint;
    private void Start()
    {
        currentWayPointIndex = 0;
        currentWayPoint = wayPoint[currentWayPointIndex];
        Move(currentWayPoint);
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
                if (0 <= loopStartIndex && loopStartIndex <= wayPoint.Count)
                {
                    currentWayPointIndex = loopStartIndex;
                }
                else
                {
                    Debug.LogError("Invalid loop start index");
                    currentWayPointIndex = 0;
                }
                Move(wayPoint[currentWayPointIndex]);
            }
        });
    }
}
