using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Car _playerCar;
    [SerializeField]
    private GameObject _pauseMenu;
    public bool IsPaused { get; private set; }
    [SerializeField]
    private Keyboard _keyboard;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            CheckPauseState();
        }
    }

    public void CheckPauseState()
    {
        if (_pauseMenu != null)
        {
            switch (_pauseMenu.activeSelf)
            {
                case true:
                    StartPause();
                    break;
                case false:
                    StopPause();
                    break;
            }
        }
    }

    private void StopPause()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        IsPaused = false;
        _keyboard.enabled = true;
    }

    private void StartPause()
    {
        _keyboard.enabled = false;
        Time.timeScale = 0f;
        Cursor.visible = true;
        IsPaused = true;
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

