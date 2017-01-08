using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCrate : MonoBehaviour {
    Cannon cannon;
    public float ammo = 3;

    // Use this for initialization
    void Start()
    {
        cannon = GameObject.Find("Weapon").GetComponent<Cannon>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            cannon.ammo += ammo;
            Destroy(gameObject);
        }
    }
}
