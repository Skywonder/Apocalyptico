using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField]
    Transform character;

    public float startz;
    private Vector3 moveTemp;
    [SerializeField]
    float speed = 3;
    [SerializeField]
    float xDifference;
    [SerializeField]
    float yDifference;

    [SerializeField]
    float movementThreshold = 3;
    
	// Update is called once per frame
	void Update () {

        if (character.transform.position.x > transform.position.x)
        {
            xDifference = character.transform.position.x - transform.position.x;
        }
        else
        {
            xDifference = transform.position.x - character.transform.position.x;

        }

        if (character.transform.position.y > transform.position.y)
        {
            yDifference = character.transform.position.y - transform.position.y;
        }
        else
        {
            yDifference = transform.position.y - character.transform.position.y;
        }

        if (xDifference >= movementThreshold || yDifference >= movementThreshold)
        {
            moveTemp = character.transform.position;
            //moveTemp.y = 1;
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, transform.position.y, startz), moveTemp, speed * Time.deltaTime);
        }
	}
}
