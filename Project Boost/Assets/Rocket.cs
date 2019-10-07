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

    enum State { Alive, Dead, Transcent };
    State status = State.Alive;

    void Start()
    {
        rigidbody   = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if( status == State.Alive ){
            ProcessThrust();
            ProcessRotate();
        }else{
            rigidbody.freezeRotation = false;
        }
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

        if( status != State.Alive ) return;

        switch( other.gameObject.tag ){
            case "Friendly": break;
            case "Finish":
                StartCoroutine(LoadNextLevel(1, 0f));
    //            SceneManager.LoadScene(1);
            break;
            default:
                status = State.Dead;
                StartCoroutine(LoadNextLevel(0, 5f));
    //            SceneManager.LoadScene(0);
            break;
        }

    }

    IEnumerator LoadNextLevel( int level, float delayTime ){
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(level);
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
