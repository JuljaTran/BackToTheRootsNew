using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 2f;
    private int currentPointIndex = 0;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (points.Length == 0) return;

        Transform targetPoint = points[currentPointIndex];
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        Vector3 newPosition = Vector3.MoveTowards(rb.position, targetPoint.position, speed * Time.fixedDeltaTime);

        rb.MovePosition(newPosition);

        if (Vector3.Distance(rb.position, targetPoint.position) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % points.Length;
        }
    }
}
