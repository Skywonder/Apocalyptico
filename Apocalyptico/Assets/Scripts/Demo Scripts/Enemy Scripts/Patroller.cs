using UnityEngine;
using System.Collections;

public class Patroller : MonoBehaviour {
    public float distanceX;

    float moveSpeed = 7;
    float gravity;

    EnemyController controller;
    
    private bool hit = false;
    private float startPointX;
    private float endPointX;
    private bool towardsEnd = false;
    private Vector2 move;
    Animator anim;
    public AudioClip explodeSound;
    private AudioSource source;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        controller = GetComponent<EnemyController>();

        gravity = -10;

        startPointX = transform.position.x;
        endPointX = transform.position.x + distanceX;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hit)
        {
            if (transform.position.x > endPointX)
            {
                towardsEnd = false;
            } else if (transform.position.x < startPointX) {
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
            source.PlayOneShot(explodeSound);
            anim.SetBool("Hit", true);
            Destroy(gameObject, 0.75f);
        }
    }
}
