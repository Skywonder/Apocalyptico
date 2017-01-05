using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponUI : MonoBehaviour {
    public GameObject weapon;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = weapon.GetComponent<RichardWeapon>().currentWeapon.name;
    }
}
