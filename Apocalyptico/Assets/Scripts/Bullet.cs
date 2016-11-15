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
            direction = new Vector2(-1.0f, 4.0f);
        }
        else
        {
            direction = new Vector2(1.0f, 4.0f);
        }

        this.GetComponent<Rigidbody>().AddForce(direction * 225f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter(Collision coll)
    {
        Destroy(gameObject);
    }
}