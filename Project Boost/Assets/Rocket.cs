using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        switch( other.gameObject.tag ){
            case "Friendly": break;
            case "Finish":
                SceneManager.LoadScene(1);
                //print( "Finished");
            break;
            default:
                SceneManager.LoadScene(0);
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
