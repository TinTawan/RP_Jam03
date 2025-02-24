using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    PlayerMovement pMovement;
    Rigidbody rb;

    [SerializeField] float impulseForceMin = 50f, impulseForceMax = 200f;
    [SerializeField] float rTimeMin = 0.5f, rTimeMax = 5f;

    float impulse = 75f, ragTime = 1f;

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
        SetRagdollLengthImpulse();

        rb.AddForce(Vector3.up * impulse, ForceMode.VelocityChange);
        pMovement.SetPlayerRagdoll(true);

        yield return new WaitForSeconds(ragTime + 1f);

        pMovement.SetPlayerRagdoll(false);

    }

    void SetRagdollLengthImpulse()
    {
        ragTime = Random.Range(rTimeMin, rTimeMax);
        Debug.Log($"Ragdoll Time: {ragTime}");

        float interp = Mathf.InverseLerp(rTimeMin, rTimeMax, ragTime);
        impulse = Mathf.Lerp(impulseForceMin, impulseForceMax, interp);

        //impulse = interp * (impulseForceMax - impulseForceMin);

        Debug.Log($"Impulse Val: {impulse}");
    }

}
