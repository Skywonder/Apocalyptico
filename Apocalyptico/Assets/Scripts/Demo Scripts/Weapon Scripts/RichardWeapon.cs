using UnityEngine;
using System.Collections;

public class RichardWeapon : MonoBehaviour
{
    public float rotationZAxis;
    public Vector3 differenceVector;
    
    // Machine Gun Stats
    public float fireRate = 0.2f;
    public float lastFire = 0f;

    public bool reloading = false;
    public Weapon currentWeapon;

    // Use this for initialization
    void Start()
    {
        currentWeapon = GetComponent<MachineGun>();
        currentWeapon.name = "Machine Gun";
    }

    // Update is called once per frame
    void Update()
    {
        //Arm Movement
        differenceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        differenceVector.Normalize();

        rotationZAxis = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZAxis);

        // Weapon Reload
        if (Input.GetKeyDown(KeyCode.R) && !currentWeapon.FullClip() && currentWeapon.AmmoCheck() && !reloading)
        {
            StartCoroutine("Reload");
        }

        if ((currentWeapon.name == "Cannon" && Input.GetKeyDown(KeyCode.Alpha1)) || (currentWeapon.name == "Machine Gun" && Input.GetKeyDown(KeyCode.Alpha2)))
        {
            StopCoroutine("Reload");
            reloading = false;
        }

        // Weapon Switch
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = GetComponent<MachineGun>();
            currentWeapon.name = "Machine Gun";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = GetComponent<Cannon>();
            currentWeapon.name = "Cannon";
        }

        // Weapon Fire
        if (Input.GetMouseButton(0) && Time.time > lastFire && !reloading)
        {
            lastFire = Time.time + fireRate;
            currentWeapon.Fire();
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(3f);
        currentWeapon.Reload();
        reloading = false;
    }
}
