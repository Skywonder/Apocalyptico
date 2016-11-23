using UnityEngine;
using System.Collections;

public class Walker2D : MonoBehaviour {
    public int hp = 1;
    
    float moveSpeed = 3;
    float gravity;

    WalkerController controller;

    private Transform player;
    private bool hit = false;
    Animator anim;

    // Use this for initialization
    void Start () {
        controller = GetComponent<WalkerController>();

        gravity = -10;

        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            Vector2 move;

            if (player.position.x > transform.position.x)
            {
                GetComponent<SpriteRenderer>().flipX = true;
                move = new Vector2(moveSpeed, gravity);
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
                move = new Vector2(-moveSpeed, gravity);
            }

            controller.Move(move * Time.deltaTime);
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            hit = true;
            anim.SetBool("Hit", true);
            Destroy(gameObject, 0.75f);
        }
    }
}
