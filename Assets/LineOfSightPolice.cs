using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineOfSightPolice : MonoBehaviour
{
    [SerializeField]
    private PoliceCar _policeCar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.StartCountDown();
            _policeCar.LevelPlayerSpeed(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.StopCountDown();
            _policeCar.LevelPlayerSpeed(false);
        }
    }
}
