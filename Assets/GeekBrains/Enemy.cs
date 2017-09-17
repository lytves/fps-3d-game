using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public int health;
	Rigidbody myBody;

	// Use this for initialization
	void Start ()
	{
		myBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	//the method for damage a enemy
	public void takeDamage(int Damage)
	{
		health--;
		if(health <= 0)
		{
			myBody.constraints = RigidbodyConstraints.None;
			GetComponent<MeshRenderer>().material.color = Color.red;
		}
	}
}
