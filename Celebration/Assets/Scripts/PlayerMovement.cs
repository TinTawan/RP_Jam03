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
    ConfigurableJoint rootJoint;


    Vector2 moveVect;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float rotateSmooth = 1f;

    bool isGrounded;
    bool rightHandUp, leftHandUp;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rootJoint = GetComponent<ConfigurableJoint>();

        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        pControls = new PlayerControls();
        pControls.General.Enable();
        pControls.General.Move.performed += Move_performed;
        pControls.General.Move.canceled += Move_canceled;
        pControls.General.Jump.performed += Jump_performed;
        pControls.General.RightGrab.performed += RightGrab_performed;
        pControls.General.RightGrab.canceled += RightGrab_canceled;
        pControls.General.LeftGrab.performed += LeftGrab_performed;
        pControls.General.LeftGrab.canceled += LeftGrab_canceled;
    }

    private void LeftGrab_canceled(InputAction.CallbackContext ctx)
    {
        leftHandUp = ctx.ReadValueAsButton();
        Debug.Log($"Left: {leftHandUp}");
    }
    private void LeftGrab_performed(InputAction.CallbackContext ctx)
    {
        leftHandUp = ctx.ReadValueAsButton();
        Debug.Log($"Left: {leftHandUp}");

    }
    private void RightGrab_canceled(InputAction.CallbackContext ctx)
    {
        rightHandUp = ctx.ReadValueAsButton();
        Debug.Log($"Right: {rightHandUp}");

    }
    private void RightGrab_performed(InputAction.CallbackContext ctx)
    {
        rightHandUp = ctx.ReadValueAsButton();
        Debug.Log($"Right: {rightHandUp}");

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

        //rotate player to face forward when moving
        if (moveVect != Vector2.zero)
        {
            Quaternion rot = Quaternion.Euler(0, cam.eulerAngles.y + 90, 0);
            Quaternion inv = Quaternion.Inverse(rot);

            Quaternion lerp = Quaternion.Lerp(transform.rotation, inv, Time.fixedDeltaTime * rotateSmooth);

            //rb.MoveRotation(lerp);
            rootJoint.targetRotation = inv;


            rb.AddForce(dir * moveSpeed, ForceMode.Acceleration);   
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


    public Vector2 GetMoveVect()
    {
        return moveVect;
    }

    public bool GetRightGrab()
    {
        return rightHandUp;
    }
    public bool GetLeftGrab()
    {
        return leftHandUp;
    }


    private void OnDisable()
    {
        pControls.General.Disable();
    }

}
