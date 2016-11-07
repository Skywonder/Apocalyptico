using UnityEngine;
using System.Collections;

public class AmmoScript : MonoBehaviour {

    // Use this for initialization
    public float time;
    public float countdown = 2; 

    //mainly checking if the bullet hits anything or not...
    //if hit has tag enemy then deal damage, else destroy itself after certain time
    void Update()
    {
        time += 1.0F * Time.deltaTime;

        if (time >= countdown)
        {
            GameObject.Destroy(gameObject);
        }
    }


}
