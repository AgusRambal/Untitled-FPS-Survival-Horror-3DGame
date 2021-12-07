using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scr_Models
{
    #region - Player -

    public enum PlayerStance 
    { 
        Stand, 
        Crouch,
        Prone
    }

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float ViewXSensitivity;
        public float ViewYSensitivity;

        public bool ViewXInverted;
        public bool ViewYInverted;

        [Header("Movement settings")]
        public bool sprintingHold;
        public float MovementSmoothing;

        [Header("Movement - Running")]
        public float runningForwardSpeed;
        public float runningStrafeSpeed;

        [Header("Movement - Walking")]
        public float WalkingForwardSpeed;
        public float WalikingStrafSpeed;
        public float WalkingStrafeSpeed;

        [Header("Jump")]
        public float JumpingHeight;
        public float JumpingFalloff;
        public float FallingSmoothing;

        [Header("Speed Effectors")]
        public float SpeedEffector = 1;
        public float CrouchSpeedEffector;
        public float ProneSpeedEfector;
        public float FallingSpeedEffector;
    }

    [Serializable]
    public class CharacterStance
    { 
        public float CameraHeight;
        public CapsuleCollider StanceCollider;

    }

    #endregion

    #region - Weapons -

    [Serializable]
    public class WeaponsSettingsModel 
    {
        [Header("Weapon Sway")]
        public float SwayAmount;
        public bool SwayYInverted;
        public bool SwayXInverted;
        public float SwaySmoothing;
        public float SwayResetSmoothing;
        public float SwayClampX;
        public float SwayClampY;

        [Header("Weapon Movement Sway")]
        public float MovementSwayX;
        public float MovementSwayY;
        public bool MovementSwayYInverted;
        public bool MovementSwayXInverted;
        public float MovementSwaySmoothing;
    }

    #endregion
}