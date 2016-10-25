using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    public float speed;
    public Transform player;
    public GameObject bullet;

    private float spawnTime;

    // Use this for initialization
    void Start()
    {
        speed = -1.5f;
        GetComponent<SpriteRenderer>().flipX = false;

        spawnTime = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        Vector2 offset;

        position = new Vector2(position.x + speed * Time.deltaTime, position.y);
        spawnTime -= Time.deltaTime;

        transform.position = position;

        if(GetComponent<SpriteRenderer>().flipX == false) {
            offset = new Vector2(position.x - 1, position.y);
        } else
        {
            offset = new Vector2(position.x + 1, position.y);
        }

        if (spawnTime <= 0)
        {
            Debug.Log("shot");
            GameObject newBullet = (GameObject)Instantiate(bullet, offset, Quaternion.identity);
            if (GetComponent<SpriteRenderer>().flipX == false)
            {
                newBullet.GetComponent<Bullet>().left = true;
            } else
            {
                newBullet.GetComponent<Bullet>().left = false;
            }

            spawnTime = 1.5f;
        }
    }

    void Shoot()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.transform.gameObject;

        if (collidedWith.tag == "Box")
        {
            Debug.Log("hit");
            speed = -speed;

            // does not work if poly collider and needs to be swapped too
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }
}
