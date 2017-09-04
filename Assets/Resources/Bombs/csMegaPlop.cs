using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csMegaPlop: MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 20f);
    }

	void Update ()
	{
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bullet")
        {
            Destroy(gameObject);
        }
    }

}