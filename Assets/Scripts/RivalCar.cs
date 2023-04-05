using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RivalCar : MonoBehaviour
{
    [SerializeField]
    private float _currentSpeed;
    private Vector3 _moveDirection;
    private Rigidbody _rb;
    [SerializeField]
    private PlayerCar _playerCar;

    private float _distanceToPlayer;
    private const float CountdownStartDistance = 3f;

    private float _minSpeed;
    private float _maxSpeed;
    private float _speed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveDirection = transform.forward;
        ChooseDiffuculty();
        Invoke("StopHoldingDistance", 5f);
        StartCoroutine(ChangeSpeed());
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
                _minSpeed = ConstSettings.EasyDiff.MinSpeed;
                _maxSpeed = ConstSettings.EasyDiff.MaxSpeed;
                break;
            case (int)Difficulty.Medium:
                _minSpeed = ConstSettings.MediumDiff.MinSpeed;
                _maxSpeed = ConstSettings.MediumDiff.MaxSpeed;
                break;
            case (int)Difficulty.Hard:
                _minSpeed = ConstSettings.HardDiff.MinSpeed;
                _maxSpeed = ConstSettings.HardDiff.MaxSpeed;
                break;
        }
    }

    private bool _isHoldingDistance = true;

    private bool _isCountdownGoing;

    private void Update()
    {
        //Debug.Log("Rival: " + _currentSpeed + " Player: " + _playerCar.currentSpeed + " Distance: " + _distanceToPlayer);

        _distanceToPlayer = transform.position.z - _playerCar.transform.position.z;
        if(_distanceToPlayer > CountdownStartDistance && !_isCountdownGoing)
        {
            UIManager.Instance.StartCountDown();
            _isCountdownGoing = true;
        }
        else if(_distanceToPlayer < CountdownStartDistance && _isCountdownGoing)
        {
            _isCountdownGoing = false;
            UIManager.Instance.StopCountDown();
        }
    }

    private void StopHoldingDistance()
    {
        _isHoldingDistance = false;
    }

    private void FixedUpdate()
    {
        _currentSpeed = _isHoldingDistance ? _playerCar.currentSpeed : _speed;

        _rb.MovePosition(_rb.position + _moveDirection * _currentSpeed * Time.deltaTime);
    }

    IEnumerator ChangeSpeed()
    {
        while (true)
        {
            _speed = Random.Range(_minSpeed, _maxSpeed);
            yield return new WaitForSeconds(5f);
        }
    }
}
