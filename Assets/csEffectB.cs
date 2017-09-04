using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csEffectB: MonoBehaviour
{
    public Vector2 shotdir;

	void Start ()
	{
        GetComponent<Rigidbody2D>().velocity = new Vector2(shotdir.x + Random.Range(-1f, 1f), shotdir.y + Random.Range(0, 1f));
        Destroy(gameObject, 1f);
    }

}