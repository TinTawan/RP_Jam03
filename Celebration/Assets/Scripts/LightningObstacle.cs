using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningObstacle : MonoBehaviour
{
    ParticleSystem lightningPS;
    SphereCollider areaCol;
    Material dangerRed;

    [SerializeField] float strikeTimer = 5f;
    [SerializeField] bool strike;

    private void Start()
    {
        areaCol = GetComponent<SphereCollider>();
        lightningPS = GetComponentInChildren<ParticleSystem>();
        dangerRed = GetComponentInChildren<MeshRenderer>().material;

        areaCol.enabled = false;
        lightningPS.Stop();
        dangerRed.color = Color.clear;

        strike = false;
    }

    private void Update()
    {
        if (strike)
        {
            LerpToRed();
        }
    }

    void LerpToRed()
    {
        dangerRed.color = Color.Lerp(dangerRed.color, Color.red, Time.deltaTime * strikeTimer);

        /*float alpha = dangerRed.color.a;
        alpha = Mathf.Lerp(alpha, 1, Time.deltaTime * strikeTimer);
        dangerRed.color = new(255, 0, 0, alpha);*/

        if(dangerRed.color == Color.red)
        {
            Debug.Log("STRIKE");
            StartCoroutine(Strike());
        }
    }

    /*void Strike()
    {
        lightningPS.Play();
        areaCol.enabled = true;

        strike = false;
    }*/

    IEnumerator Strike()
    {
        lightningPS.Play();
        areaCol.enabled = true;
        strike = false;

        yield return new WaitForSeconds(1f);

        Debug.Log("stop strike");

        dangerRed.color = Color.clear;
        lightningPS.Stop();
        areaCol.enabled = false;
    }
}
