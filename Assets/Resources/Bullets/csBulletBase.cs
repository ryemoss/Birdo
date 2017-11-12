using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBulletBase : MonoBehaviour
{
    public GameObject nextbullet;
    public int health = 1;
    public int children = 0;
    private GameObject next;

	void Start() 
	{
	
	}

    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1.1 || Camera.main.WorldToViewportPoint(transform.position).x < -.1 ||
            Camera.main.WorldToViewportPoint(transform.position).y > 1.5 || Camera.main.WorldToViewportPoint(transform.position).y < -.1)
        {
            Destroy(gameObject);
        }
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(GetComponent<Rigidbody2D>().velocity.y, GetComponent<Rigidbody2D>().velocity.x) * Mathf.Rad2Deg);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bird")
        {
            health--;
            if (health <= 0)
            {
                for (int x = 0; x < children; x++)
                {
                    Vector2 velo = GetComponent<Rigidbody2D>().velocity;
                    next = Instantiate(nextbullet, transform.position, Quaternion.identity, transform.parent) as GameObject;
                    next.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(.3f, 1.0f) * velo.x, Random.Range(.3f, 1.0f) * velo.y);
                }
                Destroy(gameObject);
            }
        }
    }

}