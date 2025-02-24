using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabPresent : MonoBehaviour
{
    PlayerMovement player;
    Rigidbody rb;
    GameObject present;

    [SerializeField] int breakForce = 2000;

    bool canGrab;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GetComponentInParent<PlayerMovement>();

    }

    private void Update()
    {
        if (player.GetRightGrab() || player.GetLeftGrab())
        {
            if (canGrab)
            {
                FixedJoint handJoint = present.AddComponent<FixedJoint>();
                handJoint.connectedBody = rb;
                handJoint.breakForce = breakForce;

                canGrab = false;
            }
            

        }
        else if (!player.GetRightGrab() || !player.GetLeftGrab())
        {
            if(present != null)
            {
                Destroy(present.GetComponent<FixedJoint>());
            }

            present = null;
            
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log($"Enter trigger: {col.name}");
        if (col.CompareTag("Present"))
        {
            present = col.gameObject;
            canGrab = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Present"))
        {
            present = null;
            canGrab = false;

            if(col.TryGetComponent(out FixedJoint fj))
            {
                Destroy(fj);
            }

        }

    }
}
