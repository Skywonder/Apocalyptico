using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolShooter : MonoBehaviour {
    public int hp = 1;
    public float distanceX;

    float moveSpeed = 7f;
    float gravity = 10f;

    EnemyController controller;

    public GameObject bullet;
    public float spawnTime;

    private float startPointX;
    private float endPointX;
    private bool towardsEnd = false;
    private Transform player;
    private Vector2 move;
    Animator anim;

    // Use this for initialization
    void Start () {
        controller = GetComponent<EnemyController>();

        gravity = -10;

        player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();

        spawnTime = 0f;
        startPointX = transform.position.x;
        endPointX = transform.position.x + distanceX;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > endPointX)
        {
            towardsEnd = false;
        }
        else if (transform.position.x < startPointX)
        {
            towardsEnd = true;
        }

        if (towardsEnd)
        {
            move = new Vector2(moveSpeed, gravity);
        }
        else
        {
            move = new Vector2(-moveSpeed, gravity);
        }

        controller.Move(move * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            hp -= 1;
        }
    }
}
