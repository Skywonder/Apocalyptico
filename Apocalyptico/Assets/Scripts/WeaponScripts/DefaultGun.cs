using UnityEngine;
using System.Collections;

public class DefaultGun : MonoBehaviour {

    public Rigidbody bullet;
    //weapon magazine size
    public int defaultWeaponMagazine = 30;
    public float gunReloadTimer;
    public float reloadTime = 5;

    //set ability to shoot to true
    private bool canShoot = true;

    //function to enable and disable ability to shoot
    public void SetAbilityToShoot(bool canShoot){
        this.canShoot = canShoot;
    }

	// Use this for initialization
	void Start () {

        gunReloadTimer = 0f;
    }

    // Wait and update if player pushes fire key
    // if fire key is pressed, then instantiate a bullet
    void Update () {
        gunReloadTimer += Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.V))
        {
            if (canShoot)
            {
                Rigidbody newBullet = Instantiate(bullet, transform.position, Quaternion.identity) as Rigidbody;
                newBullet.velocity = transform.forward * 100.0f;
                defaultWeaponMagazine--;
                gunReloadTimer = 0.0f;
            }
        }

	}
    void Reload()
    {
        //reload conditions
        if (gunReloadTimer >= 5)
        {
            defaultWeaponMagazine = 30;
            //weaponToggleKey = "Fire3";
        }
        gunReloadTimer = 0;
    }


}
