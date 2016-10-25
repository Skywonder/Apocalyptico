using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public bool left;
    public float speed;

    // Use this for initialization
    void Start()
    {
        if (left)
        {
            speed = -2.5f;
        }
        else
        {
            speed = 2.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        transform.position = position;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        Destroy(gameObject);
    }
}