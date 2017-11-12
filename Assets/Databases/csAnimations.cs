using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csAnimations : MonoBehaviour
{
    private GameObject go;
    private string effect;
    private bool run;
    private float t, interval, intensity;
    private bool reverse;

	void Start() 
	{
	}

	void Update()
	{
        if (effect == "shrink" && run == true)
            Shrink();
        else if (effect == "pulse" && run == true)
            Pulse();
        else if (effect == "throb" && run == true)
            Throb();
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

    public void GetParams(GameObject gameobj, string eff, float interv, float intense)
    {
        if (run == false)
        {
            go = gameobj;
            effect = eff;
            interval = interv;
            intensity = intense;
            t = 0;
            run = true;
        }
    }

    public void Pulse()
    {
        t += Time.deltaTime;
        go.transform.localScale = new Vector3((Mathf.Sin(t / interval)) * intensity + 1, (Mathf.Sin(t / interval)) * intensity + 1);
    }

    public void Throb()
    {
        t += Time.deltaTime;

        if (t <= interval)
            go.transform.localScale = new Vector3((Mathf.Sin(3.14f*t / interval)) * intensity + 1, (Mathf.Sin(3.14f * t / interval)) * intensity + 1);
        else if (t > interval)
            go.transform.localScale = new Vector3((Mathf.Sin(3.14f * t / interval)) * intensity*.1f + 1, (Mathf.Sin(3.14f * t / interval)) * intensity * .1f + 1);
    }
}