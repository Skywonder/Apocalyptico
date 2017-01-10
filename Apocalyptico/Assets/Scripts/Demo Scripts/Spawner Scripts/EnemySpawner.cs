using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public float setSpawnTime;
    public int maxSpawn = 3;

    public float spawnTime;
    private Transform player;
    private GameObject newEnemy;

    // Use this for initialization
    void Start () {
	    player = GameObject.Find("Player").transform;
        spawnTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, player.position) <= 30 && Vector2.Distance(transform.position, player.position) >= 25 && maxSpawn != 0)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                newEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
                spawnTime = setSpawnTime;
                maxSpawn -= 1;
            }
        }
	}
}
