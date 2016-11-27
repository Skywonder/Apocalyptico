using UnityEngine;
using System.Collections;

public class InGameMenuManager : MonoBehaviour
{
    //int playerLife;
    //public GameObject hitchecker;
    public Transform canvas;

    public void ResumeGame()
    {
        //disable the canvas and resume the game
        canvas.gameObject.SetActive(false);
        //resume time to normal speed, anything above 1 is fast!
        Time.timeScale = 1;
    }


    void Update()
    {
        //playerLife = (int)hitchecker.GetComponent<checkHitScript>().hitpoint;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(canvas.gameObject.activeInHierarchy == false)
            {
                //enable it in the game
                canvas.gameObject.SetActive(true);
                //slow down time to a halt, therefore pausing the game.
                Time.timeScale = 0;
            }
            else
            {
                //disable if key is not pressed
                canvas.gameObject.SetActive(false);
                //resume time to normal speed, anything above 1 is fast!
                Time.timeScale = 1;
            }
        }
    }
}
