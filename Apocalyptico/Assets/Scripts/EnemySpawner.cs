using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public float setSpawnTime;
    public int maxSpawn = 3;

    private Transform player;
    private GameObject newEnemy;
    private float spawnTime;

    // Use this for initialization
    void Start () {
	    player = GameObject.Find("Player").transform;
        spawnTime = setSpawnTime;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, player.position) <= 20 && Vector2.Distance(transform.position, player.position) >= 15 && maxSpawn != 0)
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
