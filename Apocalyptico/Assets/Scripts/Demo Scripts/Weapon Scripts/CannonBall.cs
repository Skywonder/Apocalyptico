﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {
    public Vector3 differenceVector;

    // Use this for initialization
    void Start()
    {
        differenceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        differenceVector.Normalize();

        GetComponent<Rigidbody2D>().velocity = differenceVector * 100;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y < min.y || transform.position.x < min.x || transform.position.y > max.y || transform.position.x > max.x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            coll.gameObject.GetComponent<EnemyHP>().hp -= 3;
        }

        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "Dead" && coll.gameObject.tag != "Ammo")
        {
            Destroy(gameObject);
        }
    }
}
