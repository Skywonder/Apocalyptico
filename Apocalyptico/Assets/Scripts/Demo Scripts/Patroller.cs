using UnityEngine;
using System.Collections;

public class Patroller : MonoBehaviour {

    public int hp = 1;
    public float distanceX;

    float moveSpeed = 7;
    float gravity;

    WalkerController controller;
    
    private bool hit = false;
    private float startPointX;
    private float endPointX;
    private bool towardsEnd = false;
    private Vector2 move;
    Animator anim;

    // Use this for initialization
    void Start()
    {
        controller = GetComponent<WalkerController>();

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
