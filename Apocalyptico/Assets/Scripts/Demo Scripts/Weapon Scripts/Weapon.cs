using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public virtual void Fire()
    {
        Debug.Log("Weapon Fire");
    }

    public virtual void Reload()
    {
        Debug.Log("Weapon Reload");
    }

    public virtual bool FullClip()
    {
        Debug.Log("Full Clip Check");
        return true;
    }

    public virtual bool AmmoCheck()
    {
        Debug.Log("Enough Ammo Check");
        return true;
    }
}
