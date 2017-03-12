using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
	private Rigidbody2D rigidBody;
	private Animator animator;
	public float bunnyJumpForce = 750f;

	// Use this for initialization
	void Start ()
	{
		rigidBody = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetButtonUp("Jump") || Input.GetMouseButtonDown(0))
			rigidBody.AddForce(transform.up * bunnyJumpForce);

		animator.SetFloat("vVelocity", Mathf.Abs(rigidBody.velocity.y));
	}
}
