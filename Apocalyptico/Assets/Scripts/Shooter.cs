using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
    public int hp = 1;

    float moveSpeed = 7f;
    float gravity = 10f;

    EnemyController controller;

    public GameObject bullet;
    public float spawnTime;

    private Transform player;
    private Vector2 move;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<EnemyController>();

        gravity = -10;

        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();

        spawnTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (20f  > 10f)
        {
            anim.SetBool("Walk", true);
            move = new Vector2(moveSpeed, gravity);
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
}
