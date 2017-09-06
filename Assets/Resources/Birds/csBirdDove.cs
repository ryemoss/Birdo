using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csBirdDove : MonoBehaviour
{
    public GameObject bombpf;
    private GameObject bomb;

    private float speed;
    private Rigidbody2D rigidbod;
    private int spawnside;

    private float t, nextt = 1f;

    void Start()
    {
        GetComponent<csBirdBase>().birdtype = csBirdDatabase.birdDB["Dove"];
        GetComponent<csBirdBase>().Initializer();
        spawnside = GetComponent<csBirdBase>().spawnside;
        speed = GetComponent<csBirdBase>().birdtype.speed;

        rigidbod = gameObject.GetComponent<Rigidbody2D>();
        speed = -spawnside * speed;

        InvokeRepeating("dropbomb", 1f, .2f);
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t > nextt)
        {
            t = 0;
            nextt = Random.Range(.2f, .3f);
            rigidbod.velocity = speed * new Vector2(Random.Range(.1f, 2f), Random.Range(-1f, 1f)).normalized;
        }
    }

    void dropbomb()
    {
        if (Random.Range(0, 10) == 0)
        {
            bomb = Instantiate(bombpf) as GameObject;
            bomb.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }
}