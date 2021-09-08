using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointComtroller : MonoBehaviour
{
    private float mZCoord;

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void Update()
    {
        mZCoord = Camera.main.WorldToScreenPoint(transform.position).z;
        transform.position = GetMouseAsWorldPoint();
    }
}
