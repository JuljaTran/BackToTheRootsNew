using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveItem : MonoBehaviour
{
    public float speed = 2f;
    public float distance = 5f;

    private Vector3 startPos;
    private Vector3 lastPos;
    public Vector3 deltaMove { get; private set; }

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // WICHTIG
        startPos = transform.position;
        lastPos = transform.position;
    }

    private void FixedUpdate()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);
        Vector3 newPos = startPos + Vector3.right * movement;

        deltaMove = newPos - lastPos;
        lastPos = newPos;

        rb.MovePosition(newPos); // Physikalische Bewegung!
    }
}