using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csMegabird : MonoBehaviour
{
    public GameObject deathparticles;
    public GameObject coinpf;
    public Bird birdtype;

    private GameObject coin;
    private float health;
    private float playerpower;
    public int spawnside;
    private float spawnposy;
    private bool flash = false;

    public GameObject bomb1pf;
    private GameObject bomb;

    private float speed;
    private Rigidbody2D rigidbod;
    private float t = 0;

    void Start()
    {
        birdtype = csBirdDatabase.birdDB["Megabird"];
        health = birdtype.health;
        speed = birdtype.speed;
        playerpower = PlayerData.power;

        rigidbod = gameObject.GetComponent<Rigidbody2D>();
        rigidbod.velocity = new Vector2(-spawnside * speed, 0);

        transform.position = new Vector2(0, 5f);
    }

    void Update()
    {
        t += Time.deltaTime;
        if (t >= 4f)
        {
            dropbomb();
        }
        if (flash == true)
            Flashred();
    }

    void dropbomb()
    {
        t = 0;
        bomb = Instantiate(bomb1pf) as GameObject;
        bomb.transform.position = new Vector3(transform.position.x + Random.Range(-3f,3f), transform.position.y, 1);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "bullet")
        {
            flash = true;
            health -= playerpower;
            Destroy(col.gameObject);

            if (health <= 0)
            {
                PlayerData.roundbirds++;
                GameObject.Find("LevelManager").GetComponent<csLevelManager>().RemoveBird(birdtype);

                deathparticles.SetActive(true);
                deathparticles.transform.parent = transform.parent;
                deathparticles.transform.position = new Vector3(transform.position.x, transform.position.y, 1);

                coin = Instantiate(coinpf) as GameObject;
                coin.transform.position = new Vector3(transform.position.x, transform.position.y, -3);

                Destroy(col.gameObject);
                Destroy(gameObject);
            }
        }
    }

    float rt;
    bool rev = false;
    SpriteRenderer spr;
    void Flashred()
    {
        spr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        rt += Time.deltaTime;
        if (rev == false)
            spr.color = new Color(spr.color.r, Mathf.Lerp(1, 0, rt / .1f), Mathf.Lerp(1, 0, rt / .1f));
        else if (rev == true)
            spr.color = new Color(spr.color.r, Mathf.Lerp(0, 1, rt / .1f), Mathf.Lerp(0, 1, rt / .1f));
        if (spr.color.g == 0)
        {
            rev = true;
            rt = 0;
        }
        else if (spr.color.g == 1 && rev == true)
        {
            rev = false;
            flash = false;
            rt = 0;
        }
    }
}