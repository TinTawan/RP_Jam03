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
    RagdollStabiliser stabiliser;

    [SerializeField] LayerMask groundLayer;
    [SerializeField] float groundCheckDist;

    Vector2 moveVect;
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 1f;
    [SerializeField] float rotateSmooth = 1f;

    bool isGrounded;
    bool rightHandUp, leftHandUp;

    ConfigurableJoint[] bodyJoints;
    [SerializeField] float slerpDrive = 4000f;
    [SerializeField] bool ragdoll = true;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rootJoint = GetComponent<ConfigurableJoint>();
        stabiliser = GetComponentInChildren<RagdollStabiliser>();
        bodyJoints = GetComponentsInChildren<ConfigurableJoint>();

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
        //Debug.Log($"Left: {leftHandUp}");
    }
    private void LeftGrab_performed(InputAction.CallbackContext ctx)
    {
        leftHandUp = ctx.ReadValueAsButton();
        //Debug.Log($"Left: {leftHandUp}");

    }
    private void RightGrab_canceled(InputAction.CallbackContext ctx)
    {
        rightHandUp = ctx.ReadValueAsButton();
        //Debug.Log($"Right: {rightHandUp}");

    }
    private void RightGrab_performed(InputAction.CallbackContext ctx)
    {
        rightHandUp = ctx.ReadValueAsButton();
        //Debug.Log($"Right: {rightHandUp}");

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

    private void Update()
    {
        GroundCheck();
    }
    private void FixedUpdate()
    {
        Move();

        //SetRagdollPlayer(ragdoll);
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


    void GroundCheck()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit lineHit, groundCheckDist, groundLayer))
        {
            isGrounded = true;
            stabiliser.SetActivateForce(true);
        }
        else
        {
            isGrounded = false;
            stabiliser.SetActivateForce(false);

        }
        Vector3 end = new(transform.position.x, transform.position.y - groundCheckDist, transform.position.z);
        Debug.DrawLine(transform.position, end, Color.red);
    }

    void SetRagdollPlayer(bool isRagdoll)
    {
        if (isRagdoll)
        {
            stabiliser.SetActivateForce(false);
            SetSlerpDrive(0);
        }
        else
        {
            stabiliser.SetActivateForce(true);
            SetSlerpDrive(slerpDrive);
        }
        
    }

    void SetSlerpDrive(float inVal)
    {
        foreach(ConfigurableJoint cj in bodyJoints)
        {
            JointDrive drive = new JointDrive();
            cj.slerpDrive = drive;
            drive.positionSpring = inVal;
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
