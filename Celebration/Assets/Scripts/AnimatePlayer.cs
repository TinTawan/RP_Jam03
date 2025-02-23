using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    PlayerMovement pMovement;
    Animator anim;


    private void Start()
    {
        anim = GetComponent<Animator>();
        pMovement = GetComponentInParent<PlayerMovement>();
    }


}
