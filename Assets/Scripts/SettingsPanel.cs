using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _stars;
    [SerializeField]
    private bool _isChosenFirstTime;
    private int _currentDifficulty;

    private void Start()
    {
        _currentDifficulty = PlayerPrefs.GetInt("Difficulty", 0);
        ShowStarsVisualFeedback(_currentDifficulty);

        for (int i = 0; i < _currentDifficulty; i++)
        {
            _stars[i].GetComponent<DifficultyStar>().isChecked = true;
        }
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
            _stars[i].GetComponent<DifficultyStar>().isChecked = false;
        }

        ShowStarsVisualFeedback(starId);

        for (int i = 0; i < starId; i++)
        {
            _stars[i].GetComponent<DifficultyStar>().isChecked = true;
        }

        PlayerPrefs.SetInt("Difficulty", starId);
        _currentDifficulty = starId;

        if (_isChosenFirstTime)
        {
            _isChosenFirstTime = false;
            this.GetComponent<Animator>().SetTrigger("Hide");
        }
    }
}
