using System;
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

    private float _distanceToPlayer;

    private const float MaxDistanceToPlayer = 12.7f;
    private const float CloseRangeDistance = 3.0f;
    private float _startChaseSpeed;
    private float _stopChaseSpeed;

    private bool _isApproaching;
    private bool _isDroppingSpeed;
    private bool _hasReachedPlayer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveDirection = transform.forward;
        _hasReachedPlayer = false;
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

    private void CheckChaseState()
    {
        if (_playerCar.currentSpeed <= _startChaseSpeed && !_isApproaching)
        {
            _isDroppingSpeed = false;
            _isApproaching = true;

            StopAllCoroutines();
            StartCoroutine(ApproachPlayer());
            return;
        }
        else if (_playerCar.currentSpeed >= _stopChaseSpeed && !_isDroppingSpeed && _isApproaching)
        {
            _isApproaching = false;
            _isDroppingSpeed = true;

            StopAllCoroutines();
            StartCoroutine(ReturnToMaxDistance());
            return;
        }
    }

    IEnumerator ApproachPlayer()
    {
        while (_distanceToPlayer > CloseRangeDistance)
        {
            yield return new WaitForSeconds(1f);
            _currentSpeed = _playerCar.currentSpeed + 0.5f;
        }

        _currentSpeed = _playerCar.currentSpeed;
        _isApproaching = false;
    }

    IEnumerator ReturnToMaxDistance()
    {
        float offset = 3f;
        while (_distanceToPlayer < MaxDistanceToPlayer)
        {
            yield return new WaitForSeconds(1f);
            _currentSpeed -= 0.5f;
        }

        while(_distanceToPlayer > MaxDistanceToPlayer + offset)
        {
            yield return new WaitForSeconds(0.1f);
            _currentSpeed += 3f;
        }

        _currentSpeed = _playerCar.currentSpeed;
        _isDroppingSpeed = false;
    }

    private void Update()
    {
        _distanceToPlayer = Vector3.Distance(transform.position, _playerCar.transform.position);
        Debug.Log(_distanceToPlayer);

        if (_playerCar.currentSpeed > 0f)
        {
            CheckChaseState();
        }
        if ((!_isApproaching && !_isDroppingSpeed))
        {
            _currentSpeed = _playerCar.currentSpeed;
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * _currentSpeed * Time.deltaTime);
    }

    public void LevelPlayerSpeed(bool state)
    {
        _hasReachedPlayer = state;
    }

}