using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    public float speed = 7f;
    public GameObject bullet;
    public bool isOnGround = false;
    public float spawnTime;

    private Transform player;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();

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

        if (Vector2.Distance(transform.position, player.position) > 10f)
        {
            anim.SetBool("Walk", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        } else
        {
            anim.SetBool("Walk", false);
        }

        if (Vector2.Distance(transform.position, player.position) <= 10f)
        {
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0)
            {
                StartCoroutine(Shoot());
            }
        }
    }

    IEnumerator Shoot()
    {
        anim.SetBool("Shoot", true);
        yield return new WaitForSeconds(0.4f);
        GameObject newBullet = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            newBullet.GetComponent<Bullet>().left = true;
        }
        else
        {
            newBullet.GetComponent<Bullet>().left = false;
        }

        anim.SetBool("Shoot", false);
        spawnTime = 3f;
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
