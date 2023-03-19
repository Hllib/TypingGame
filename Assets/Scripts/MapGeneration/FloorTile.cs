using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : MonoBehaviour
{
    [SerializeField]
    private Transform _nextSpawnPointTransform;

    private void OnEnable()
    {
        FloorMover.Instance.SetNextSpawnPointTransform(_nextSpawnPointTransform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FloorMover.Instance.SpawnNextFloorTile(1);
            DisableFloor();
        }
    }

    private void DisableFloor()
    {
        this.gameObject.SetActive(false);
    }
}
