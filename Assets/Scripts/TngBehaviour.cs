using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class TngBehaviour : MonoBehaviour
{
    [SerializeField] BreadManager breadManager;
    [SerializeField] float maxForce = 1.0f;
    [SerializeField] float range = 1.0f;
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        this.transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        if (Input.GetMouseButtonDown(0))
        {
            PushBread(this.transform.position);
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
}
