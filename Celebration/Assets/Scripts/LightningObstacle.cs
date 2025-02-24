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


        if(dangerRed.color.a == 225)
        {
            Debug.Log("STRIKE");
            StartCoroutine(Strike());
        }
    }

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
