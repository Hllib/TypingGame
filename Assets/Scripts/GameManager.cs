using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerCar _playerCar;
    [SerializeField]
    private GameObject _pauseMenu;
    public bool IsPaused { get; private set; }
    [SerializeField]
    private Keyboard _keyboard;

    private static int CurrentScore;
    private static int BestScore;
    private static int TotalScore;

    private static int CurrentWordsCount;
    private static int BestWordsCount;
    private static int TotalWordsCount;

    private static float CurrentTime;
    private static float BestTime;
    private static float TotalTime;

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

    public void LoadStats()
    {
        CurrentWordsCount = 0;
        BestWordsCount = PlayerPrefs.GetInt("BestWordsCount", 0);
        TotalWordsCount = PlayerPrefs.GetInt("TotalWordsCount", 0);
        ValueTuple<int, int, int> wordsCountStats = (CurrentWordsCount, BestWordsCount, TotalWordsCount);

        CurrentScore = 0;
        BestScore = PlayerPrefs.GetInt("BestScore", 0);
        TotalScore = PlayerPrefs.GetInt("TotalScore", 0);
        ValueTuple<int, int, int> scoretStats = (CurrentScore, BestScore, TotalScore);

        CurrentTime = 0;
        BestTime = PlayerPrefs.GetFloat("BestTime", 0);
        TotalTime = PlayerPrefs.GetFloat("TotalTime", 0);
        ValueTuple<float, float, float> timeStats = (CurrentTime, BestTime, TotalTime);

        var stats = (scoretStats, wordsCountStats, timeStats);

        UIManager.Instance.UpdateStats(stats);
    }

    public void UpdateStats(int currentScore, int currentWordsCount, float currentTime)
    {
        int scoreSurplus = currentScore - CurrentScore;
        int wordsSurplus = currentWordsCount - CurrentWordsCount;
        float timeSurplus = currentTime - CurrentTime;

        CurrentScore = currentScore;
        CurrentWordsCount = currentWordsCount;
        CurrentTime = currentTime;

        BestScore = CurrentScore > BestScore ? CurrentScore : BestScore;
        BestWordsCount = CurrentWordsCount > BestWordsCount ? CurrentWordsCount : BestWordsCount;
        BestTime = CurrentTime > BestTime ? CurrentTime : BestTime;

        TotalScore += scoreSurplus;
        TotalWordsCount += wordsSurplus;
        TotalTime += timeSurplus;

        ValueTuple<int, int, int> wordsCountStats = (CurrentWordsCount, BestWordsCount, TotalWordsCount);
        ValueTuple<int, int, int> scoretStats = (CurrentScore, BestScore, TotalScore);
        ValueTuple<float, float, float> timeStats = (CurrentTime, BestTime, TotalTime);

        var stats = (scoretStats, wordsCountStats, timeStats);

        UIManager.Instance.UpdateStats(stats);
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("BestWordsCount", BestWordsCount);
        PlayerPrefs.SetInt("TotalWordsCount", TotalWordsCount);

        PlayerPrefs.SetInt("BestScore", BestScore);
        PlayerPrefs.SetInt("TotalScore", TotalScore);

        PlayerPrefs.SetFloat("BestTime", BestTime);
        PlayerPrefs.SetFloat("TotalTime", TotalTime);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadGame()
    {
        SaveStats();
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
        FloorMover.Instance.SpawnNextFloorTile(5);
        LoadStats();
    }
}

