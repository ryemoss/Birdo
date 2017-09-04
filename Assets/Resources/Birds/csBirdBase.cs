using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csBirdBase : MonoBehaviour
{
    public GameObject deathparticles;
    public GameObject coinpf, coinpf1;
    public Bird birdtype;

    private GameObject coin;
    private float health;
    private float playerpower;
    public int spawnside;
    private float spawnposy;
    private bool flash = false;
    private bool hit;
    private List<GameObject> hits = new List<GameObject>();

    public void Initializer()
    {
        health = birdtype.health;
        playerpower = PlayerData.power;

        if (PlayerData.currentlevel.loc == -1)
            spawnside = 1;
        else if (PlayerData.currentlevel.loc == 0)
        {
            spawnside = Random.Range(0, 2);
            if (spawnside == 0)
            {
                spawnside = -1;
                transform.GetChild(0).localScale = new Vector2(-1, 1);
            }
        }

        spawnposy = Random.Range(-4f, 8f);
        transform.position = new Vector2(spawnside * Camera.main.orthographicSize * Screen.width/ Screen.height, spawnposy);
    }

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > Camera.main.orthographicSize * 2.15f)
        {
            GameObject.Find("LevelManager").GetComponent<csLevelManager>().ReplaceBird(birdtype);
            Destroy(transform.gameObject);
        }

        if (flash == true)
        {
            Flashred();
        }

        if (hit)
        {
            bool dead = false;
            foreach (GameObject hit in hits)
            {
                GetComponent<AudioSource>().Play();
                flash = true;
                health -= playerpower;
                DestroyImmediate(hit);
                if (health <= 0)
                {
                    PlayerData.roundbirds++;
                    GameObject.Find("LevelManager").GetComponent<csLevelManager>().RemoveBird(birdtype);

                    deathparticles.SetActive(true);
                    deathparticles.transform.parent = transform.parent;
                    deathparticles.transform.position = new Vector3(transform.position.x, transform.position.y, 1);

                    if (Random.Range(0, 2) == 1)
                        coin = Instantiate(coinpf) as GameObject;
                    else
                        coin = Instantiate(coinpf1) as GameObject;
                    coin.transform.position = new Vector3(transform.position.x, transform.position.y, -3);

                    dead = true;
                    break;
                }
            }
            if (dead)
                Destroy(gameObject);
            else
                hits.Clear();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bullet")
        {
            hits.Add(col.gameObject);
            hit = true;
        }
    }

    float t;
    bool rev = false;
    SpriteRenderer spr;
    void Flashred()
    {
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        t += Time.deltaTime;
        if (rev == false)
            spr.color = new Color(spr.color.r, Mathf.Lerp(1, 0, t / .1f), Mathf.Lerp(1, 0, t / .1f));
        else if (rev == true)
            spr.color = new Color(spr.color.r, Mathf.Lerp(0, 1, t / .1f), Mathf.Lerp(0, 1, t / .1f));
        if (spr.color.g == 0)
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
