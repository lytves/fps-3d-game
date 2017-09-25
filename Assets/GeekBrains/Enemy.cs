using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Transform target;

    public float maxDistance, minDistance, speedRotation, attackDistance;

	public int health;
	Rigidbody myBody;

    Transform myTransform;
    public float mySpeed, timeCast;

    public bool couldown = false;
    public int damage;

    // Use this for initialization
    void Start ()
	{
		myBody = GetComponent<Rigidbody>();
		myTransform = transform;

		target = GameObject.FindGameObjectWithTag("Player").transform;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
        //if distance beetween enemy and player is a little, to turn the enemy to the player
		if (Vector3.Distance(myTransform.position, target.position) < maxDistance)
		{
			Vector3 myRotation = target.position - myTransform.position;


            //TODO do move the enemy with Impulse (how moved the player)

            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(myRotation), speedRotation * Time.deltaTime);

            //for enemy go to player
            if (Vector3.Distance(myTransform.position, target.position) > minDistance)
            {
                myTransform.position += myTransform.forward * mySpeed * Time.deltaTime;
            }

            //for
            if (Vector3.Distance(myTransform.position, target.position) < attackDistance)
            {
                if (!couldown)
                {
                    couldown = true;
                    StartCoroutine(Attack());
                }
            }

        }
	}

    IEnumerator Attack()
    {
        //wait .. second
        yield return new WaitForSeconds(timeCast);

        //TODO???

        //here start attack to player
        if (Vector3.Distance(myTransform.position, target.position) < attackDistance)
        {
            target.GetComponent<Player>().TakeDamage(damage);
        }
        //......

        couldown = false;
    }

	//the method for damage the  enemy
	public void TakeDamage(int damage)
	{
		health -= damage;

		if (health <= 0)
		{
			myBody.constraints = RigidbodyConstraints.None;
			GetComponent<MeshRenderer>().material.color = Color.red;
		}
	}
}
