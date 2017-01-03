using UnityEngine;
using System.Collections;

public class EnemyInfSpawner : MonoBehaviour {
    public int hp = 3;

    public GameObject enemy;
    public float spawnTime = 2.5f;

    private Transform player;
    private GameObject newEnemy;
    Animator anim;

    // Use this for initialization
    void Start () {
	    player = GameObject.Find("Player").transform;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (hp <= 0)
        {
            Destroy(gameObject);
        }

        if (Vector2.Distance(transform.position, player.position) <= 15)
        {
            spawnTime -= Time.deltaTime;
            if (spawnTime <= 0)
            {
                StartCoroutine(Spawn());
                spawnTime = 2.5f;
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

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            hp -= 1;
        }
    }
}
