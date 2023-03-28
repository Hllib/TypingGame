using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConstSettings
{
    public struct EasyDiff
    {
        public const float StartChaseSpeed = 12.0f;
        public const float StopChaseSpeed = 25.0f;
    }

    public struct MediumDiff
    {
        public const float StartChaseSpeed = 25.0f;
        public const float StopChaseSpeed = 35.0f;
    }

    public struct HardDiff
    {
        public const float StartChaseSpeed = 35.0f;
        public const float StopChaseSpeed = 45.0f;
    }
}
