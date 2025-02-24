using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopObstacle : MonoBehaviour
{
    Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            rb.useGravity = true;
            rb.isKinematic = false;
        }
    }
}
