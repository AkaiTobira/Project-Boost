using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rotationSpeed =  50f;
    [SerializeField] float thrustSpeed   = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip gameStart;
    [SerializeField] AudioClip deadSound;

    Rigidbody rigidbody;
    AudioSource audioSource;

    enum State { Alive, Dead, Transcent };
    State status = State.Alive;

    void Start(){
        rigidbody   = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop();
    }

    void Update(){
        if( status == State.Alive ){
            ProcessThrust();
            ProcessRotate();
        }else{
            rigidbody.freezeRotation = false;
        }
    }

    private void ProcessThrust(){
        if( Input.GetKey( KeyCode.Space )){
            ApplyThrust();
        }else{
            audioSource.Stop();
        }
    }

    private void ApplyThrust(){
        rigidbody.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
        if(!audioSource.isPlaying) audioSource.PlayOneShot(mainEngine);
        rigidbody.freezeRotation = true;
    }


    void OnCollisionEnter(Collision other) {
        if( status != State.Alive ) return;
        switch( other.gameObject.tag ){
            case "Friendly": break;
            case "Finish": 
                OnFinishCollision();
            break;
            default: 
                OnDefaultCollision();
            break;
        }
    }

    private void OnDefaultCollision(){
        status = State.Dead;
        StartCoroutine(LoadNextLevel(0, 2.5f));
        audioSource.Stop();
        audioSource.PlayOneShot(deadSound);
    }

    private void OnFinishCollision(){
        status = State.Transcent;
        StartCoroutine(LoadNextLevel(1, 2f));
        audioSource.Stop();
        audioSource.PlayOneShot(gameStart);
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
