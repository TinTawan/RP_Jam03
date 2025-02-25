using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningObstacle : MonoBehaviour
{
    ParticleSystem lightningPS;
    SphereCollider areaCol;
    Material dangerRed;

    [SerializeField] float strikeLerpTime = 5f, maxStrikeTimer = 16f;
    
    bool strike;

    float timer;


    private void Start()
    {
        areaCol = GetComponent<SphereCollider>();
        lightningPS = GetComponentInChildren<ParticleSystem>();
        dangerRed = GetComponentInChildren<MeshRenderer>().material;

        areaCol.enabled = false;
        lightningPS.Stop();
        dangerRed.color = Color.clear;

        strike = true;

        timer = Random.Range(8, 14);
    }

    private void Update()
    {
        if (strike)
        {
            LerpToRed();
        }
        else
        {
            RandomiseStrike();
        }
    }

    void LerpToRed()
    {
        dangerRed.color = Color.Lerp(dangerRed.color, Color.red, Time.deltaTime * strikeLerpTime);


        if(dangerRed.color == Color.red)
        {
            //Debug.Log("STRIKE");
            StartCoroutine(Strike());
        }
    }

    IEnumerator Strike()
    {
        lightningPS.Play();
        areaCol.enabled = true;
        strike = false;

        yield return new WaitForSeconds(1.5f);

        //Debug.Log("stop strike");

        dangerRed.color = Color.clear;
        lightningPS.Stop();
        areaCol.enabled = false;
    }

    void RandomiseStrike()
    {
        if(timer <= 0)
        {
            strike = true;
            timer = Random.Range(4, maxStrikeTimer);
        }
        else
        {
            timer -= Time.deltaTime;

        }
    }
}
