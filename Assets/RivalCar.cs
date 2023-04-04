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
    private const float MaxDistanceBehind = -12f;
    private const float MaxDistanceAhead = 15f;

    private float _minSpeed;
    private float _maxSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveDirection = transform.forward;
        ChooseDiffuculty();
        Invoke("StopHoldingDistance", 5f);
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
    private bool _isFallingBehind = false;
    private bool _isGoingTooFar = false;

    private void Update()
    {
        //Debug.Log("Rival: " + _currentSpeed + " Player: " + " Distance: " + _distanceToPlayer);
        //Debug.Log("falling behind? - " + _isFallingBehind + " too far? - " + _isGoingTooFar);

        _distanceToPlayer = transform.position.z - _playerCar.transform.position.z;

        _isFallingBehind = _distanceToPlayer < MaxDistanceBehind ? true : false;
        _isGoingTooFar = _distanceToPlayer > MaxDistanceAhead ? true : false;

        _currentSpeed = _isHoldingDistance ? _playerCar.currentSpeed : _minSpeed;
    }

    private void StopHoldingDistance()
    {
        _isHoldingDistance = false;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * _currentSpeed * Time.deltaTime);
    }
}
