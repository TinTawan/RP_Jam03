using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateJoints : MonoBehaviour
{
    private ConfigurableJoint joint;

    [SerializeField] private Transform targetJoint;
    [SerializeField] private Quaternion initRotation;

    private void Start()
    {
        joint = GetComponent<ConfigurableJoint>();

        initRotation = joint.transform.localRotation;
    }

    private void FixedUpdate()
    {
        SetTargetRotation(joint, targetJoint.localRotation, initRotation, Space.Self);
    }

    //rotate the joints to copy the animated model's movements
    void SetTargetRotation(ConfigurableJoint inJoint, Quaternion inTargetRot, Quaternion inInitRot, Space inSpace)
    {
        var right = inJoint.axis;
        var forward = Vector3.Cross(inJoint.axis, inJoint.secondaryAxis).normalized;
        var up = Vector3.Cross(forward, right).normalized;
        Quaternion worldtoJointSpace = Quaternion.LookRotation(forward, up);

        Quaternion resultRot = Quaternion.Inverse(worldtoJointSpace);

        /*if(inSpace == Space.World)
        {
            resultRot *= inInitRot * Quaternion.Inverse(inTargetRot);
            
        }
        else
        {
            resultRot *= Quaternion.Inverse(inTargetRot) * inInitRot;

        }*/

        resultRot *= Quaternion.Inverse(inTargetRot) * inInitRot;

        resultRot *= worldtoJointSpace;

        inJoint.targetRotation = resultRot;
    }
}
