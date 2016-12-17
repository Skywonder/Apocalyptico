using UnityEngine;
using System.Collections;

public class Arm : MonoBehaviour {

    public GameObject arm;
    public float rotationZAxis;
    public Vector3 differenceVector;
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        differenceVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        differenceVector.Normalize();

        rotationZAxis = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;

        arm.transform.rotation = Quaternion.Euler(0f, 0f, rotationZAxis);
    }
}
