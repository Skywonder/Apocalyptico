using UnityEngine;
using System.Collections;

public class DefaultGun : MonoBehaviour {

    public Rigidbody bullet;

    //set ability to shoot to true
    private bool canShoot = true;


    //function to enable and disable ability to shoot
    public void SetAbilityToShoot(bool canShoot){
        this.canShoot = canShoot;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Wait and update if player pushes fire key
    // if fire key is pressed, then instantiate a bullet
	void Update () {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (canShoot)
            {
                Rigidbody newBullet = Instantiate(bullet, transform.position, Quaternion.identity) as Rigidbody;
                newBullet.velocity = transform.forward * 100.0f; 
            }
        }

	}
}
