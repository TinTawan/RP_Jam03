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
        if(moveVect.magnitude != 0)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        //rGrab = pMovement.GetRightGrab();
        //lGrab = pMovement.GetLeftGrab();

        anim.SetBool("rGrab", pMovement.GetRightGrab());
        anim.SetBool("lGrab", pMovement.GetLeftGrab());
    }
}
