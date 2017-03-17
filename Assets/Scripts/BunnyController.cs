using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour
{
	private Rigidbody2D rigidBody;
	private Animator animator;
	public float bunnyJumpForce = 750f;
	private float bunnyHurtTime = -1;
	private Collider2D collider;
	public Text scoreText;
	private float startTime;
	private int jumpsLeft = 2;

	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
		collider = GetComponent<Collider2D>();

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(bunnyHurtTime == -1)
		{
			if((Input.GetButtonUp("Jump") || Input.GetMouseButtonDown(0)) && (jumpsLeft > 0))
			{
				if(rigidBody.velocity.y < 0)
					rigidBody.velocity = Vector2.zero;

				if(jumpsLeft == 1)
					rigidBody.AddForce(transform.up * bunnyJumpForce * 0.75f);
				else
					rigidBody.AddForce(transform.up * bunnyJumpForce);

				jumpsLeft--;
			}

			animator.SetFloat("vVelocity", rigidBody.velocity.y);
			scoreText.text = (Time.time - startTime).ToString("0.0");
		}
		else
		{
			if(Time.time > bunnyHurtTime + 2)
				Application.LoadLevel(Application.loadedLevel);
		}
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
		{
			foreach(PrefabSpawner spawner in FindObjectsOfType<PrefabSpawner>())
				spawner.enabled = false;

			foreach(MoveLeft moveLefter in FindObjectsOfType<MoveLeft>())
				moveLefter.enabled = false;

			bunnyHurtTime = Time.time;
			animator.SetBool("bunnyHurt", true);
			rigidBody.velocity = Vector2.zero;
			rigidBody.AddForce(transform.up * bunnyJumpForce);
			collider.enabled = false;
		}
		else if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
				jumpsLeft = 2;	
		}
	}
}
