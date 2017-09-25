using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]

public class Player : MonoBehaviour {

	public Rigidbody myBody;
	public float speed;
	private Vector3 movement;
	public GameObject bullet, startBullet;

    public int health;

    public const int magazine = 30;
    public int ammo = 90, currentMagazine = 30;

    public bool canAttack = true, reload = false;

	// Use this for initialization
	void Start ()
	{
		myBody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
	{
        //GetMouseButtonDown for fire slow
        //GetMouseButton for fire fast
        if (Input.GetMouseButton(0))
		{
			StartCoroutine( Fire());
		}

        //call for reload the magazine
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(StartReload());
    }

	//method for move the player, better the only Update, because refresh more
	void FixedUpdate()
	{
        //here is a movement the player
		float right = Input.GetAxisRaw("Horizontal");
		float forward = Input.GetAxisRaw("Vertical");

		movement.Set(forward, 0f, right);

		myBody.AddForce(transform.forward * forward * speed, ForceMode.Impulse);
		myBody.AddForce(transform.right * right * speed, ForceMode.Impulse);
	}

	//method for fire
	IEnumerator Fire()
	{
        //if is posible to fire
        if (canAttack && currentMagazine > 0 && !reload)
        {
            canAttack = false;
            currentMagazine--;

		    //clone the object "bullet" 
		    Instantiate(bullet, startBullet.transform.position, startBullet.transform.rotation);

            if (currentMagazine <= 0)
            {
                StartCoroutine(StartReload());
                reload = true;
            }

            yield return new WaitForSeconds(0.05f);

            canAttack = true;
        }
    }

    //the method for reload the magazine
    IEnumerator StartReload()
    {
        yield return new WaitForSeconds(1f);

        if (ammo > magazine)
        {
            int num = magazine - currentMagazine;
            ammo -= num;
            currentMagazine = magazine;
        }
        else
        {
            currentMagazine = ammo;
            ammo = 0;
        }

        reload = false;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            //!!!!!!! death of the player
           // myBody.constraints = RigidbodyConstraints.None;
            //GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }
} 