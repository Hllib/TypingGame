using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour
{
    public float rotationSpeed;
    public Transform pivotPoint;

    public int rotationAxisZ;
    public int rotationAxisX;
    public int rotationAxisY;

    private void Update()
    {
        transform.RotateAround(pivotPoint.position, new Vector3(rotationAxisX, rotationAxisY, rotationAxisZ), rotationSpeed * Time.deltaTime);   
    }
}
