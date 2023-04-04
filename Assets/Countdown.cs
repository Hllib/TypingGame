using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public void EndGame()
    {
        GameManager.Instance.ReloadGame();
    }

    private int _triggerCounter = 1;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_triggerCounter % 2 == 0)
            {
                UIManager.Instance.StopCountDown();
            }
            else
            {
                UIManager.Instance.StartCountDown();
            }
            _triggerCounter += 1;
        }
    }
}
