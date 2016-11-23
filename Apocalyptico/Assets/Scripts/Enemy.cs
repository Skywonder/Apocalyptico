using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed = 5f;
    public bool isOnGround = false;
    
    private GameObject player;
    private bool hit;
    Animator anim;
    
    //added variables by Kuan
    public int hp; 
    [SerializeField]
    private int curHealth;
    public int maxHealth
    { //Max avaliable Hp ...does not change
        get { return hp; }//max health depends on level
    }
    //end of added variable

    // Use this for initialization
    void Start ()
    {
        player = GameObject.Find("Player");
        hit = false;
        anim = GetComponent<Animator>();
        curHealth = maxHealth;
    }
	
    void FixedUpdate()
    {
        if (!isOnGround)
        {
            Physics.gravity = new Vector3(0, -10f, 0);
        }

        if (transform.position.x < player.GetComponent<Transform>().position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (!hit)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, speed * Time.deltaTime);
        }

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isOnGround = true;
        }

        Debug.Log("Hit by Object");
        if (coll.gameObject.tag == "bullet")
        {
            Debug.Log("Destroy object");
            Destroy(coll.gameObject);
            takehit();
        }
        if (coll.gameObject.tag == "flame")
        {
            Debug.Log("Destroy object with flame");
            Destroy(coll.gameObject);
            takehit();
        }
    
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            hit = true;
            //Physics.IgnoreLayerCollision(9, 13, true);
            Physics.IgnoreCollision(coll.collider, GetComponent<Collider>());
            StartCoroutine(Explode());
        }
    }

    //Added by Kuan

    void takehit()
    {
        Debug.Log(curHealth);
        if (curHealth > 0)
        {
            curHealth -= 1;
        }
        else if (curHealth <= 0)
        {
            curHealth = 0;
        }
    }

    //End of added


    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit()
    {
        isOnGround = false;
    }

    IEnumerator Explode()
    {
        Debug.Log("Exploding");
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }

    

}
