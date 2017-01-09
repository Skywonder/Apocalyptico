using UnityEngine;
using System.Collections;

public class RichardPlayer : MonoBehaviour
{
    public int hp = 5;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 10;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    public bool hit;
    public float invicibleTimer = 2.0f;

    RichardController controller;

    void Start()
    {
        controller = GetComponent<RichardController>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);
    }

    void Update()
    {
        if (hit)
        {
            invicibleTimer -= Time.deltaTime;
            GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            if (invicibleTimer <= 0)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                GetComponent<SpriteRenderer>().enabled = true;
                hit = false;
                invicibleTimer = 2.0f;
            }
        } else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
        }

		if (Input.GetAxisRaw("Horizontal") == -1)
		{
			GetComponent<SpriteRenderer>().flipX = true;
		}
		else if (Input.GetAxisRaw("Horizontal") == 1)
		{
			GetComponent<SpriteRenderer>().flipX = false;
		}

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
	
		velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (hp <= 0)
        {
            Camera.main.transform.parent = null;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            hit = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
            hp--;
        }

        if (coll.gameObject.tag == "Obstacle")
        {
            hp = 0;
        }
    }
}
