using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    float moveSpeed = 9;
    float gravity;

    public GameObject bullet;
    public float spawnTime;

    EnemyController controller;

    private Transform player;
    private bool shooting = false;
    private bool dead = false;
    private Vector2 move;
    private float distanceX;
    private GameObject newBullet;

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
        if (!shooting && !dead)
        {
            distanceX = player.position.x - transform.position.x;
            if (distanceX >= 10f)
            {
                move = new Vector2(moveSpeed, gravity);
                anim.SetBool("Walk", true);
                controller.Move(move * Time.deltaTime);
            }
            else if (distanceX <= -10f)
            {
                move = new Vector2(-moveSpeed, gravity);
                anim.SetBool("Walk", true);
                controller.Move(move * Time.deltaTime);
            }

            if (distanceX < 10f && distanceX > -10f)
            {
                anim.SetBool("Walk", false);
            }
        }

        if (GetComponent<EnemyHP>().hp <= 0)
        {
            tag = "Dead";
            dead = true;
            anim.SetBool("Dead", true);
            Destroy(gameObject, 0.75f);
        }

        if (Vector2.Distance(transform.position, player.position) <= 10.5f && !shooting && !dead)
        {
            anim.SetBool("Shoot", false);
            spawnTime -= Time.deltaTime;

            if (spawnTime <= 0)
            {
                StartCoroutine("Shoot");
            }
        }
    }

    IEnumerator Shoot()
    {
        shooting = true;
        anim.SetBool("Shoot", true);
        yield return new WaitForSeconds(1f);
        newBullet = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity);

        if (GetComponent<SpriteRenderer>().flipX == false)
        {
            newBullet.GetComponent<EnemyBullet>().left = true;
        }
        else
        {
            newBullet.GetComponent<EnemyBullet>().left = false;
        }

        anim.SetBool("Shoot", false);
        yield return new WaitForSeconds(.7f);
        spawnTime = 3f;
        shooting = false;
    }
}
