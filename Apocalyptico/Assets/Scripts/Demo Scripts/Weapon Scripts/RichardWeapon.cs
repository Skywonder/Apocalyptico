﻿using UnityEngine;
using System.Collections;

public class RichardWeapon : MonoBehaviour {
    public float rotationZAxis;
    public Vector3 differenceVector;

    public GameObject bullet;
    public float setCooldown = .5f;
    public float magazine = 70f;
    public float fireRate = 0.2f;
    public float lastFire = 0f;

    private GameObject newBullet;
    private Vector3 offset;
    private float cooldown;

    // Use this for initialization
    void Start()
    {
        cooldown = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            magazine = 70f;
        }

        //Arm Movement
        differenceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        differenceVector.Normalize();

        rotationZAxis = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotationZAxis);

        //Weapon Fire
        if (Input.GetMouseButton(0) && Time.time > lastFire)
        {
            lastFire = Time.time + fireRate;
            DefaultWeapon();
        }
    }

    void DefaultWeapon()
    {
        if (magazine != 0)
        {
            newBullet = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            magazine--;
        }
    }
}