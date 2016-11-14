using UnityEngine;
using System.Collections;

public class EnemyInfSpawner : MonoBehaviour {
    public GameObject enemy;
    public float spawnTime = 2.5f;
    public Sprite norm;
    public Sprite spawn;

    private Transform player;
    private GameObject newEnemy;

    // Use this for initialization
    void Start () {
	    player = GameObject.Find("Player").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, player.position) <= 15)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                GetComponent<SpriteRenderer>().sprite = spawn;
                newEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
                spawnTime = 2.5f;
                StartCoroutine(TimeDelay());
            }
        }
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().sprite = norm;
    }
}
