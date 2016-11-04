using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
    public GameObject enemy;
    public Transform player;
    public float spawnTime = 4.0f;
    public int maxSpawn = 5;

    private GameObject newEnemy;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, player.position) <= 5f && maxSpawn != 0)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0 )
            {
                newEnemy = (GameObject)Instantiate(enemy, new Vector2(-3, 2), Quaternion.identity);
                spawnTime = 4f;
                maxSpawn -= 1;
            }
        }
	}
}
