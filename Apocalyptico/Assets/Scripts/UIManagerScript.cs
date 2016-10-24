using UnityEngine;
using System.Collections;

public class UIManagerScript : MonoBehaviour {

	// Use this for initialization
	public void StartGame () {

        Application.LoadLevel("Kuan (working scene)");
	}
	
	// Update is called once per frame
	public void ExitGame () {
        Application.Quit();
	}
}
