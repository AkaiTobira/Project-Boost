using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 rotationVector;
    [Range(0,1)][SerializeField] float speed;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.RotateAround( startPosition, rotationVector, speed * 360 * Time.deltaTime);
    }

}
