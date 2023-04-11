using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideTrafficMovement : MonoBehaviour
{
    [SerializeField] private MovementDirection _movementDirection;
    [SerializeField] private float _speed;
    private float _distanceToPlayer;
    [SerializeField] private PlayerCar _playerCar;

    enum MovementDirection
    {
        TowardsPlayer,
        WithPlayer
    }

    private float _canChangePos = 0f;
    private float _changePosRate = 5f;
    private const float MaxDistanceToPlayer = 50f;

    private void Update()
    {
        switch (_movementDirection)
        {
            case MovementDirection.TowardsPlayer: transform.Translate(Vector3.back * _speed * Time.deltaTime); break;
            case MovementDirection.WithPlayer: transform.Translate(Vector3.forward * _speed * Time.deltaTime); break;
        }

        _distanceToPlayer = Mathf.Abs(transform.position.z - _playerCar.transform.position.z);
        if (_distanceToPlayer > MaxDistanceToPlayer && Time.timeSinceLevelLoad > _canChangePos)
        {
            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, _playerCar.transform.position.z + MaxDistanceToPlayer);
            _canChangePos  = Time.timeSinceLevelLoad + _changePosRate;
        }
    }
}
