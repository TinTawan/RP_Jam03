using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    PlayerMovement pMovement;
    Rigidbody rb;

    [SerializeField] float impulseForce = 50f, ragdollTime = 1f;

    bool hit;

    private void Start()
    {
        pMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        if (hit)
        {
            StartCoroutine(HitByObstacle());
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Obstacle"))
        {
            Debug.Log($"Hit by: {col.gameObject.name}");
            hit = true;
        }
    }


    IEnumerator HitByObstacle()
    {
        hit = false;

        rb.AddForce(Vector3.up * impulseForce, ForceMode.VelocityChange);
        pMovement.SetPlayerRagdoll(true);
        pMovement.SetGrabbing(false);

        yield return new WaitForSeconds(ragdollTime);

        pMovement.SetPlayerRagdoll(false);

    }
}
