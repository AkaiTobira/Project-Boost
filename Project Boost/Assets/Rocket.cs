using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        ProcessInput();
    }

    private void ProcessInput(){
        
        if( Input.GetKey( KeyCode.Space )){
            rigidbody.AddRelativeForce(Vector3.up * 100 * Time.deltaTime);
        }
        if( Input.GetKey( KeyCode.A )){
            transform.Rotate(Vector3.forward * 50 * Time.deltaTime);

        } else if( Input.GetKey( KeyCode.D )){
            transform.Rotate(-Vector3.forward * 50 * Time.deltaTime);
        }
    }
}
