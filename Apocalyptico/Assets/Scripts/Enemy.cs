using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed;
    public Transform player;

	// Use this for initialization
	void Start () {
        speed = -3f;
        GetComponent<SpriteRenderer>().flipX = false;
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;

        position = new Vector2(position.x + speed * Time.deltaTime, position.y);

        transform.position = position;
	}

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject collidedWith = coll.transform.gameObject;

        if (collidedWith.tag == "Box")
        {
            Debug.Log("hit");
            speed = -speed;

            // does not work if collider needs to be swapped too
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
        }
    }
}
