using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WeaponMagazineSize : MonoBehaviour
{

    public GameObject magazineSize;
    private int currentMagSize;
    Text text;

    // Use this for initialization
    void Awake()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        currentMagSize = (int)magazineSize.GetComponent<PlayerScript>().defaultWeaponMagazine;
        text.text = "Magazine Size: " + (currentMagSize);
        Debug.Log("Magazine Size: " + currentMagSize);
        */
    }
}
