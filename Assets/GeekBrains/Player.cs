using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text displayHealth, displayMagazine, displayAmmo;

    public float jumpForce;
    float groundDistance;
    Collider myCollider;

    public GameObject weapon, camera;
    public float rotationSpeedWeapon;
    Ray myRay;
    RaycastHit myRaycastHit;

	// Use this for initialization
	void Start ()
	{
		myBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        //for create at start UI
        displayHealth.text = "Health: " + health;
        displayMagazine.text = "Magazine: " + currentMagazine;
        displayAmmo.text = "Ammo: " + ammo;

        myCollider = GetComponent<Collider>();
        groundDistance = myCollider.bounds.extents.y;
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

        //for rotate the arm to center of the display
        myRay = new Ray(camera.transform.position, camera.transform.forward);
        Physics.Raycast(myRay, out myRaycastHit);

        Vector3 rotation;

        if (myRaycastHit.collider == null)
        {
            rotation = weapon.transform.forward;
        } else
        {
            rotation = myRaycastHit.point - weapon.transform.position;
        }

        weapon.transform.rotation = Quaternion.Slerp(weapon.transform.rotation, Quaternion.LookRotation(rotation), rotationSpeedWeapon * Time.deltaTime);

        //for show myRaycastHit
        //Debug.DrawRay(camera.transform.position, rotation, Color.green);
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

        //for jump player
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            myBody.AddForce(Vector3.up * jumpForce * 20, ForceMode.Impulse);
    }

	//method for fire
	IEnumerator Fire()
	{
        //if is posible to fire
        if (canAttack && currentMagazine > 0 && !reload)
        {
            canAttack = false;
            currentMagazine--;

            displayMagazine.text = "Magazine: " + currentMagazine;

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

        displayAmmo.text = "Ammo: " + ammo;

        reload = false;

    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        displayHealth.text = "Health: " + health;

        if (health <= 0)
        {
            //!!!!!!! death of the player
            // myBody.constraints = RigidbodyConstraints.None;
            //GetComponent<MeshRenderer>().material.color = Color.red;
            Debug.Log("Player is dead!");
        }
    }

    //for single jump player
    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, groundDistance + 0.1f);
    }
} 