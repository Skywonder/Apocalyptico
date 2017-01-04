﻿using UnityEngine;
using System.Collections;

public class RichardWeapon : MonoBehaviour
{
    public float rotationZAxis;
    public Vector3 differenceVector;

    MachineGun machineGun;
    Cannon cannon;

    // Machine Gun Stats
    public float fireRate = 0.2f;
    public float lastFire = 0f;

    public bool reloading = false;
    public string currentWeapon;

    // Use this for initialization
    void Start()
    {
        machineGun = GetComponent<MachineGun>();
        cannon = GetComponent<Cannon>();

        currentWeapon = "Machine Gun";
    }

    // Update is called once per frame
    void Update()
    {
        //Arm Movement
        differenceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        differenceVector.Normalize();

        rotationZAxis = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZAxis);

        // Weapon Switch
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = "Machine Gun";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = "Cannon";
        }

        // Weapon Reload
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("Reload");
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopCoroutine("Reload");
            reloading = false;
        }

        // Weapon Fire
        if (Input.GetMouseButton(0) && Time.time > lastFire)
        {
            lastFire = Time.time + fireRate;
            if (currentWeapon == "Machine Gun")
            {
                machineGun.Fire();
            }
            else if (currentWeapon == "Cannon")
            {
                cannon.Fire();
            }
        }
    }

    IEnumerator Reload()
    {
        if (currentWeapon == "Machine Gun")
        {
            reloading = true;
            yield return new WaitForSeconds(3f);
            machineGun.Reload();
            reloading = false;
        }
        else if (currentWeapon == "Cannon")
        {
            reloading = true;
            yield return new WaitForSeconds(3f);
            cannon.Reload();
            reloading = false;
        }
    }
}
