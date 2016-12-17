using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InGameMenuScript : MonoBehaviour {

    public Canvas quitMenu;
    public Button resumeText;
    public Button exitText;

    void Start()
    {
        quitMenu = quitMenu.GetComponent<Canvas>();
        resumeText = resumeText.GetComponent<Button>();
        exitText = exitText.GetComponent<Button>();

        quitMenu.enabled = false;
    }
    
    public void ExitPress()
    {
        //enable menu to confirm exit
        quitMenu.enabled = true;
        resumeText.enabled = false;
        exitText.enabled = false;
        //disable other buttons not relevant
        resumeText.gameObject.SetActive(false);
        exitText.gameObject.SetActive(false);
    }


    public void NoPress()
    {
        //if no is pressed, re-enable menu buttons
        resumeText.gameObject.SetActive(true);
        exitText.gameObject.SetActive(true);
        //disable confirm exit menu
        quitMenu.enabled = false;
        resumeText.enabled = true;
        exitText.enabled = true;

    }
    /*
    public void Resume()
    {
        //disable the canvas and resume the game
        //canvas.gameObject.SetActive(false);
        //resume time to normal speed, anything above 1 is fast!
        Time.timeScale = 1;
    }
    */

    //WHEN RETRY IS CLICKED
    public void StartLevel()
    {
        Application.LoadLevel("JohnTest");
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}

