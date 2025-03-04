using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TngBehaviour : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField, Range(0f, 2f)] float maxForce = 1.0f;
    [SerializeField, Range(0f, 3f)] float range = 1.0f;
    [SerializeField] GameObject left;
    [SerializeField] GameObject right;
    private SpriteRenderer leftRenderer;
    private SpriteRenderer rightRenderer;
    private float clickTime;
    [SerializeField, Range(0f, 1f)] float longClickThreshold = 0.5f;
    private void Start()
    {
        leftRenderer = left.GetComponent<SpriteRenderer>();
        rightRenderer = right.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        //if (Input.GetMouseButtonDown(0))
        //{
        //    PushBread(left.transform.position);
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    PushBread(right.transform.position);
        //}

        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            clickTime = Time.deltaTime;
        }
        if (Input.GetMouseButton(0) && Time.deltaTime - clickTime > longClickThreshold)
        {
            if (CheckBreadExist(mousePos).Item1 != null)
            {
                Debug.Log("Left Grabbed");
                CheckBreadExist(mousePos).Item2?.Grabbed(left.transform.position);
            }
            else
            {
                PushBread(left.transform.position);
            }
        }
        if (Input.GetMouseButton(1) && Time.deltaTime - clickTime > longClickThreshold)
        {
            if (CheckBreadExist(mousePos).Item1 != null)
            {
                Debug.Log("Left Grabbed");
                CheckBreadExist(mousePos).Item2?.Grabbed(right.transform.position);
            }
            else
            {
                PushBread(right.transform.position);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            PushBread(left.transform.position);
        }
        if (Input.GetMouseButtonUp(1))
        {
            PushBread(right.transform.position);
        }

        if (Input.GetMouseButton(0))
        {
            leftRenderer.color = Color.red;
        }
        else
        {
            leftRenderer.color = Color.white;
        }
        if (Input.GetMouseButton(1))
        {
            rightRenderer.color = Color.red;
        }
        else
        {
            rightRenderer.color = Color.white;
        }
    }
    private void PushBread(Vector3 pos)
    {
        for (int i = 0; i < breadManager.Breads.Count; i++)
        {
            BreadBehaviour bread = breadManager.Breads.ElementAt(i).Value;
            if (GetDistance(pos, bread.transform.position) > range) continue;
            Vector3 force = (bread.transform.position - pos).normalized * maxForce;
            AddForceToBread(bread, force);
        }
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
        for (int i = 0; i < breadManager.Breads.Count; i++)
        {
            BreadBehaviour bread = breadManager.Breads.ElementAt(i).Value;
            if (GetDistance(this.transform.position, bread.transform.position) < range)
            {
                AddForceToBread(bread, force);
            }
        }
    }
    private bool longClicked(float threshold = 0.5f)
    {
        return Input.GetMouseButton(0) && Input.GetMouseButtonDown(0) && Input.GetMouseButtonUp(0);
    }
    private (GameObject, BreadBehaviour) CheckBreadExist(Vector3 pos)
    {
        foreach (var bread in breadManager.Breads)
        {
            if (Physics2D.OverlapPointAll(pos).Select(c => c.gameObject).ToList().Contains(bread.Key))
            {
                return (bread.Key, bread.Value);
            }
        }
        return (null, null);
    }
}
