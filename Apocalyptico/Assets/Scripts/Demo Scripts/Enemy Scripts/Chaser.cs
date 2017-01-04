﻿using UnityEngine;
using System.Collections;

public class Chaser : MonoBehaviour {
    float moveSpeed = 11;
    float gravity;

    EnemyController controller;

    private Transform player;
    private bool hit = false;
    private Vector2 move;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<EnemyController>();

        gravity = -10;

        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            if (player.position.x > transform.position.x)
            {
                move = new Vector2(moveSpeed, gravity);
            }
            else
            {
                move = new Vector2(-moveSpeed, gravity);
            }

            controller.Move(move * Time.deltaTime);
        }

        if (GetComponent<EnemyHP>().hp <= 0)
        {
            tag = "Dead";
            hit = true;
            anim.SetBool("Hit", true);
            Destroy(gameObject, 0.75f);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            tag = "Dead";
            hit = true;
            anim.SetBool("Hit", true);
            Destroy(gameObject, 0.75f);
        }
    }
}
