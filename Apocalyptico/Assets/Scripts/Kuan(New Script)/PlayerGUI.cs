using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerGUI : MonoBehaviour {

    public GameObject player;
    private int playerhp;
    private int maxhp;
    Text text;

	// Use this for initialization
	void Awake () {
        maxhp = (int)player.GetComponent<PlayerScript>().getMaxHealth();
        playerhp = (int)player.GetComponent<PlayerScript>().getCurHealth();
        playerhp = maxhp;
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {

  
        text.text = "Player Health: " + playerhp;
	}
}
