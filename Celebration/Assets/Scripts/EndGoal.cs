using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGoal : MonoBehaviour
{
    PlayerMovement pMovement;
    bool playerIn, presentIn, won, doOnce = true;

    [SerializeField] ParticleSystem[] confettiPS;
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
        if (doOnce)
        {
            //Instantiate(confettiPS, transform.position, Quaternion.identity, transform);
            foreach(ParticleSystem ps in confettiPS)
            {
                ps.Play();
            }
            doOnce = false;
        }

        Time.timeScale = 0.75f;

        won = true;

        //stop player movement 
        if (pMovement != null)
        {
            pMovement.StopInputs();

        }

        yield return null;

    }
    public bool GetWon()
    {
        return won;
    }
}
