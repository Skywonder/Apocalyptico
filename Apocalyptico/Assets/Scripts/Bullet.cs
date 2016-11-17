using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public bool left;

    // Use this for initialization
    void Start()
    {
        Vector2 direction;

        if (left)
        {
            direction = new Vector2(-1.0f, 0.5f);
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
            direction = new Vector2(1f, 0.5f);
        }

        this.GetComponent<Rigidbody>().AddForce(direction * 500f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}