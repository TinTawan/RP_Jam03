using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    bool playerIn, presentIn;

    private void Update()
    {
        if(playerIn && presentIn)
        {
            Debug.Log("WIN");
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerIn = true;
        }

        if (col.CompareTag("Present"))
        {
            presentIn = true;
        }
    }
}
