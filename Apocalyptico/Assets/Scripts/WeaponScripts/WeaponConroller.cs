using UnityEngine;
using System.Collections;

public class WeaponConroller : MonoBehaviour {

    public GameObject bullet; //get the bullet object

    //weapon toggle key
    public string weaponToggleKey;
    public int defaultWeaponMagazine;
    public float gunTimer;

    public int bulletSpeed = 100;

    // Use this for initialization
    void Start () {
        //default weapon setting
        weaponToggleKey = "Fire3";

        //set default magazine size
        defaultWeaponMagazine = 30;
        
        //set reload time to 0
        gunTimer = 0f;

    }

    // Update is called once per frame
    void Update () {
        WeaponToggle();

        //start counting time to know when reload takes place
        gunTimer += Time.deltaTime;

        if (Input.GetKey(KeyCode.F))
        {
            Debug.Log("fire version 1");
            //call method that controls the current selected weapon
            WeaponController();

            //this prevents reloading "twice" 
            if (weaponToggleKey != "Reload")
            {
                gunTimer = 0f;
            }
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            //cancel the repeating invoking of bullets AND method that calls it
            CancelInvoke("WeaponController");
            CancelInvoke("InstantiateBullet");
            CancelInvoke("InstantiateCannon");
            CancelInvoke("InstantiateFlame");
        }
        else//if nothing is done reset created
        {
        //    anim.SetBool("Fire", false);
        //   isCreated = false;
        }
    }

    //default machine gun to fire at approx 400RPM
    void Fire3()
    {
        Debug.Log("Fire3");
        //anim.SetBool("Fire", true);
        InvokeRepeating("InstantiateBullet", 0.2f, 0.15f);
    }

    //Flamethrower weapon, needs a set range and parabolic motion
    void Fire4()
    {
        Debug.Log("Fire4");
        //anim.SetBool("Fire", true);
        InvokeRepeating("InstantiateFlame", 0.25f, 0.95f);
    }

    void Fire5()
    {
        Debug.Log("Fire5");
        //anim.SetBool("Fire", true);
        InvokeRepeating("InstantiateCannon", 0.25f, 0.95f);
    }

    void Reload()
    {
        if (gunTimer >= 3.5)
        {
            defaultWeaponMagazine = 30;
            //need to set to previous selected gun
            weaponToggleKey = "Fire3";
        }
    }

    //fire correct weapon based on user input
    void WeaponController()
    {
        Debug.Log("Weapon controller switch");
        if (weaponToggleKey == "Fire3")
        {
            if (!IsInvoking("InstantiateBullet"))
            {
                Fire3();
            }
        }
        else if (weaponToggleKey == "Fire4")
        {
            if (!IsInvoking("InstantiateFlame"))
            {
                Fire4();
            }
        }
        else if (weaponToggleKey == "Fire5")
        {
            if (!IsInvoking("InstantiateCannon"))
            {
                Fire5();
            }
        }
        else if (weaponToggleKey == "Reload")
        {
            Reload();
        }
    }

    //get input from keyboard or controller
    void WeaponToggle()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Weapon Switched to 1");
            weaponToggleKey = "Fire3";
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Weapon Switched to 2");
            weaponToggleKey = "Fire4";
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("Weapon Switched to 3");
            weaponToggleKey = "Fire5";
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //add previous state feature to prevent reloading twice!!!!!!!!
            gunTimer = 0f;
            Debug.Log("Weapon Switched to Reload");
            weaponToggleKey = "Reload";
        }
    }

    //Allow the instantiation of bullet...or whatever comes out
    void InstantiateBullet()
    {
        GameObject Clone;

        //instantiate the bullet in the direction the cursor is pointing
        Clone = (Instantiate(bullet, GetComponent<Arm>().transform.position, 
            Quaternion.identity)) as GameObject;

        if (defaultWeaponMagazine > 0)
        {
            Vector2 armVector = GetComponent<Arm>().differenceVector;
           
            //add force to the bullet in the direction of the cursor
            Clone.GetComponent<Rigidbody>().AddForce(armVector * bulletSpeed, ForceMode.Impulse);

            defaultWeaponMagazine--;
        }
    }
    //Allow the instantiation of bullet...or whatever comes out
    //cannon still needs to have cursor direction changes.
    void InstantiateCannon()
    {
        GameObject Clone;

        //CHANGE GETCOMPONENT SCRIPT TO NECESSARY SCRIPT
        Clone = (Instantiate(bullet, GetComponent<JohnPlayerScript>().getFacePosition(),
            Quaternion.identity)) as GameObject;


            //set this to get the value from the file
        if (GetComponent<JohnPlayerScript>().facingRight)
        {
            Clone.GetComponent<SpriteRenderer>().flipX = true;
            Clone.GetComponent<Rigidbody>().AddForce(Vector2.right.normalized * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            Clone.GetComponent<SpriteRenderer>().flipX = false;
            Clone.GetComponent<Rigidbody>().AddForce(Vector2.left.normalized * bulletSpeed, ForceMode.Impulse);
        }
    }
}