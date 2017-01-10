using UnityEngine;
using System.Collections;

public class EnemyInfSpawner : MonoBehaviour {
    public int hp = 3;

    public GameObject enemy;
    public float setSpawnTime = 1.5f;

    public float spawnTime;
    private Transform player;
    private GameObject newEnemy;
    Animator anim;

    // Use this for initialization
    void Start () {
	    player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();

        spawnTime = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<EnemyHP>().hp <= 0)
        {
            StopCoroutine(Spawn());
            Destroy(gameObject);
        }

        if (Vector2.Distance(transform.position, player.position) <= 15)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                StartCoroutine(Spawn());
                spawnTime = setSpawnTime;
            }
        }
    }

    IEnumerator Spawn()
    {
        anim.SetBool("Spawn", true);
        yield return new WaitForSeconds(0.3f);
        newEnemy = (GameObject)Instantiate(enemy, transform.position, Quaternion.identity);
        anim.SetBool("Spawn", false);
    }
}
