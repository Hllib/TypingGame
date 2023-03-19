using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float _speed = 20f;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void AddForceToCar(int forceMultiplier)
    {
        Vector3 moveForward = transform.forward * _speed * forceMultiplier;
        _rb.AddForce(moveForward, ForceMode.VelocityChange);
    }
}
