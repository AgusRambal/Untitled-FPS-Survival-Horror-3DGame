using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Scr_Models
{
    #region - Player -

    [Serializable]
    public class PlayerSettingsModel
    {
        [Header("View Settings")]
        public float ViewXSensitivity;
        public float ViewYSensitivity;

        public bool ViewXInverted;
        public bool ViewYInverted;

        [Header("Movement")]
        public float WalkingForwardSpeed;
        public float WalikingStrafSpeed;
        public float WalkingStrafeSpeed;

        [Header("Jump")]
        public float JumpingHeight;
        public float JumpingFalloff;
    }

    #endregion
}