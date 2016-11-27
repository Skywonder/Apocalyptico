using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public GameObject reloadTime;
    private int time;
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
        time = (int)reloadTime.GetComponent<PlayerScript>().gunTimer;
        if (time > 5)
        {
            text.text = "RELOAD: \"R\" KEY";
        }
        else
            text.text = "";

        Debug.Log("Time: " + time);
        */
    }
}
