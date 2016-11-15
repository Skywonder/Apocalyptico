using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public float speed = 9f;
    public bool isOnGround = false;

    private Transform player;
    private bool hit;
    Animator anim;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player").GetComponent<Transform>();
        hit = false;
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isOnGround)
        {
            Physics.gravity = new Vector3(0, -10f, 0);
        }

        if (transform.position.x < player.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        } else
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        
        if (!hit)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
        
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Hit");
            hit = true;
            StartCoroutine(Explode());
        }
    }

    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "ground")
        {
            isOnGround = true;
        }
    }

    void OnCollisionExit()
    {
        isOnGround = false;
    }

    IEnumerator Explode()
    {
        anim.SetBool("Hit", true);
        yield return new WaitForSeconds(0.75f);
        Destroy(gameObject);
    }
}
