using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _stars;
    private bool _isDifficultySet;

    private int _currentDifficulty;

    private void Start()
    {
        _currentDifficulty = PlayerPrefs.GetInt("Difficulty", 0);
        _currentDifficulty = 0;
        SetDifficulty(_currentDifficulty);
    }

    public void HideStarsVisualFeedback(int starId)
    {
        for (int i = 0; i < starId; i++)
        {
            if (!_stars[i].GetComponent<DifficultyStar>().isChecked)
            {
                _stars[i].GetComponent<Image>().color = new Color(0, 130 / 255f, 168 / 255f);
            }
        }
    }

    public void ShowStarsVisualFeedback(int starId)
    {
        for (int i = 0; i < starId; i++)
        {
            _stars[i].GetComponent<Image>().color = new Color(217 / 255f, 168 / 255f, 19 / 255f);
        }
    }

    public void SetDifficulty(int starId)
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            _stars[i].GetComponent<Image>().color = new Color(0, 130 / 255f, 168 / 255f);
        }

        ShowStarsVisualFeedback(starId);

        for (int i = 0; i < starId; i++)
        {
            _stars[i].GetComponent<DifficultyStar>().isChecked = true;
        }

        PlayerPrefs.SetInt("Difficulty", starId);
        _currentDifficulty = starId;
        Debug.Log("Chosen difficulty: " + _currentDifficulty);
    }
}
