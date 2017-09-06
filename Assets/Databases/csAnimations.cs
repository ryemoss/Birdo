using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csAnimations : MonoBehaviour
{
    private GameObject go;
    private string effect;
    private bool run;
    private float t, interval;
    private bool reverse;

	void Start() 
	{
	}

	void Update()
	{
        if (effect == "shrink" && run == true)
            Shrink();
	}

    public void Shrink()
    {
        t += Time.deltaTime;

        if (t >= interval && reverse == false)
        {
            reverse = true;
            t = 0;
        }
        else if (reverse == true && t >= interval)
        {
            run = false;
            reverse = false;
            t = 0;
        }

        if (reverse == false)
            go.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one*.8f, t / interval);
        else
            go.transform.localScale = Vector3.Lerp(Vector3.one * .8f, Vector3.one, t / interval);
    }

    public void Shake()
    {
        t += Time.deltaTime;

        if (t >= interval && reverse == false)
        {
            reverse = true;
            t = 0;
        }
        else if (reverse == true && t >= interval)
        {
            run = false;
            reverse = false;
            t = 0;
        }

        if (reverse == false)
            go.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * .8f, t / interval);
        else
            go.transform.localScale = Vector3.Lerp(Vector3.one * .8f, Vector3.one, t / interval);
    }

    public void GetParams(GameObject gameobj, string eff)
    {
        if (run == false)
        {
            go = gameobj;
            effect = eff;
            interval = .1f;
            t = 0;
            run = true;
        }
    }
}