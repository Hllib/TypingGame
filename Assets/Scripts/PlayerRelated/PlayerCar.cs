using System.Collections;
using UnityEngine;

public class PlayerCar : MonoBehaviour
{
    private float _initialSpeed = 20f;
    [SerializeField]
    public float currentSpeed;
    private Vector3 _moveDirection;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _moveDirection = transform.forward;
        currentSpeed = 0f;
        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.engineStart, this.transform.position);
    }

    private void RestoreSpeed()
    {
        StopAllCoroutines();
        if (currentSpeed < _initialSpeed)
        {
            currentSpeed = _initialSpeed;
        }
    }

    public void SetCarSpeed(float typingSpeed)
    {
        if (typingSpeed > 0)
        {
            currentSpeed = typingSpeed;
            RestoreSpeed();
            StartCoroutine(SlowDown());
        }
    }

    IEnumerator SlowDown()
    {
        do
        {
            yield return new WaitForSeconds(0.25f);
            currentSpeed -= 0.5f;
        } while (currentSpeed > 0);
        currentSpeed = 0f;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveDirection * currentSpeed * Time.deltaTime);
    }
}
