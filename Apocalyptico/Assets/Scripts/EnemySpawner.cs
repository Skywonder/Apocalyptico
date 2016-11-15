using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public float spawnTime = 0.7f;
    public int maxSpawn = 3;

    private Transform player;
    private GameObject newEnemy;

    // Use this for initialization
    void Start () {
	    player = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, player.position) <= 20 && Vector2.Distance(transform.position, player.position) >= 15 && maxSpawn != 0)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                newEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
                spawnTime = 0.7f;
                maxSpawn -= 1;
            }
        }
	}
}
