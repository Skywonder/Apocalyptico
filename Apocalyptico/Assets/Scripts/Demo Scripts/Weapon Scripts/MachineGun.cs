using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon {
    public GameObject bullet;

    public float magazine = 30f;
    public float setMagazine = 30f;

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
    }

    public override bool FullClip()
    {
        return magazine == setMagazine;
    }

    public override bool AmmoCheck()
    {
        return base.AmmoCheck();
    }
}
