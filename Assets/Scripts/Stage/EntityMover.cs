using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class EntityMover : MonoBehaviour
{
    [SerializeField] private List<Vector2> wayPoint;
    [SerializeField] private float speed;
    [SerializeField] private bool isLoop;
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
        this.transform.DOMove(point, speed).OnComplete(() =>
        {
            currentWayPointIndex++;
            if (currentWayPointIndex < wayPoint.Count)
            {
                Move(wayPoint[currentWayPointIndex]);
            }
            else if (isLoop)
            {
                currentWayPointIndex = 0;
                Move(wayPoint[currentWayPointIndex]);
            }
        });
    }
}
