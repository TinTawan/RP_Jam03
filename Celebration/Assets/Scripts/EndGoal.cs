using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    PlayerMovement pMovement;
    bool playerIn, presentIn, won;

    private void Start()
    {
        pMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(playerIn && presentIn)
        {
            StartCoroutine(Win());
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

    IEnumerator Win()
    {
        //Debug.Log("WIN");
        Time.timeScale = 0.75f;

        won = true;

        //stop player movement 
        if(pMovement != null)
        {
            pMovement.StopInputs();

        }

        yield return new WaitForSeconds(2f);

        //show win screen
    }
    public bool GetWon()
    {
        return won;
    }
}
