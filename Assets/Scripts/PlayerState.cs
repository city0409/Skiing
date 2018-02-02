using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerState
{
    public bool IsSkiing { get; set; }

    public bool IsJump { get; set; }

    public bool IsRoll { get; set; }

    public bool IsRideSnowMan { get; set; }

    public bool IsLie { get; set; }

    public bool IsGround { get; set; }

}