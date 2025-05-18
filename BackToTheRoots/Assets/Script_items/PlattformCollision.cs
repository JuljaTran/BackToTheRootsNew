using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformParenting : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            other.transform.SetParent(null);
        }
    }

}
