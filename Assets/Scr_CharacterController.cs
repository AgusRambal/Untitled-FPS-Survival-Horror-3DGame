using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_CharacterController : MonoBehaviour
{
    private CharacterController characterController;
    private DefaultInput defaultInput;
    public Vector2 input_Movement;
    public Vector2 input_View;

    private Vector3 newCameraRotation;
    private Vector3 newCharacterRotation;

    [Header("References")]
    public Transform cameraHolder;

    [Header("Settings")]
    public Scr_Models.PlayerSettingsModel playerSettings;
    public float viewClampYMin;
    public float viewClampYMax;

    [Header("Gravity")]
    public float gravityAmount;
    public float gravityMin;
    private float playerGravity;

    public Vector3 jumpingForce;
    private Vector3 jumpingForceVelocity;

    [Header("Stance")]
    public Scr_Models.PlayerStance playerStance;
    public float playerStartsSmothing;
    public float cameraStandingHeight;
    public float cameraCrouchingHeight;
    public float cameraProneingHeight;

    private float cameraHeight;
    private float cameraHeightVelocity;

    private void Awake()
    {
        defaultInput = new DefaultInput();

        //Llama al new input system
        defaultInput.Character.Movement.performed += e => input_Movement = e.ReadValue<Vector2>();
        defaultInput.Character.View.performed += e => input_View = e.ReadValue<Vector2>();
        defaultInput.Character.Jump.performed += e => Jump();

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
        CalculateCameraHeight();
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

    private void CalculateCameraHeight()
    {
        var stanceHeight = cameraStandingHeight;

        if (playerStance == Scr_Models.PlayerStance.Crouch)
        {
            stanceHeight = cameraCrouchingHeight;
        }
        else if (playerStance == Scr_Models.PlayerStance.Prone) 
        {
            stanceHeight = cameraProneingHeight;
        }

        cameraHeight = Mathf.SmoothDamp(cameraHolder.localPosition.y, stanceHeight, ref cameraHeightVelocity, playerStartsSmothing);
        cameraHolder.localPosition = new Vector3(cameraHolder.localPosition.x, cameraHeight, cameraHolder.localPosition.z);
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
}