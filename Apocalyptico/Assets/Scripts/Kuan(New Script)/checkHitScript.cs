using UnityEngine;
using System.Collections;

public class checkHitScript : MonoBehaviour {
    //This script works for every single game object including the player!!!!
    //Can modify depending on need...
    //place the prefab attached by this script to any gameobject as child, then it can take dmg from 2d object with certain tag name
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
 