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

        public bool ViexXInverted;
        public bool ViexYInverted;
    }

    #endregion
}
