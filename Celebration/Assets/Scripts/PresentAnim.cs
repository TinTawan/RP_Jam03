using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentAnim : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        SetState(1);
    }

    public void SetState(float inVal)
    {
        anim.SetFloat("state", inVal);
    }
}
