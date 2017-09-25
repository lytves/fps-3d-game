using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Bullet : MonoBehaviour {

	public float force;
	public int damage;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Rigidbody>().AddForce(transform.forward * force, ForceMode.Impulse);

		//for destroy bullets after 2 seconds
		Destroy(gameObject, 2);
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	//the method for collisione with enemy
	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy")
		{
			other.GetComponent<Enemy>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}
}
