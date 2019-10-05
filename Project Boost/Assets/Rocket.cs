using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    Rigidbody rigidbody;
    AudioSource audioSource;

    void Start()
    {
        rigidbody   = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    private void ProcessThrust(){
        if( Input.GetKey( KeyCode.Space )){
            rigidbody.AddRelativeForce(Vector3.up * 100 * Time.deltaTime);
            if(!audioSource.isPlaying) audioSource.Play();
        }else{
            audioSource.Stop();
        }
    }

    private void ProcessRotate(){
        if( Input.GetKey( KeyCode.A )){
            transform.Rotate(Vector3.forward * 50 * Time.deltaTime);

        } else if( Input.GetKey( KeyCode.D )){
            transform.Rotate(-Vector3.forward * 50 * Time.deltaTime);
        }
    }
}
