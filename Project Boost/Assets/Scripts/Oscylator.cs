using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscylator : MonoBehaviour
{

    [SerializeField] Vector3 direcionVector;
    [Range(0,1)][SerializeField] float speed;
    [SerializeField] float period = 2f;

    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position = startPosition + direcionVector*CalculateRange();
    }

    private float CalculateRange( ){
        if( period <= Mathf.Epsilon ) return 0f;
        float cycles = speed*Time.time/period;
        const float tau = Mathf.PI * 2;
        return Mathf.Sin(tau * cycles) / 2f + 0.5f;
    }


}
