using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [SerializeField] float rotationSpeed =  75f;
    [SerializeField] float thrustSpeed   = 100f;
    [SerializeField] float loadLevelTime = 2.5f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip gameStart;
    [SerializeField] AudioClip deadSound;

    [SerializeField] ParticleSystem deadParticle;
    [SerializeField] ParticleSystem moveParticle;
    [SerializeField] ParticleSystem finishParticle;

    Rigidbody rigidbody;
    AudioSource audioSource;
    bool collisionEnable = true;

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
            if( Debug.isDebugBuild ) ProcessCheat();
        }else{
            rigidbody.freezeRotation = false;
        }
    }

    private void ProcessCheat(){
        if( Input.GetKeyDown( KeyCode.L ) ){
            LoadNextLevel(0);
        }else if( Input.GetKeyDown( KeyCode.C ) ){
            collisionEnable = !collisionEnable;
        }
    }

    private void LoadNextLevel( float time){
            int loadIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if(loadIndex == SceneManager.sceneCountInBuildSettings) loadIndex = 0;
            StartCoroutine(WaitForNextLevel(loadIndex,time));
    }

    private void ProcessThrust(){
        if( Input.GetKey( KeyCode.Space )){
            ApplyThrust();
        }else{
            audioSource.Stop();
        }
        if( Input.GetKeyUp( KeyCode.Space )){
            moveParticle.Stop();
        }
        if( Input.GetKeyDown( KeyCode.Space )){
            moveParticle.Play();
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
                if( collisionEnable ) OnDefaultCollision();
            break;
        }
    }

    private void OnDefaultCollision(){
        status = State.Dead;
        moveParticle.Stop();
        deadParticle.Play();
        
        StartCoroutine(WaitForNextLevel(SceneManager.GetActiveScene().buildIndex, loadLevelTime));
        
        audioSource.Stop();
        audioSource.PlayOneShot(deadSound);
    }

    private void OnFinishCollision(){
        status = State.Transcent;
        moveParticle.Stop();
        finishParticle.Play();
        LoadNextLevel(loadLevelTime);
        audioSource.Stop();
        audioSource.PlayOneShot(gameStart);
    }

    IEnumerator WaitForNextLevel( int level, float delayTime ){
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(level);
    }

    private void ProcessRotate(){
        rigidbody.freezeRotation = false;
        if( Input.GetKey( KeyCode.A )|| Input.GetKey("left")){
            transform.Rotate( Vector3.forward * rotationSpeed * Time.deltaTime);
        } else if( Input.GetKey( KeyCode.D ) || Input.GetKey("right")){
            transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        }
        rigidbody.freezeRotation = true;
    }
}
