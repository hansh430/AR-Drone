using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 rotationVector;
    private void Update()
    {
        transform.Rotate(rotationVector*Time.deltaTime);
    }
}
 