using UnityEngine;
using System.Collections;

public class PlayerBullet : MonoBehaviour {
    public bool left;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
	    if (left)
        {
            transform.position += new Vector3(-2f, 0, 0);
        } else
        {
            transform.position += new Vector3(2f, 0, 0);
        }
	}
}
