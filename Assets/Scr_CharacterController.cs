using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private DefaultInput defaultInput;
    private Vector2 input_Movement;
    private Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform cameraHolder;
    public Transform feetTransform;

    [Header("Settings")]
    public Scr_Models.PlayerSettingsModel playerSettings;
    public float viewClampYMin;
    public float viewClampYMax;
    public LayerMask playerMask;

    [Header("Gravity")]
    public float gravityAmount;
    public float gravityMin;
    private float playerGravity;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    [Header("Stance")]
    public Scr_Models.PlayerStance playerStance;
    public float playerStartsSmothing;
    public Scr_Models.CharacterStance playerStandStance;
    public Scr_Models.CharacterStance playerCrouchStance;
    public Scr_Models.CharacterStance playerProneStance;
    private float stanceCheckErrorMargin = 0.05f;
    private float cameraHeight;
    private float cameraHeightVelocity;

    private Vector3 stanceCapsuleCenterVelocity;
    private float stanceCapsuleHeightVelocity;

    private void Awake()
    {
        defaultInput = new DefaultInput();

        //Llama al new input system
        defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();
        defaultInput.Character.Jump.performed += e => Jump();

        defaultInput.Character.Crouch.performed += e => Crouch();
        defaultInput.Character.Prone.performed += e => Prone();

        defaultInput.Enable();

        newCameraRotation = cameraHolder.localRotation.eulerAngles;
        newCharacterRotation = transform.localRotation.eulerAngles;

        characterController = GetComponent<CharacterController>();

        cameraHeight = cameraHolder.localPosition.y;
    }

    private void Update() 
    {
        CalculateView();
        CalculateMovement();
        CalculateJump();
        CalculateStance();
    }

    private void CalculateView()
    {
        newCharacterRotation.y += playerSettings.ViewXSensitivity * (playerSettings.ViewXInverted ? -input_View.x : input_View.x) * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(newCharacterRotation);

        newCameraRotation.x += playerSettings.ViewYSensitivity * (playerSettings.ViewYInverted? input_View.y : -input_View.y) * Time.deltaTime;
        newCameraRotation.x = Mathf.Clamp(newCameraRotation.x, viewClampYMin, viewClampYMax); 

        cameraHolder.localRotation = Quaternion.Euler(newCameraRotation);
    }

    private void CalculateMovement()
    {
        var verticalSpeed = playerSettings.WalkingForwardSpeed * input_Movement.y * Time.deltaTime;
        var horizontalSpeed= playerSettings.WalkingStrafeSpeed * input_Movement.x * Time.deltaTime;

        var newMovementSpeed = new Vector3(horizontalSpeed, 0, verticalSpeed);
        newMovementSpeed = transform.TransformDirection(newMovementSpeed);

        if (playerGravity > gravityMin)
        {
            playerGravity -= gravityAmount * Time.deltaTime;
        }

        if (playerGravity < -0.1f && characterController.isGrounded) 
        {
            playerGravity = -0.1f;
        }

        newMovementSpeed.y += playerGravity;
        newMovementSpeed += jumpingForce * Time.deltaTime;

        characterController.Move(newMovementSpeed);
    }

    private void CalculateJump()
    {
        jumpingForce = Vector3.SmoothDamp(jumpingForce, Vector3.zero, ref jumpingForceVelocity, playerSettings.JumpingFalloff);
    }

    private void CalculateStance()
    {
        var currentStance = playerStandStance;

        if (playerStance == Scr_Models.PlayerStance.Crouch)
        {
            currentStance = playerCrouchStance;
        }
        else if (playerStance == Scr_Models.PlayerStance.Prone) 
        {
            currentStance = playerProneStance;
        }

        cameraHeight = Mathf.SmoothDamp(cameraHolder.localPosition.y, currentStance.CameraHeight, ref cameraHeightVelocity, playerStartsSmothing);
        cameraHolder.localPosition = new Vector3(cameraHolder.localPosition.x, cameraHeight, cameraHolder.localPosition.z);

        characterController.height = Mathf.SmoothDamp(characterController.height, currentStance.StanceCollider.height, ref stanceCapsuleHeightVelocity, playerStartsSmothing);
        characterController.center = Vector3.SmoothDamp(characterController.center, currentStance.StanceCollider.center, ref stanceCapsuleCenterVelocity, playerStartsSmothing);
    }

    private void Jump()
    {
        if (!characterController.isGrounded) 
        {
            return;        
        }

        jumpingForce = Vector3.up * playerSettings.JumpingHeight;
        playerGravity = 0;
    }

    private void Crouch()
    {
        if (playerStance == Scr_Models.PlayerStance.Crouch)
        {
            if (StanceCheck(playerStandStance.StanceCollider.height))
            {
                return;
            }


            playerStance = Scr_Models.PlayerStance.Stand;
            return;
        }

        if (StanceCheck(playerCrouchStance.StanceCollider.height))
        {
            return;
        }

        playerStance = Scr_Models.PlayerStance.Crouch;
    }

    private void Prone()
    {
        playerStance = Scr_Models.PlayerStance.Prone;
    }

    private bool StanceCheck(float stanceCheckHeight)
    {
        var start = new Vector3(feetTransform.position.x, feetTransform.position.y + characterController.radius + stanceCheckErrorMargin, feetTransform.position.z);
        var end = new Vector3(feetTransform.position.x, feetTransform.position.y - characterController.radius - stanceCheckErrorMargin + stanceCheckHeight, feetTransform.position.z);

        return Physics.CheckCapsule(start, end, characterController.radius, playerMask);
    }
}