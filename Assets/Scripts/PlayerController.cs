using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    private Rigidbody2D myRigidbody;
    private Animator myAnim;
    public float PlayerJumpForce = 600f;
    private float PlayerDeathTime = -1;
    private Collider2D myCollider;
    public Text ScoreText;
    private float StartTime;

	// Use this for initialization
	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        StartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

        if (PlayerDeathTime == -1)
        {

            if (Input.GetButtonDown("Jump"))
            {
                myRigidbody.AddForce(transform.up * PlayerJumpForce);
            }

            myAnim.SetFloat("vVelocity", myRigidbody.velocity.y);

            ScoreText.text = (Time.time - StartTime).ToString("0.0");
        }
        else
        {

            if(Time.time > PlayerDeathTime + 2)
            {

                Application.LoadLevel(Application.loadedLevel);

            }
            
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
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
        }
    }
}
