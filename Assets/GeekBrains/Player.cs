using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour {

	public Rigidbody myBody;
	public float speed;
	private Vector3 movement;
	public GameObject bullet, startBullet;

	// Use this for initialization
	void Start ()
	{
		myBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Fire();
		}
	}

	//method for move the player
	void FixedUpdate()
	{
		float right = Input.GetAxisRaw("Horizontal");
		float forward = Input.GetAxisRaw("Vertical");

		movement.Set(forward, 0f, right);

		myBody.AddForce(transform.forward * forward * speed, ForceMode.Impulse);
		myBody.AddForce(transform.right * right * speed, ForceMode.Impulse);
	}

	//method for fire
	void Fire()
	{
		//clone the object "bullet" 
		Instantiate(bullet, startBullet.transform.position, startBullet.transform.rotation);
	}
} 