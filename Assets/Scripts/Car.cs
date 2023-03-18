using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();

        FloorMover.Instance.ActivatePoolObjects(1);
        FloorMover.Instance.SpawnNextFloorTile(10);
    }

    private void FixedUpdate()
    {
        Vector3 moveForward = transform.forward * _speed * Time.deltaTime;
        _rb.MovePosition(_rb.position +  moveForward);
    }
}
