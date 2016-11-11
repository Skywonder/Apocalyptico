using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    public float speed = 7f;
    public Transform player;
    public GameObject bullet;
    public bool isOnGround = false;

    public float spawnTime;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").transform;
        GetComponent<SpriteRenderer>().flipX = false;

        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnGround)
        {
            Physics.gravity = new Vector3(0, -10f, 0);
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
            offset = new Vector2(transform.position.x - 1.5f, transform.position.y);
        }
        else
        {
            offset = new Vector2(transform.position.x + 1.5f, transform.position.y);
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

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
    }

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
}
