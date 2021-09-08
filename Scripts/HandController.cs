using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    private void Update()
    {
        Vector3 mousePoint = Input.mousePosition;
        float z = Camera.main.WorldToScreenPoint(transform.position).z;
        mousePoint.z = z;
        transform.position = Camera.main.ScreenToWorldPoint(mousePoint);
        transform.position = new Vector3(transform.position.x, -1.5f, transform.position.z);
    }

}
