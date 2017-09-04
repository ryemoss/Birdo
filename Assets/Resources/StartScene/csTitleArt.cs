using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csTitleArt: MonoBehaviour
{

	void Start ()
	{
        foreach (Transform child in transform)
            child.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * Random.Range(.45f,.55f);
    }

	void Update ()
	{
        foreach (Transform child in transform)
            Parralax(child.gameObject);
	}
        
    private void Parralax(GameObject cloud)
    {
        if (cloud.transform.position.x - cloud.GetComponent<SpriteRenderer>().bounds.extents.x > Camera.main.orthographicSize * 2f)
        {
            cloud.transform.position = new Vector2(-(Camera.main.orthographicSize * Screen.width/Screen.height + cloud.GetComponent<SpriteRenderer>().bounds.extents.x*1.1f), Random.Range(-Camera.main.orthographicSize, Camera.main.orthographicSize));
        }
    }
}