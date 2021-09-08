using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHand : MonoBehaviour
{
    [SerializeField] private float speed;
    internal Vector3 point;
    internal Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        point = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, point, speed * Time.fixedDeltaTime);
    }

    internal void ResetPosition()
    {
        point = startPosition;
    }
}
