﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed = 9f;
    public Transform player;
    public bool isOnGround = false;
    public Transform spawner;  

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        GetComponent<SpriteRenderer>().flipX = false;
        transform.position = spawner.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (!isOnGround)
        {
            Physics.gravity = new Vector3(0, -10f, 0);
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
