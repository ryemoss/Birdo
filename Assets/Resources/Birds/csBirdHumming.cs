using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csBirdHumming : MonoBehaviour
{
    public GameObject coinpf;
    public GameObject bombpf;

    private GameObject bomb;
    private GameObject coin;

    private float speed;
    private Rigidbody2D rigidbod;

    private Vector2 targetpos;
    private Vector2 prevpos;
    private int movecounter = 0;

    void Start()
    {
        GetComponent<csBirdBase>().birdtype = csBirdDatabase.birdDB["Hummingbird"];
        GetComponent<csBirdBase>().Initializer();
        speed = GetComponent<csBirdBase>().birdtype.speed;

        rigidbod = gameObject.GetComponent<Rigidbody2D>();

        InvokeRepeating("dropbomb", 1f, .2f);
        InvokeRepeating("move", 0f, .2f);
        targetpos = transform.position;
    }

    void Update()
    {
        if (transform.position.x < (targetpos.x + .1f) && transform.position.x > (targetpos.x - .1f))
            rigidbod.velocity = Vector2.zero;
    }

    void dropbomb()
    {
        if (Random.Range(0, 10) == 0)
        {
            bomb = Instantiate(bombpf) as GameObject;
            bomb.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }

    void move()
    {
        if (movecounter == 10)
        {
            movecounter = 0;
            targetpos = new Vector2(transform.position.x + Random.Range(2f, 3f)*-GetComponent<csBirdBase>().spawnside, Mathf.Clamp(transform.position.y + Random.Range(-2, 3),-4,8));
            prevpos = transform.position;
            rigidbod.velocity = (targetpos - prevpos).normalized * speed;
        }
        movecounter++;
    }
}