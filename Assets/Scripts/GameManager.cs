using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Car _playerCar;
    public bool IsGamePaused { get; private set; }

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("GameManager is NULL!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        Cursor.visible = false;
    }

    private void Start()
    {
        FloorMover.Instance.ActivatePoolObjects(1);
        FloorMover.Instance.SpawnNextFloorTile(10);
    }
}

