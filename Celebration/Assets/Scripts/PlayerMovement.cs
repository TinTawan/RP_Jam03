using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerControls pControls;
    Rigidbody rb;
    Transform cam;

    Vector2 moveVect;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float rotateSmooth = 1f;

    bool isGrounded;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        pControls = new PlayerControls();
        pControls.General.Enable();
        pControls.General.Move.performed += Move_performed;
        pControls.General.Move.canceled += Move_canceled;
        pControls.General.Jump.performed += Jump_performed;

        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Move_performed(InputAction.CallbackContext ctx)
    {
        moveVect = ctx.ReadValue<Vector2>();
    }
    private void Move_canceled(InputAction.CallbackContext ctx)
    {
        moveVect = Vector2.zero;
    }
    private void Jump_performed(InputAction.CallbackContext ctx)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        /*Vector3 dir = (cam.forward.normalized * moveVect.y) + (cam.right.normalized * moveVect.x) + (Vector3.up * vertMove);
        rb.AddForce(dir * moveForce, ForceMode.Force);*/

        Vector3 dir = (cam.forward.normalized * moveVect.y) + (cam.right.normalized * moveVect.x);
        rb.AddForce(dir * moveSpeed, ForceMode.Acceleration);

        //rotate player to face forward when moving
        if (moveVect != Vector2.zero)
        {
            Quaternion rot = Quaternion.Euler(0, cam.eulerAngles.y, 0);

            Quaternion lerp = Quaternion.Lerp(transform.rotation, rot, Time.fixedDeltaTime * rotateSmooth);
            //Quaternion inv = Quaternion.Inverse(rot);

            //transform.rotation = rot;
            //rb.rotation = rot;
            rb.MoveRotation(lerp);
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit(Collision col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }


    private void OnDisable()
    {
        pControls.General.Disable();
    }

}
