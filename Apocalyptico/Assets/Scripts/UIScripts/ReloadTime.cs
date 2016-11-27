using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReloadTime : MonoBehaviour
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

        //time = (int)reloadTime.GetComponent<PlayerScript>().gunTimer;
       // text.text = "Time: " + (time);
        //Debug.Log("Time: " + time);
    }
}
