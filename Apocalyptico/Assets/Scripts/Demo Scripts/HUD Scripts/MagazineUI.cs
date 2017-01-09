using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagazineUI : MonoBehaviour {
    public GameObject weapon;

    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (weapon.GetComponent<RichardWeapon>().reloading)
        {
            GetComponent<Text>().text = "Reloading";
        }
        else
        {
            Debug.Log(weapon.GetComponent<RichardWeapon>().currentWeapon.name);
            if (weapon.GetComponent<RichardWeapon>().currentName == "Machine Gun")
            {
                GetComponent<Text>().text = weapon.GetComponent<MachineGun>().magazine + "/" + weapon.GetComponent<MachineGun>().setMagazine;
            }
            else if (weapon.GetComponent<RichardWeapon>().currentName == "Cannon")
            {
                GetComponent<Text>().text = weapon.GetComponent<Cannon>().magazine + "/" + weapon.GetComponent<Cannon>().setMagazine;
            }
        }
    }
}
