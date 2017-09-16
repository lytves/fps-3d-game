using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour {

	public Rigidbody MyBody;
	public float speed;
	private Vector3 Movement;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {

		float Right = Input.GetAxisRaw("Horizontal");
		float Forward = Input.GetAxisRaw("Vertical");

		Movement.Set(Forward, 0f, Right);
	}
}