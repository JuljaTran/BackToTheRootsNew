using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class FallingItem : MonoBehaviour
{
    public float delayBeforeFall = 3f;
    public string playerTag = "Player";

    private float timer = 0f;
    private bool playerOnPlatform = false;
    private bool hasFallen = false;

    private Rigidbody rb;

    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Startzustand: fest

        // Originalposition & Rotation merken
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (playerOnPlatform && !hasFallen)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeFall)
            {
                Fall();
            }
        }
    }

    private void Fall()
    {
        Debug.Log("Plattform fällt jetzt!");
        rb.isKinematic = false;
        hasFallen = true;

        // Plattform nach 10 Sekunden zurücksetzen
        Invoke(nameof(ResetPlatform), 10f);
    }

    private void ResetPlatform()
    {
        Debug.Log("Plattform wird zurückgesetzt.");

        // Position und Rotation zurücksetzen
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // Rigidbody wieder kinematic machen (fest)
        rb.isKinematic = true;

        // Status zurücksetzen
        hasFallen = false;
        timer = 0f;
        playerOnPlatform = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player hat Plattform betreten");
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            Debug.Log("Player hat Plattform verlassen");
            playerOnPlatform = false;
            timer = 0f;
        }
    }
}
