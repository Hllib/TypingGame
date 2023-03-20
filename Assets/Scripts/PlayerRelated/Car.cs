using System.Collections;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float _initialSpeed = 20f;
    [SerializeField]
    private float _currentSpeed;
    private float _speedBonus;
    private Vector3 _moveDirection;
    private Rigidbody _rb;

    private bool _canMove = false;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveDirection = transform.forward;
        _currentSpeed = 0f;
    }

    private void RestoreSpeed()
    {
        StopAllCoroutines();
        if (_currentSpeed < _initialSpeed)
        {
            _currentSpeed = _initialSpeed;
        }
    }

    public void PushCar(float speedBonus)
    {
        RestoreSpeed();
        _currentSpeed += speedBonus;
        StartCoroutine(SlowDown());
    }

    IEnumerator SlowDown()
    {
        do
        {
            yield return new WaitForSeconds(0.2f);
            _currentSpeed -= 1f;
        } while (_currentSpeed > 0);
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * _currentSpeed * Time.deltaTime);
    }
}
