using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : MonoBehaviour
{
    public float delay = 3f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // damit es zuerst nicht fällt
        Invoke("ActivateGravity", delay);
    }

    void ActivateGravity()
    {
        rb.isKinematic = false; // jetzt aktiviert es die Physik
    }
}