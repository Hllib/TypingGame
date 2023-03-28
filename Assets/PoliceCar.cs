using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceCar : MonoBehaviour
{
    [SerializeField]
    private float _currentSpeed;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    [SerializeField]
    private PlayerCar _playerCar;

    private float _playerSpeed;
    private float _distanceToPlayer;

    private const float MaxDistanceToPlayer = 12.7f;
    private float _startChaseSpeed;
    private float _stopChaseSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveDirection = transform.forward;
        ChooseDiffuculty();
    }

    enum Difficulty
    {
        Easy = 1,
        Medium = 2,
        Hard = 3
    }

    private void ChooseDiffuculty()
    {
        switch (PlayerPrefs.GetInt("Difficulty"))
        {
            case (int)Difficulty.Easy:
                _startChaseSpeed = ConstSettings.EasyDiff.StartChaseSpeed;
                _stopChaseSpeed = ConstSettings.EasyDiff.StopChaseSpeed;
                break;
            case (int)Difficulty.Medium:
                _startChaseSpeed = ConstSettings.MediumDiff.StartChaseSpeed;
                _stopChaseSpeed = ConstSettings.MediumDiff.StopChaseSpeed;
                break;
            case (int)Difficulty.Hard:
                _startChaseSpeed = ConstSettings.HardDiff.StartChaseSpeed;
                _stopChaseSpeed = ConstSettings.HardDiff.StopChaseSpeed;
                break;
        }
    }

    private void FixedUpdate()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _playerCar.transform.position);
        Debug.Log(_distanceToPlayer);

        _rb.MovePosition(_rb.position + _moveDirection * _currentSpeed * Time.deltaTime);
    }
}
