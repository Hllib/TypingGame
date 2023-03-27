using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadGame()
    {
        //save stats
        Time.timeScale = 1f;
        LoadScene("Main");
    }

    public void LeaveGame()
    {
        //save stats
        Time.timeScale = 1f;
        LoadScene("Menu");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckPauseState();
        }
    }

    public void CheckPauseState()
    {
        _pauseMenu.SetActive(!_pauseMenu.activeSelf);

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

