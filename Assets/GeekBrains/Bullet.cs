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
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Enemy")
		{
			other.GetComponent<Enemy>().takeDamage(damage);
			Destroy(gameObject);
		}
	}
}
