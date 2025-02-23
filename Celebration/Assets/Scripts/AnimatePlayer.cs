using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatePlayer : MonoBehaviour
{
    PlayerMovement pMovement;
    PlayerControls pc;
    Animator anim;

    bool rGrab, lGrab;

    private void Start()
    {
        anim = GetComponent<Animator>();
        pMovement = GetComponentInParent<PlayerMovement>();

    }

    private void Update()
    {
        MoveAnims();
    }

    void MoveAnims()
    {
        if (pMovement == null) return;
        Vector2 moveVect = pMovement.GetMoveVect();

        //rGrab = pMovement.GetRightGrab();
        //lGrab = pMovement.GetLeftGrab();

        anim.SetBool("rGrab", pMovement.GetRightGrab());
        anim.SetBool("lGrab", pMovement.GetLeftGrab());
    }
}
