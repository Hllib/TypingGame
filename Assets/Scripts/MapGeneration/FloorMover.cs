using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMover : MonoBehaviour
{
    [SerializeField] private ObjectPooler _floorPooler;

    private Transform _nextSpawnTransform;

    private static FloorMover _instance;
    public static FloorMover Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Floor mover is NULL!");
            }

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public void SetNextSpawnPointTransform(Transform transform)
    {
        _nextSpawnTransform = transform;
    }

    public void ActivatePoolObjects(int amount)
    {
        _floorPooler.ActivatePoolObjects(amount);
    }

    public void SpawnNextFloorTile(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var floorTile = _floorPooler.GetPooledObject();
            floorTile.transform.position = _nextSpawnTransform.position;
            floorTile.transform.rotation = _nextSpawnTransform.rotation;
            floorTile.SetActive(true);
        }
    }
}