using UnityEngine;
using System.Collections;

public class RichardPlayer : MonoBehaviour
{
    public int hp = 5;
    public GameObject bullet;
    public float setCooldown = .5f;

    public float jumpHeight = 4;
    public float timeToJumpApex = .4f;
    float accelerationTimeAirborne = .2f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 10;

    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;

    private bool hit;
    private float invicibleTimer = 2.0f;
    private GameObject newBullet;
    private Vector3 offset;
    private float cooldown;

    RichardController controller;

    void Start()
    {
        controller = GetComponent<RichardController>();

        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        jumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        print("Gravity: " + gravity + "  Jump Velocity: " + jumpVelocity);

        cooldown = 0f;
    }

    void Update()
    {
        if (hit)
        {
            invicibleTimer -= Time.deltaTime;
            if (invicibleTimer <= 0)
            {
                GetComponent<BoxCollider2D>().isTrigger = true;
                invicibleTimer = 2.0f;
            }
        }

        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0;
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

        if (Input.GetMouseButtonDown(0) && cooldown <= 0)
        {
            if (GetComponent<SpriteRenderer>().flipX)
            {
                offset = new Vector3(-2.5f, 0f, 0f);
            } else
            {
                offset = new Vector3(2.5f, 0f, 0f);
            }

            newBullet = (GameObject)Instantiate(bullet, transform.position + offset, Quaternion.identity);
            newBullet.GetComponent<PlayerBullet>().left = GetComponent<SpriteRenderer>().flipX;
            cooldown = setCooldown;
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (hp <= 0)
        {
            Destroy(gameObject);
        } 
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            hit = true;
            GetComponent<BoxCollider2D>().isTrigger = false;
            hp -= 1;
        }

        if (coll.gameObject.tag == "Obstacle")
        {
            Camera.main.transform.parent = null; // until camera gets its own script
            Destroy(gameObject);
        }
    }
}
