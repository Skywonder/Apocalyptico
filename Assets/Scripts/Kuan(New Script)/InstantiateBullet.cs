using UnityEngine;
using System.Collections;

public class InstantiateBullet : MonoBehaviour {

    public GameObject bullet;
    public float bulletSpeed = 50;
    public Vector3 position;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    position = GetComponent<Transform>().position;//gets object position
        //usually we will get facing direction...but for test purpose bullet always goes to left
        position = position + new Vector3(-(float)3, (float)0.25, 0);

        GameObject Clone;
        Clone = (Instantiate(bullet, position, Quaternion.identity)) as GameObject;
        Debug.Log("Enemy Fire");
        Clone.GetComponent<Rigidbody2D>().AddForce(Vector2.left.normalized * bulletSpeed, ForceMode2D.Impulse);

    }


    
  
    
//Allow the instantiation of bullet...or whatever comes out



    
}
