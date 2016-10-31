using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public float spawnTime = 4.0f;

    private GameObject newEnemy;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0)
        {
            newEnemy = (GameObject)Instantiate(enemy, new Vector2(-3, 2), Quaternion.identity);
            spawnTime = 4f;
        }
	}
}
