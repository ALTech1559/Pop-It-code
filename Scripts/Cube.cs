using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private Transform point;
    [SerializeField] private TrajectoryRenderer playerLine;
    [SerializeField] private TrajectoryRenderer enemyLine;
    [SerializeField] private float force;
    [SerializeField] private List<Transform> parts;

    private Rigidbody rigidbody;
    private Collider collider;
    private bool isThrown = false;
    private float power = 100;
    private GameObject child;
    private Camera mainCamera;
    private Vector3 speed;

    internal int number;
    internal bool playerOrder;
    [SerializeField] internal Vector3 playerPosition;
    [SerializeField] internal Vector3 enemyPosition;

    private void Awake()
    {
        child = gameObject.transform.GetChild(0).gameObject;
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    internal void EnemyThrow()
    {
        StartCoroutine(EnemyThrowCoroutine());
    }

    private IEnumerator EnemyThrowCoroutine()
    {
        enemyLine.gameObject.SetActive(true);
        Vector3 mouseInWorld = new Vector3(Random.Range(-0.3f, 0.3f), Random.Range(-0.6f, 0.4f), Random.Range(-1.94f, -1.6f));
        mouseInWorld.y += 0.5f;
        Vector3 speed = (mouseInWorld - transform.position) * power;
        enemyLine.ShowTrajectory(transform.position, speed);
        yield return new WaitForSeconds(1);
        child.SetActive(true);
        AddTorque();
        rigidbody.AddForce(speed, ForceMode.VelocityChange);
        enemyLine.gameObject.SetActive(false);
        yield return new WaitUntil(() => playerOrder);
    }

    internal void Throw()
    {
        StartCoroutine(ThrowCoroutine());
    }

    private IEnumerator ThrowCoroutine()
    {
        yield return new WaitUntil(() => isThrown);
        yield return new WaitUntil(() => rigidbody.velocity.magnitude == 0);

        float maximumYPosition = -100;
        foreach (Transform part in parts)
        {
            if(maximumYPosition < part.transform.position.y)
            {
                maximumYPosition = part.transform.position.y;
                number = Convert.ToInt32(part.name);
            }
        }
        GameController.number = number;
        isThrown = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "ground")
        {
            isThrown = true;
        }
    }

    private void AddTorque()
    {
        float x = Random.Range(1000, 10000);
        float y = Random.Range(1000, 10000);
        float z = Random.Range(1000, 10000);
        rigidbody.AddTorque(new Vector3(x, y, z));
    }

    internal void OnDrag()
    {
        Vector3 mouseInWorld = point.position;
        mouseInWorld.y += 0.5f;
        speed = (mouseInWorld - transform.position) * power;

        if (transform.position.y > -1.65f)
        {
            collider.enabled = true;
        }

        playerLine.ShowTrajectory(transform.position, speed);
    }

    internal void OnEndDrag()
    {
        child.SetActive(true);
        gameObject.layer = 4;
        AddTorque();
        rigidbody.AddForce(speed, ForceMode.VelocityChange);
        playerLine.gameObject.SetActive(false);
    }

    internal void OnBeginDrag()
    {
        playerLine.gameObject.SetActive(true);
    }
}

