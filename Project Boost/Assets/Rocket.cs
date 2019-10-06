using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{

    [SerializeField]float rotationSpeed =  50f;
    [SerializeField]float thrustSpeed   = 100f;
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
            rigidbody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            if(!audioSource.isPlaying) audioSource.Play();
            rigidbody.freezeRotation = true;
        }else{
            audioSource.Stop();
        }
    }

    void OnCollisionEnter(Collision other) {
        
        print( "Coool");
        switch( other.gameObject.tag ){
            case "Friendly":
                print( "Alive");
            break;

            default:
                print( "DEAD" );
            break;
        }

    }

    private void ProcessRotate(){

        rigidbody.freezeRotation = false;

        if( Input.GetKey( KeyCode.A )){
            transform.Rotate( Vector3.forward * rotationSpeed * Time.deltaTime);
        } else if( Input.GetKey( KeyCode.D )){
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }

        rigidbody.freezeRotation = true;
    }
}
