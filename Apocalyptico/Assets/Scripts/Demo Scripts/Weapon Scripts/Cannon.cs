using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon {
    public GameObject bullet;

    public float magazine = 1f;
    public float setMagazine = 1f;
    public float ammo = 1f;
    
    private GameObject newBullet;
    
    public override void Fire()
    {
        if (magazine != 0)
        {
            newBullet = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            magazine--;
        }
    }

    public override void Reload()
    {
        magazine = setMagazine;
        ammo--;
    }

    public override bool FullClip()
    {
        return magazine == setMagazine;
    }

    public override bool AmmoCheck()
    {
        return ammo > 0f;
    }
}
