using UnityEngine;
using System.Collections;

public class Walker2D : MonoBehaviour {
    public int hp = 1;
    
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 3;

    float gravity;
    Vector3 velocity;
    float velocityXSmoothing;

    RichardController controller;

    private Transform player;

    // Use this for initialization
    void Start () {
        controller = GetComponent<RichardController>();

        gravity = -10;

        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move;
        
        if (player.position.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            move = new Vector2(moveSpeed, gravity);
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
            move = new Vector2(-moveSpeed, gravity);
        }

        controller.Move(move * Time.deltaTime);

        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
