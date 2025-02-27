using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollStabiliser : MonoBehaviour
{
    [SerializeField] private Rigidbody spineRB;

    [SerializeField] bool activateForce;
    [SerializeField] float forceVal = 2f;

    private void Start()
    {
        activateForce = true;
    }

    private void FixedUpdate()
    {
        if (activateForce)
        {
            spineRB.AddForce(Vector3.up * forceVal * Time.deltaTime);


        }
    }

    public void SetActivateForce(bool inBool)
    {
        activateForce = inBool;
    }

    public void SetForceVal(float inVal)
    {
        forceVal = inVal;
    }

}
