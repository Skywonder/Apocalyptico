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
}
