using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public void EndGame()
    {
        GameManager.Instance.CheckPauseState();
        UIManager.Instance.ShowPlayBt(false);
        GameManager.Instance.IsGameOver = true;
    }
}
