using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
    public Vector3 differenceVector;

    // Use this for initialization
    void Start ()
    {
        differenceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        differenceVector.Normalize();

        GetComponent<Rigidbody2D>().velocity = differenceVector * 50;
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    void OnTriggerEnter2D (Collider2D coll)
    {
        if (coll.gameObject.tag != "Player")
        {
            Destroy(gameObject);
        }
    }
}
