using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {
    public bool left;

    // Use this for initialization
    void Start()
    {
        Vector2 direction;

        if (left)
        {
            direction = new Vector2(-1.0f, 0.1f);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            direction = new Vector2(1f, 0.1f);
        }

        this.GetComponent<Rigidbody2D>().AddForce(direction * 2000f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag != "Enemy" && coll.gameObject.tag != "Dead" && coll.gameObject.tag != "Ammo" && coll.gameObject.tag != "Bullet")
        {
            Destroy(gameObject);
        }
    }
}
