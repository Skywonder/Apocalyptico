using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon {
    public GameObject bullet;

    public float magazine = 30f;
    public float setMagazine = 30f;

    private GameObject newBullet;

    public AudioClip gunSound;
    private AudioSource source;

    void Start() {
        source = GetComponent<AudioSource>();
    }

    public override void Fire()
    {
        if (magazine != 0)
        {
            source.PlayOneShot(gunSound);
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
