using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Keyboard")]
    [field: SerializeField] public EventReference correctHit { get; private set; }
    [field: SerializeField] public EventReference missedHit { get; private set; }
    [field: SerializeField] public EventReference music { get; private set; }
    private static FMODEvents _instance;

    public static FMODEvents Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("FMODEvents is NULL :: FMODEvents.cs");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
}
