using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _carTransform;
    private Vector3 _offset;

    private void Start()
    {
        _offset = transform.position - _carTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = _carTransform.position + _offset;
        targetPosition.x = 0;
        transform.position = targetPosition;    
    }
}
