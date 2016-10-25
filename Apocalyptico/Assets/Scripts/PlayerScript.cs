using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerScript : MonoBehaviour{
    //remove // to activate it 
    //public Animator anim;
    public GameObject player;
    public bool invincible = false;
    public bool invisible = false;
    public bool isDead = false;
    public string statUrl = "Assests/Data Sheets/PlayerStats.csv";
    private BoxCollider2D boxcol;
    //private float x_width;
    //private float y_height;
    //private float x_offset;
    //private float y_offset;
    public float JumpSpeed;
    public float JumpDamping = 0.1f;
    public bool isOnGround = false;

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

    //creates a dictionary that stores float values inside the key "string"
    //public Dictionary<string, int[]> statTable;

    public float Speed = 1.5f; //for movement

    private int curHealth; //<--the only value changing

    public int Level{//Level == powerup <-- if we ever decide to have it
        get { return 1; }
    }

    public int maxHealth { //Max avaliable Hp ...does not change
        get { return 3; }//max health depends on level
    }

    void Awake() {//variable only set on start

        Debug.Log(maxHealth);
        curHealth = maxHealth;
        //x_width = GetComponent<BoxCollider2D>().size.x;
        //y_height = GetComponent<BoxCollider2D>().size.y;
        //x_offset = GetComponent<BoxCollider2D>().offset.x;
        //y_offset = GetComponent<BoxCollider2D>().offset.y;
        //boxcol = player.GetComponent<BoxCollider2D>();
    }

    void start()
    {
        layerMask = LayerMask.NameToLayer("normalCollisions");
        //these sets the animator 
        //anim = GetComponent<Animator>();
        //rbody = GetComponent<Animator>();
        //facedirection = "right";
    }

    void Update(){

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
        //box = new Rect(GetComponent<Collider2D>().bounds.min.x, GetComponent<Collider2D>().bounds.min.y, GetComponent<Collider2D>().bounds.size.x, GetComponent<Collider2D>().bounds.size.y);
        box = new Rect(GetComponent<Collider>().bounds.min.x, GetComponent<Collider>().bounds.min.y, GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.y);
        //check for center and slope

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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("going right");
            //anim.SetBool("right", true);
            //anim.SetBool("left", false);
            if (isOnGround)
            {
                transform.Translate(Vector2.right * Speed/2 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.right * Speed/3 * Time.deltaTime);
            }
            //facedirection = "right";
            //anim.SetBool("WalkRight", true);
        }
        else
        {
            //anim.SetBool("WalkRight", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("going left");
            //anim.SetBool("right", false);
            //anim.SetBool("left", true);
            if (isOnGround)
            {
                transform.Translate(Vector2.left * Speed/2 * Time.deltaTime);
            }
            else
            {
                transform.Translate(Vector2.left * Speed/3 * Time.deltaTime);
            }
            //facedirection = "left";
            //anim.SetBool("WalkLeft", true);
        }
        else
        {
            //anim.SetBool("WalkLeft", false);
        }
        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
           
            Jump();
            
        }

        if (!isOnGround)
        {

            Physics.gravity = new Vector3(0, -20.0F, 0);
        }
        /*
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //anim.SetBool("up", false);
            //anim.SetBool("down", true);
            //instead of transform we change the collider box height 
            //reduce the collider height by 1/2
            //anim.SetBool("Dodge", true);
           
            //needs tuning
            boxcol.size = new Vector2(x_width, y_height/2);
            boxcol.offset = new Vector2(x_offset, y_offset - y_height/2 + 1);
            
        }
        else
        {
            boxcol.size = new Vector2(x_width, y_height);
            boxcol.offset = new Vector2(x_offset, y_offset);
            //anim.SetBool("Dodge", false);
            //restore the collider box height

        }
        */
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
        //anim.SetTrigger("Die");
        //display death animation and stop all action (movement, ability)
    }


    void LateUpdate()
    {
        //apply movement slowly
        transform.Translate(velocity * Time.deltaTime);
    }

    void OnCollisionEnter()
    {
        Debug.Log("is on ground");
        isOnGround = true;
    }
    void OnCollisionExit()
    {
        Debug.Log("is off ground");
        isOnGround = false;
    }

  
    void Jump()
    {

        Vector3 v3 = GetComponent<Rigidbody>().velocity;
        v3.y = 0;
        GetComponent<Rigidbody>().velocity = v3;
        GetComponent<Rigidbody>().AddForce(new Vector2(0, JumpSpeed), ForceMode.Impulse);


    }
}