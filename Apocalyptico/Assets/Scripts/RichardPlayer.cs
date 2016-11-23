using UnityEngine;
using System.Collections;

public class RichardPlayer : MonoBehaviour
{
    public float speed = 7f;
    public float jumpSpeed = 10f;

    public bool isOnGround = false;

    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        if (!isOnGround)
        {
            Physics.gravity = new Vector3(0, -10f, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<SpriteRenderer>().flipX = false;
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && isOnGround)
        {
            GetComponent<Rigidbody>().AddForce(new Vector2(0, jumpSpeed) * 950);
        }
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
