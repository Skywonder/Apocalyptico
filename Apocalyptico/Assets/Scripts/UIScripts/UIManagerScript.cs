using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour {

	// Use this for initialization
	public void StartGame () {

        SceneManager.LoadScene("Kuan(STAGE)");
    }
	
	// Update is called once per frame
	public void ExitGame () {
        Application.Quit();
	}
}
