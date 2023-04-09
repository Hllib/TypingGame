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
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.loseSound, Vector3.zero);
    }
}
