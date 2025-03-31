using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TngBehaviour : MonoBehaviour
{
    [SerializeField] private StageManager stageManager;
    [SerializeField] BreadManager breadManager;
    [SerializeField, Range(0f, 2f)] float maxForce = 1.0f;
    [SerializeField, Range(0f, 3f)] float range = 1.0f;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    private SpriteRenderer leftRenderer;
    private SpriteRenderer rightRenderer;
    private float clickTime;
    //private bool isLeftLongClick = false;
    //private bool isRightLongClick = false;
    //[SerializeField, Range(0f, 1f)] float longClickThreshold = 0.2f;
    private BreadBehaviour leftGrabbed;
    private BreadBehaviour rightGrabbed;

    private void Start()
    {
        leftRenderer = left.GetComponent<SpriteRenderer>();
        rightRenderer = right.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        if (Input.GetMouseButtonDown(0) && !stageManager.IsGoalCompleted)
        {
            PushBread(left.transform.position);
            stageManager.AddClickCount();
        }
        if (Input.GetMouseButtonDown(1) && !stageManager.IsGoalCompleted)
        {
            PushBread(right.transform.position);
            stageManager.AddClickCount();
        }
        //if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        //{
        //    clickTime = Time.time;
        //}
        //if (Input.GetMouseButton(0) && Time.time - clickTime > longClickThreshold && !isLeftLongClick)
        //{
        //    if (CheckBreadExist(left.transform.position) is BreadBehaviour bread)
        //    {
        //        leftGrabbed = bread;
        //        DragBread(bread, left.transform.position);
        //    }
        //    else
        //    {
        //        PushBread(left.transform.position);
        //    }
        //    isLeftLongClick = true;
        //}
        //if (Input.GetMouseButton(1) && Time.time - clickTime > longClickThreshold && !isRightLongClick)
        //{
        //    if (CheckBreadExist(right.transform.position) is BreadBehaviour bread)
        //    {
        //        rightGrabbed = bread;
        //        DragBread(bread, right.transform.position);
        //    }
        //    else
        //    {
        //        PushBread(right.transform.position);
        //    }
        //    isRightLongClick = true;
        //}
        //leftGrabbed?.Grabbed(left.transform.position, GrabType.Left);
        //rightGrabbed?.Grabbed(right.transform.position, GrabType.Right);
        //if (Input.GetMouseButtonUp(0))
        //{
        //    if (Time.time - clickTime < longClickThreshold)
        //    {
        //        PushBread(left.transform.position);
        //    }
        //    leftGrabbed?.Released();
        //    isLeftLongClick = false;
        //    leftGrabbed = null;
        //}
        //if (Input.GetMouseButtonUp(1))
        //{
        //    if (Time.time - clickTime < longClickThreshold)
        //    {
        //        PushBread(right.transform.position);
        //    }
        //    rightGrabbed?.Released();
        //    isRightLongClick = false;
        //    rightGrabbed = null;
        //}
        leftRenderer.color = Input.GetMouseButton(0) ? Color.red : Color.white;
        rightRenderer.color = Input.GetMouseButton(1) ? Color.red : Color.white;
    }
    private void PushBread(Vector3 pos)
    {
        foreach (var bread in breadManager.Breads)
        {
            if (GetDistance(pos, bread.transform.position) > range || bread.Data.grabType != GrabType.Released) continue;
            Vector3 force = (bread.transform.position - pos).normalized * maxForce;
            AddForceToBread(bread, force);
        }
    }
    private void DragBread(BreadBehaviour bread, Vector3 pos)
    {
        bread.transform.position = pos;
    }
    private void AddForceToBread(BreadBehaviour bread, Vector3 force)
    {
        bread.AddForce(force);
    }
    private float GetDistance(Vector3 a, Vector3 b)
    {
        return Vector3.Distance(a, b);
    }
    private void AddForceToNearBread(Vector3 force)
    {
        foreach (var bread in breadManager.Breads)
        {
            if (GetDistance(this.transform.position, bread.transform.position) < range)
            {
                AddForceToBread(bread, force);
            }
        }
    }
    private BreadBehaviour CheckBreadExist(Vector3 pos)
    {
        GameObject[] found = Physics2D.OverlapPointAll(pos).Select(c => c.gameObject).ToArray();
        foreach (var bread in breadManager.Breads)
        {
            if (found.Contains(bread.gameObject))
            {
                return bread;
            }
        }
        return null;
    }
}
