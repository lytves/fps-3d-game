using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour {

	public Rigidbody MyBody;
	public float speed;
	private Vector3 Movement;

	// Use this for initialization
	void Start ()
	{
		MyBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	//method for move the player
	void FixedUpdate()
	{

		float Right = Input.GetAxisRaw("Horizontal");
		float Forward = Input.GetAxisRaw("Vertical");

		Movement.Set(Forward, 0f, Right);

		MyBody.AddForce(transform.forward * Forward * speed, ForceMode.Impulse);
		MyBody.AddForce(transform.right * Right * speed, ForceMode.Impulse);
	}
}