using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MagazineGUI : MonoBehaviour
{

    public GameObject player;
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
        currentMagSize = (int)player.GetComponent<WeaponConroller>().getMagazineSize();
        text.text = "Magazine Size: " + (currentMagSize);
        Debug.Log("Magazine Size: " + currentMagSize);
    }
}
