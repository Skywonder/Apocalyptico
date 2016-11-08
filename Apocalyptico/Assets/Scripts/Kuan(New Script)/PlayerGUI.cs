using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour {

    public GameObject hitchecker;
    public GameObject player;
    private int hithp; //temp hp for calculation
    private int maxhp;//set max hp for status max
    Text text;

	// Use this for initialization
	void Awake () {
        maxhp = (int)player.GetComponent<PlayerScript>().getMaxHealth();
        hithp = (int)hitchecker.GetComponent<checkHitScript>().hitpoint;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

  
        text.text = "Player Health: " + hithp;
	}
}
