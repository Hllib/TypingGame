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
    [SerializeField] private GameObject _playButton;

    [Header("WPM")]
    [SerializeField] private TextMeshProUGUI _currentWPM;
    [SerializeField] private TextMeshProUGUI _bestWPM;
    [SerializeField] private TextMeshProUGUI _totalWPM;
    [Header("Accuracy")]
    [SerializeField] private TextMeshProUGUI _currentAccuracy;
    [SerializeField] private TextMeshProUGUI _bestAccuracy;
    [SerializeField] private TextMeshProUGUI _totalAccuracy;
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

    public void UpdateWPM(double wpm)
    {
        _wordsPerMinute.text = $"wpm : {wpm}";
    }

    public void ShowPlayBt(bool state)
    {
        _playButton.SetActive(state);
    }

    public void UpdateStats(((double, double, double), (float, float, float), (double, double, double)) statistics)
    {
        _currentWPM.text = statistics.Item1.Item1.ToString();
        _bestWPM.text = statistics.Item1.Item2.ToString();
        _totalWPM.text = statistics.Item1.Item3.ToString();

        _currentAccuracy.text = statistics.Item2.Item1.ToString();
        _bestAccuracy.text = statistics.Item2.Item2.ToString();
        _totalAccuracy.text = statistics.Item2.Item3.ToString();

        _currentTime.text = statistics.Item3.Item1.ToString();
        _bestTime.text = statistics.Item3.Item2.ToString();
        _totalTime.text = statistics.Item3.Item3.ToString();
    }
}
