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
    public string currentName;

    // Use this for initialization
    void Start()
    {
        currentWeapon = GetComponent<MachineGun>();
        currentName = "Machine Gun";
    }

    // Update is called once per frame
    void Update()
    {
        if (!transform.parent.GetComponent<RichardPlayer>().dead)
        {
            if (transform.parent.GetComponent<RichardPlayer>().hit)
            {
                GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
            }
            else
            {
                GetComponent<SpriteRenderer>().enabled = true;
            }

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

            if ((currentName == "Cannon" && Input.GetKeyDown(KeyCode.Alpha1)) || (currentName == "Machine Gun" && Input.GetKeyDown(KeyCode.Alpha2)))
            {
                StopCoroutine("Reload");
                reloading = false;
            }

            // Weapon Switch
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                currentWeapon = GetComponent<MachineGun>();
                currentName = "Machine Gun";
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                currentWeapon = GetComponent<Cannon>();
                currentName = "Cannon";
            }

            // Weapon Fire
            if (Input.GetMouseButton(0) && Time.time > lastFire && !reloading)
            {
                lastFire = Time.time + fireRate;
                currentWeapon.Fire();
            }
        } else
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    IEnumerator Reload()
    {
        reloading = true;
        yield return new WaitForSeconds(1f);
        currentWeapon.Reload();
        reloading = false;
    }
}
