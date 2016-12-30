using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : MonoBehaviour {
    public GameObject bullet;

    public float magazine = 70f;
    public float setMagazine = 70f;

    private GameObject newBullet;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {

	}

    public void Fire()
    {
        if (magazine != 0)
        {
            newBullet = (GameObject)Instantiate(bullet, transform.position, Quaternion.identity);
            magazine--;
        }
    }

    public void Reload()
    {
        magazine = setMagazine;
    }
}
