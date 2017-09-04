using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class csPlayer: MonoBehaviour
{
    public GameObject Healthbar;
    private float health = 0;

    private bool flash;

	void Start ()
	{
        if (PlayerData.currentlevel.loc == -1)
            transform.parent.position = new Vector2(-14f, 0f);
        else if (PlayerData.currentlevel.loc == 0)
            transform.parent.position = new Vector2(0, 0f);
    }

    void Update ()
	{
        if (flash == true)
        {
            Flashred();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bomb")
        {
            Decreasehealth();
        }
    }

    private void Decreasehealth()
    {
        flash = true;
        Healthbar.GetComponent<Slider>().value -= .1f;
        health = Healthbar.GetComponent<Slider>().value;

        if (health <= 0)
        {
            GameObject.Find("LevelManager").GetComponent<csLevelManager>().LevelFailed();
        }
    }

    float t;
    bool rev = false;
    SpriteRenderer spr;
    void Flashred()
    {
        spr = transform.GetComponent<SpriteRenderer>();
        t += Time.deltaTime;
        if (rev == false)
            spr.color = new Color(Mathf.Lerp(1, .5f, t / .08f), Mathf.Lerp(1, .5f, t / .08f), Mathf.Lerp(1, .5f, t / .08f));
        else if (rev == true)
            spr.color = new Color(Mathf.Lerp(.5f, 1f, t / .08f), Mathf.Lerp(.5f, 1f, t / .08f), Mathf.Lerp(.5f, 1f, t / .08f));
        if (spr.color.g == .5f)
        {
            rev = true;
            t = 0;
        }
        else if (spr.color.g == 1 && rev == true)
        {
            rev = false;
            flash = false;
            t = 0;
        }
    }
}