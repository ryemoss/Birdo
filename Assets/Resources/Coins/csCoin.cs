using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csCoin: MonoBehaviour
{
    public int value;
    private bool vanish = false;
    private float t = 0;

	void Start ()
	{
        Destroy(gameObject, 1.65f);
        StartCoroutine(Pause());
	}

	void Update ()
	{
        if (vanish == true)
        {
            t += Time.deltaTime;
            transform.localScale = new Vector2(Mathf.Lerp(1, 0, t/.15f), 1);
        }
	}

    private IEnumerator Pause()
    {
        yield return new WaitForSeconds(1.5f);
        vanish = true;
        t = Time.deltaTime;
    }
}