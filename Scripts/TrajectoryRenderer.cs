using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRendererComponent;


    private void Start()
    {
        lineRendererComponent = GetComponent<LineRenderer>();
    }

    internal void ShowTrajectory(Vector3 origin, Vector3 speed)
    {
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < 30; i++)
        {
            float time = i * 0.1f;
            Vector3 pos = origin + speed * time + Physics.gravity * time * time / 2f;
            if (pos.y < -1.65f && i > 5)
            {
                break;
            }
            points.Add(pos);
        }
        lineRendererComponent.positionCount = points.Count;
        lineRendererComponent.SetPositions(points.ToArray());
    }

}
