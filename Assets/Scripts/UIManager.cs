using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _countdown;
    [SerializeField] private TextMeshProUGUI _wordsPerMinute;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI _currentScore;
    [SerializeField] private TextMeshProUGUI _bestScore;
    [SerializeField] private TextMeshProUGUI _totalScore;
    [Header("Words Count")]
    [SerializeField] private TextMeshProUGUI _currentWordsCount;
    [SerializeField] private TextMeshProUGUI _bestWordsCount;
    [SerializeField] private TextMeshProUGUI _totalWordsCount;
    [Header("Time")]
    [SerializeField] private TextMeshProUGUI _currentTime;
    [SerializeField] private TextMeshProUGUI _bestTime;
    [SerializeField] private TextMeshProUGUI _totalTime;


    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UI Manager is NULL");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void StartCountDown()
    {
        _countdown.SetActive(true);
    }

    public void StopCountDown()
    {
        _countdown.SetActive(false);
    }

    public void UpdateWPM(float wpm)
    {
        _wordsPerMinute.text = $"wpm : {wpm}";
    }

    public void UpdateStats(((int, int, int), (int, int, int), (float, float, float)) statistics)
    {
        _currentScore.text = statistics.Item1.Item1.ToString();
        _bestScore.text = statistics.Item1.Item2.ToString();
        _totalScore.text = statistics.Item1.Item3.ToString();

        _currentWordsCount.text = statistics.Item2.Item1.ToString();
        _bestWordsCount.text = statistics.Item2.Item2.ToString();
        _totalWordsCount.text = statistics.Item2.Item3.ToString();

        _currentTime.text = statistics.Item3.Item1.ToString();
        _bestTime.text = statistics.Item3.Item2.ToString();
        _totalTime.text = statistics.Item3.Item3.ToString();
    }
}
