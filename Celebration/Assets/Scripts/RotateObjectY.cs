using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectY : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = RandYRot(Quaternion.identity, 180);
    }

    Quaternion RandYRot(Quaternion curRot, float delta)
    {
        Vector3 euler = curRot.eulerAngles;

        float zRot = Random.Range(euler.z - delta, euler.z + delta);

        return Quaternion.Euler(-90, 0, zRot);
    }
}
