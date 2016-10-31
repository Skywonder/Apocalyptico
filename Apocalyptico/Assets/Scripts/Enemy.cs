using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed = 3f;
    public Transform player;
    public bool isOnGround = false;    

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        GetComponent<SpriteRenderer>().flipX = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isOnGround)
        {
            Physics2D.gravity = new Vector2(0, -10f);
        } else
        {
            //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // drop then follows
        }

        if (transform.position.x < player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // strafes towards
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isOnGround = true;
        }

        /*GameObject collidedWith = coll.transform.gameObject;

        if (collidedWith.tag == "Box")
        {
            Debug.Log("hit");
            speed = -speed;

            // does not work if collider needs to be swapped too
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }*/
    }

    void OnCollisionStay2D(Collision coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit2D()
    {
        isOnGround = false;
    }
}
