using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConstSettings
{
    public struct EasyDiff
    {
        public const float MinSpeed = 15.0f;
        public const float MaxSpeed = 25.0f;
    }

    public struct MediumDiff
    {
        public const float MinSpeed = 25.0f;
        public const float MaxSpeed = 35.0f;
    }

    public struct HardDiff
    {
        public const float MinSpeed = 30.0f;
        public const float MaxSpeed = 45.0f;
    }
}
