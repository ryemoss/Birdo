using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csBomb: MonoBehaviour
{
    private float miny;

    void Start()
    {
        miny = -Camera.main.orthographicSize * 1.2f;
    }

	void Update ()
	{
        if (transform.position.y < miny)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "player")
        {
            Destroy(gameObject);
        }
    }

}