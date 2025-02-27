using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFObstacle : MonoBehaviour
{

    bool inside;
    PlayerMovement pMove;
    List<Rigidbody> playerRbs = new List<Rigidbody>();

    [SerializeField] float floatForce = 10f, floatTime = 3f;

    private void Update()
    {
        if (inside && pMove != null)
        {
            foreach(Rigidbody rb in playerRbs)
            {
                rb.AddForce(Vector3.up * floatForce, ForceMode.Acceleration);
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            inside = true;

            if (col.TryGetComponent(out PlayerMovement pm))
            {
                pMove = pm;
            }

            StartCoroutine(FloatPlayer());

            playerRbs.Add(col.GetComponent<Rigidbody>());

            FindObjectOfType<AudioManager>().PlaySound(AudioManager.soundType.alien, transform.position, 0.25f);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            inside = false;

        }

        playerRbs.Clear();
    }

    IEnumerator FloatPlayer()
    {
        if(pMove != null)
        {
            pMove.SetPlayerRagdoll(true);
            GetComponentInChildren<CapsuleCollider>().enabled = false;

            yield return new WaitForSeconds(floatTime);

            inside = false;
            pMove.SetPlayerRagdoll(false);

            yield return new WaitForSeconds(floatTime);

            GetComponentInChildren<CapsuleCollider>().enabled = true;
        }
        
    }
}
