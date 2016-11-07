using UnityEngine;
using System.Collections;

public class checkHitScript : MonoBehaviour {

	// Use this for initialization
	public int hitpoint = 20;


    void Update()
    {
        dead();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Hit by Object");
        if (collision.gameObject.tag == "bullet")
        {
            Debug.Log("Destroy object");
            Destroy(collision.gameObject);
            takehit();
        }

    }

    void takehit()
    {
        Debug.Log(hitpoint);
        if (hitpoint > 0)
        {
            hitpoint -= 1;
        }
        else if (hitpoint <= 0)
        {
            hitpoint = 0;
        }
    }

    void dead()
    {
        if (hitpoint == 0)
        {
            Debug.Log("Destroy the enemy");
            //destroy this with 
            Destroy(transform.parent.gameObject);
        }
    }


}
 