using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    public float PlayerJumpForce = 600f;
    private float PlayerDeathTime = -1;
    private Collider2D myCollider;
    public Text ScoreText;
    private float StartTime;
    public float Delay;
    public int JumpPhase;
    public int MaxJump;
    public AudioSource jumpSFX;
    public AudioSource deathSFX;
    public AudioSource bgMusic;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }

        if (PlayerDeathTime == -1)
        {
            if (JumpPhase < MaxJump)
            {
                if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1"))
                {
                    jumpSFX.Play();

                    myRigidbody.velocity = new Vector2(0f, 0f);
                    myRigidbody.AddForce(transform.up * PlayerJumpForce);
                    StartCoroutine(JumpDelay());
                }
                
            }


            myAnim.SetFloat("vVelocity", myRigidbody.velocity.y);

            ScoreText.text = ((Time.time - StartTime) * 1000).ToString("0");
        }
        else
        {

            if(Time.time > PlayerDeathTime + 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            ContactPoint2D contact = collision.contacts[0];
            if (Vector3.Dot(contact.normal, Vector3.up) > 0.5)
            {
                JumpPhase = 0;
            }
        }

        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {

            foreach (PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
            {
                spawner.enabled = false;
            }

            foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
            {
                moveLefter.enabled = false;
            }

            PlayerDeathTime = Time.time;
            myAnim.SetBool("PlayerDeath", true);
            myRigidbody.velocity = Vector2.zero;
            myRigidbody.AddForce(transform.up * PlayerJumpForce);
            myCollider.enabled = false;

            bgMusic.Pause();
            deathSFX.Play();

            float currentHighScore = PlayerPrefs.GetFloat("HighScore", 0);
            float currentScore = ((Time.time - StartTime) * 1000);

            if (currentScore > currentHighScore)
            {
                PlayerPrefs.SetFloat("HighScore", currentScore);
            }
        }
    }

    IEnumerator JumpDelay()
    {
        yield return new WaitForSeconds(Delay);
        JumpPhase = JumpPhase + 1;
    }
}
