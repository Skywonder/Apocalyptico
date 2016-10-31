using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    public float speed;
    public Transform player;
    public GameObject bullet;
    public bool isOnGround = false;

    private float spawnTime;

    // Use this for initialization
    void Start()
    {
        speed = 1.5f;
        GetComponent<SpriteRenderer>().flipX = false;

        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnGround)
        {
            Physics2D.gravity = new Vector2(0, -10f);
        }
        else
        {
            //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // drop then follows
        }

        if (transform.position.x < player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Vector2.Distance(transform.position, player.position) > 5f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime); // strafes towards
        }

        if (Vector2.Distance(transform.position, player.position) <= 5f)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 offset;

        spawnTime -= Time.deltaTime;

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            offset = new Vector2(transform.position.x - 1, transform.position.y);
        }
        else
        {
            offset = new Vector2(transform.position.x + 1, transform.position.y);
        }

        if (spawnTime <= 0)
        {
            Debug.Log("shot");
            GameObject newBullet = (GameObject)Instantiate(bullet, offset, Quaternion.identity);
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                newBullet.GetComponent<Bullet>().left = true;
            }
            else
            {
                newBullet.GetComponent<Bullet>().left = false;
            }

            spawnTime = 3f;
        }
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
