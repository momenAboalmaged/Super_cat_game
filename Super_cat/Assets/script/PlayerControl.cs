using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float turnspeed;
    public float Speed;
    public float jumpforce;
    public float horizontalInput;
    private Animator playeranm;
    private Rigidbody rb;
    public bool onground = true;
    public float grav;
    public bool GameOver = true;
    public GameObject Star;
    public GameObject x2;
    public ParticleSystem ExplosionParticle;
    public ParticleSystem Win;
    public AudioSource playerSound;
    public AudioClip jumpSound;
    public AudioClip crashSound1;
    public AudioClip crashSound2;
    // public AudioClip gameSound;
    public AudioClip powerUpSound;
    public AudioClip X2Sound;
    //public float timeRemain = 10;
    public GameManage gamemanage;
    public TextMeshProUGUI scoretest;
    public int score=0 ;

    void Start()
    {
        updatescore(0);
        playeranm = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= grav;
        Invoke("EnableMove", 2.5f);
        StartCoroutine(Startt());
        playerSound = GetComponent<AudioSource>();
    }
    void Update()
    {

       // if (timeRemain > 0)
       // timeRemain -= Time.deltaTime;
            
         //  else if (timeRemain < 0)
       // {
         //   playeranm.SetBool("Dead", true);
           // GameOver = true;
        //}
                
        
       if (transform.position.x < -9.5f)
        {
           transform.position = new Vector3(-9.5f, transform.position.y, transform.position.z);
        }
        if (transform.position.x > 9.5f)
        {
         transform.position = new Vector3(9.5f, transform.position.y, transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Space)&&onground&&GameOver==false)
        {
            rb.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
            onground = false;
            playeranm.SetBool("Jumping",true);
            playerSound.PlayOneShot(jumpSound, 2);
        }
        if (GameOver==false)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            transform.Rotate(Vector3.up * Time.deltaTime * turnspeed * horizontalInput);
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
           // playerSound.PlayOneShot(gameSound,1);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
     
        onground = true;
        playeranm.SetBool("Jumping", false);
        if (collision.gameObject.CompareTag("obstacle"))
        {
            Debug.Log("Game Over");

            playeranm.SetTrigger("Hit");
            playeranm.SetBool("Dead", true);
            GameOver = true;
           
            ExplosionParticle.Play();
            //DirtParticle.Stop();
            playerSound.PlayOneShot(crashSound1, 2);
            playerSound.PlayOneShot(crashSound2, 2);

        }

      

    }
    void EnableMove()
    {
        GameOver = false;
    }
    IEnumerator Startt()
    {
        //updatescore(0);

        yield return new WaitForSeconds(4);
        GetComponent<Animator>().Play("playeranm");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("X2"))
        {
            Speed = Speed +2;
           Destroy(other.gameObject);
            playerSound.PlayOneShot(X2Sound, 1.5f);
        }
        if(other.CompareTag("star"))
        {
            Destroy(other.gameObject);
            playerSound.PlayOneShot(powerUpSound, 2);
            //timeRemain += 1;
            updatescore(1);
        }
        if (other.CompareTag("finish"))
        {
          Win.Play();
            Debug.Log("Win");
           playeranm.SetTrigger("Hit");
            playeranm.SetFloat("RandomIdle", 1);
           

           //playeranm.SetBool("Dead", true);
            Destroy(other.gameObject);
            GameOver = true;
            
          
        }
        
    }
    public void updatescore(int value)
    {
        score += value;
        scoretest.text = "Score:" + score;
    }

}
