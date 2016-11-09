using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerScriptV2 : MonoBehaviour{
	//remove // to activate it 
	//public Animator anim;
	public GameObject player;
	public float bulletSpeed = 100;
    public float flameSpeed = 50;
    public GameObject bullet; //get the bullet object
    public GameObject flame;  //get the flame object
    public bool invincible = false;
	public bool invisible = false;
	public bool isDead = false;
	public string statUrl = "Assests/Data Sheets/PlayerStats.csv";
	private BoxCollider boxcol;
	private float x_width;
	private float y_height;
	private float x_offset;
	private float y_offset;
	public float JumpSpeed;
	public float JumpDamping = 0.1f;
	public bool isOnGround = false;
	public bool crouching = false;
	//private Transform tr;
	//public float height;

	//all variables used to check for physics with collider
	int layerMask;
	Rect box;
	Vector2 velocity;
	bool grounded = false;
	bool falling = false;
	float acceleration = 0.4f;
	int horizontalRays = 6;
	int verticalRays = 4;
	int margin = 1;
	float gravity = 6f;
	float maxSpeed = 0.0f;
	float maxfall = 0.0f;

	//set this to check instantiation
	private bool isCreated;
	private float startTime;
	//for animation
	Animator anim;
	public bool facingRight = true;
	public bool jump = false;
	public float Speed = 1.5f; //for movement

	//for other variables
	private int curHealth; //<--the only value changing

	//check fire....
	private float lastFire;
	private float fireRate;

    //weapon toggle key
    public string weaponToggleKey;

    //check fire timer
    private float timer;

	public int Level{//Level == powerup <-- if we ever decide to have it
		get { return 1; }
	}

	public int maxHealth { //Max avaliable Hp ...does not change
		get { return 3; }//max health depends on level
	}

	void Awake() {//variable only set on start

		Debug.Log(maxHealth);
		curHealth = maxHealth;
		x_width = GetComponent<BoxCollider>().size.x;
		y_height = GetComponent<BoxCollider>().size.y;
		//x_offset = GetComponent<BoxCollider>().offset.x;
		//y_offset = GetComponent<BoxCollider>().offset.y;
		boxcol = player.GetComponent<BoxCollider>();
		anim = GetComponent<Animator>();

        //set default weapon to to fire3 mode
        weaponToggleKey = "Fire3";
    }

	void start()
	{

		layerMask = LayerMask.NameToLayer("Player");

	}

	void Update(){
		Debug.Log(getMaxHealth());
		AdjustCurrentHealth(0);

	}

	public void AdjustCurrentHealth(int adj)
	{
		curHealth += adj;
		if (curHealth < 0)
			curHealth = 0;
		if (curHealth > maxHealth)
			curHealth = maxHealth;
	}

	//fix movement to refresh 1 per frame
	void FixedUpdate()
	{

		box = new Rect(GetComponent<Collider>().bounds.min.x, GetComponent<Collider>().bounds.min.y, GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.y);

		//Gravity 
		//used to predict location when we check for ground
		if (!grounded)
			velocity = new Vector2(velocity.x, Mathf.Max(velocity.y - gravity, -maxfall));

		if (velocity.y < 0)
		{
			falling = true;
		}
		//we start the ray from the center of player box -- so it can detect edges
		if (grounded || falling) { //if only when player is in air
			Vector3 startPoint = new Vector3(box.xMin + margin, box.center.y, transform.position.z);
			Vector3 endPoint = new Vector3(box.xMin - margin, box.center.y, transform.position.z);
			RaycastHit hitInfo;
			//float distance = box.height + 1;
			float distance = box.height/2 + (grounded ? margin : Mathf.Abs(velocity.y * Time.deltaTime));
			//how long the ray will be based

			bool connected = false; //check if we are grounded 
			for (int i = 0; i < verticalRays; i++) {
				float lerpAmount = (float)i / (float)verticalRays - 1;
				Vector3 origin = Vector3.Lerp(startPoint, endPoint, lerpAmount);
				Ray ray = new Ray(origin, Vector3.down);
				connected = Physics.Raycast(ray, out hitInfo, distance, layerMask);
				Debug.DrawRay(startPoint, Vector3.down, Color.blue);
				if (connected) {
					grounded = true;
					falling = false;
					transform.Translate(Vector3.down * (hitInfo.distance - box.height / 2));
					velocity = new Vector2(velocity.x, 0);
					break;
				}
			}
			if (!connected)
			{
				Debug.Log("Air");
				grounded = false;
			}
			else
			{
				Debug.Log("ground");
			}

		}

		//horizontal movement check
		float horizontalAxis = Input.GetAxisRaw("Horizontal");
		float newVelocityX = velocity.x;
		if (horizontalAxis != 0)
		{
			newVelocityX += acceleration * horizontalAxis;
			newVelocityX = Mathf.Clamp(newVelocityX, -maxSpeed, maxSpeed);
		}
		else if (velocity.x != 0)
		{
			int modifier = velocity.x > 0 ? -1 : 1;
			newVelocityX += acceleration * modifier;
		}


		velocity = new Vector2(newVelocityX, velocity.y);

		if (velocity.x != 0) {
			Vector3 startPoint = new Vector3(box.center.x, box.yMin + margin, transform.position.z);
			Vector3 endPoint = new Vector3(box.center.x, box.yMax - margin, transform.position.z);
			RaycastHit hitInfo;

			float sideRayLength = box.width / 2 + Mathf.Abs(newVelocityX * Time.deltaTime);
			Vector3 direction = newVelocityX > 0 ? Vector3.right : Vector3.left;

			bool connected = false;

			for (int i = 0; i < horizontalRays; i++) {
				float lerpAmount = (float)i / (float)(horizontalRays - 1);
				Vector3 origin = Vector3.Lerp(startPoint, endPoint, lerpAmount);
				Ray ray = new Ray(origin, direction);

				connected = Physics.Raycast(ray, out hitInfo, sideRayLength);
				Debug.Log(hitInfo);
				if (connected) {
					transform.Translate(direction * (hitInfo.distance - box.width / 2));
					velocity = new Vector2(0, velocity.y);
					break;
				}
			}
		}

		//movement and action
		//Control Scheme Right arrow
		if (Input.GetKey(KeyCode.RightArrow))
		{
			anim.SetBool("Idle", false);
			Debug.Log("going right");
			facingRight = true;
			if (isOnGround && (crouching == false))
			{
				Debug.Log("Crouching false");
				transform.Translate(Vector2.right * Speed/2 * Time.deltaTime);
			}
			else if (!isOnGround)
			{
				Debug.Log("Crouching on");
				transform.Translate(Vector2.right * Speed/3 * Time.deltaTime);
			}
			anim.SetBool("WalkRight", true);

			//facedirection = "right";

			GetComponent<SpriteRenderer>().flipX = false;


		}
		else {
			anim.SetBool("Idle", true);
			anim.SetBool("WalkRight", false);
		}


		//Control Scheme left arrow
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			anim.SetBool("Idle", false);
			facingRight = false;
			Debug.Log("going left");
			//anim.SetBool("right", false);
			//anim.SetBool("left", true);

			if (isOnGround && (crouching == false))
			{
				transform.Translate(Vector2.left * Speed/2 * Time.deltaTime);
			}
			else if (!isOnGround)
			{
				transform.Translate(Vector2.left * Speed/3 * Time.deltaTime);
			}
			anim.SetBool("WalkLeft", true);

			//facedirection = "left";

			GetComponent<SpriteRenderer>().flipX = true;

		}
		else {
			anim.SetBool("Idle",true);
			anim.SetBool("WalkLeft",false);
		}


		if (Input.GetKey(KeyCode.F))
		{
			Debug.Log("fire version 1");
            //Debug.Log("fire version 2");
            //Fire();
            //Fire2();

            //call method that controls the current selected weapon
            WeaponController();
        }
		else//if nothing is done reset created
		{
			anim.SetBool("Fire", false);
			isCreated = false;
		}



		//jump
		if (Input.GetKey(KeyCode.Space) && isOnGround)
		{
			if (!crouching)
			{
				Vector3 v3 = GetComponent<Rigidbody>().velocity;
				v3.y = 0;
				GetComponent<Rigidbody>().velocity = v3;
				anim.SetBool("Jumping", true);
				Debug.Log("jumping on");
				//anim.SetTrigger("Jumping");
				GetComponent<Rigidbody>().AddForce(new Vector2(0, JumpSpeed), ForceMode.Impulse);
				if (!isOnGround)
				{
					anim.SetBool("Jumping", false);
					Debug.Log("Jumping state");
				}
				if (isOnGround)
				{
					anim.SetBool("Idle", true);
					Debug.Log("Idle On");
				}
			}
		}
		else
		{
			anim.SetBool("Jumping", false);
			Debug.Log("jumping off");
		}


		//check gravity 
		if (!isOnGround)
		{

			Physics.gravity = new Vector3(0, -20.0F, 0);
		}

		//checking crouching condition
		if (Input.GetKey(KeyCode.DownArrow))
		{
			if (isOnGround)
			{
				crouching = true;
				anim.SetBool("Crouching", true);
				boxcol.enabled = false;//close the box collider
				//fix character on x position
				player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
			}
		}
		else
		{
			crouching = false;
			boxcol.enabled = true; //reenable the box collider
			anim.SetBool("Crouching", false);
			player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ|RigidbodyConstraints.FreezeRotation;
		}
	}


	public void TakeDamage(int amount) {

		Debug.Log("take damage: ");
		if (invincible) //only trigger when the player is hit once...trigger invincible for a short duration
		{
			Debug.Log("no damage dealt"); 
			curHealth -= 0;
			Debug.Log(curHealth);
		}
		else
		{
			if (!invincible) {
				//damage = true;
				curHealth -= amount;
				Debug.Log(curHealth);
			}

		}

		if (curHealth <= 0 && !isDead)
		{
			Debug.Log("You are Dead!!!");
			Death(); //or pause the game and bring up menu
		}
	}

	public float getCurHealth()
	{
		return curHealth;
	}

	public float getMaxHealth(){
		return maxHealth;
	}

	public void Death()
	{
		isDead = true;
		Debug.Log("Player is DEAD!!!");
		//anim.SetTrigger("Die");
		//display death animation and stop all action (movement, ability)
	}


	void LateUpdate()
	{
		//apply movement slowly
		transform.Translate(velocity * Time.deltaTime);
	}

	//press F key to fire weapon
	// version 1...can only single fire
	void Fire()
	{
		Debug.Log("Fire1");

		if (!isCreated)
		{
			anim.SetBool("Fire", true);
			InstantiateBullet();
			isCreated = true;
		}
	}

	//version 2...can hold fire
	void Fire2()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			timer += 1;
		}
		if (timer > 0)
		{
			Debug.Log("Fire2");
			anim.SetBool("Fire", true);
			InstantiateBullet();
		}
	}

    //default machine gun to fire at approx 400RPM
    void Fire3()
    {
        Debug.Log("Fire3");
        anim.SetBool("Fire", true);
        InvokeRepeating("InstantiateBullet", 0.2f, 0.15f);
    }

    //Flamethrower weapon, needs a set range and parabolic motion
    void Fire4()
    {
        Debug.Log("Fire4");
        anim.SetBool("Fire", true);
        InvokeRepeating("InstantiateFlame", 0.2f, 0.75f);
    }

    void Fire5()
    {
        Debug.Log("Fire5");
        anim.SetBool("Fire", true);
        InvokeRepeating("InstantiateBullet", 0.05f, 0.95f);
    }

    void WeaponController()
    {
        if (weaponToggleKey == "Fire3")
        {
            if (!IsInvoking("InstantiateBullet"))
            {
                Fire3();
            }
        }
        else if (weaponToggleKey == "Fire4")
        {
            if (!IsInvoking("InstantiateBullet"))
            {
                Fire4();
            }
        }
        else if (weaponToggleKey == "Fire5")
        {
            if (!IsInvoking("InstantiateBullet"))
            {
                Fire5();
            }
        }
    }


    void WeaponToggle()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Weapon Switched to 1");
            weaponToggleKey = "Fire3";
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Weapon Switched to 2");
            weaponToggleKey = "Fire4";
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Weapon Switched to 3");
            weaponToggleKey = "Fire5";
        }
    }

    //Function just to get face direction
    Vector3 getFacePosition(){
		Vector3 position = GetComponent<Transform>().position;//gets character position
		if (facingRight)
		{
			position = position + new Vector3((float)1.25, (float)0.25, 0);
			//GetComponent<SpriteRenderer>().flipX = false;
		}
		else
		{
			position = position + new Vector3(-(float)1.25,(float)0.25, 0);
			//GetComponent<SpriteRenderer>().flipX = true;
		}
		return position;
	}
	//Allow the instantiation of bullet...or whatever comes out
	void InstantiateBullet()
	{
		GameObject Clone;
		Vector3 position = getFacePosition();
		Clone = (Instantiate(bullet, position, Quaternion.identity)) as GameObject;
		Debug.Log("Fire");
		if (facingRight)
		{
			Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.right.normalized * bulletSpeed, ForceMode2D.Impulse);

		}
		else
		{
			Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left.normalized * bulletSpeed, ForceMode2D.Impulse);

		}
	}

    //Allow the instantiation of "Flames"

    void InstantiateFlame()
    {
        GameObject CloneFlame;
        Vector3 position = getFacePosition();
        CloneFlame = (Instantiate(flame, position, Quaternion.identity)) as GameObject;
        Debug.Log("Firing Flame");
        if (facingRight)
        {
            CloneFlame.GetComponent<Rigidbody2D>().AddForce(Vector2.one.normalized * flameSpeed, ForceMode2D.Impulse);
        }
        else
        {
            CloneFlame.GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * flameSpeed, ForceMode2D.Impulse);
        }
    }


    void OnCollisionEnter(Collision col) //only checks enter
	{	
		if(col.gameObject.tag == "ground"){
			Debug.Log("is on ground");
			isOnGround = true;
			anim.SetBool("Ground", true);
		}

	}
	void OnCollisionStay(Collision col) //checks stay because player wont reenter
	{
		if (col.gameObject.tag == "ground"){
			Debug.Log("is on ground");
			isOnGround = true;
		}

	}

	void OnCollisionExit()
	{
		Debug.Log("is off ground");
		isOnGround = false;
		anim.SetBool("Ground", false);
	}

}	
