using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentAnim : MonoBehaviour
{
    Animator anim;
    Material presentMat;

    private void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetInteger("State", 1);

        presentMat = GetComponent<MeshRenderer>().materials[0];

        //Vector4 col = new(Random.Range(0,256), Random.Range(0, 256), Random.Range(0, 256), 255);
        Color col = Random.ColorHSV();
        presentMat.color = col;
    }

    public void SetState(int inInt)
    {
        anim.SetInteger("State", inInt);
    }
}
