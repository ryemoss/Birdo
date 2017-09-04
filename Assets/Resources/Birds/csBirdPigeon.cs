using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csBirdPigeon : MonoBehaviour
{
    public GameObject coinpf;
    public GameObject bombpf;

    private GameObject bomb;
    private GameObject coin;

    private float speed;
    private Rigidbody2D rigidbod;
    private int spawnside;

    void Start()
    {
        GetComponent<csBirdBase>().birdtype = csBirdDatabase.birdDB["Pigeon"];
        GetComponent<csBirdBase>().Initializer();
        spawnside = GetComponent<csBirdBase>().spawnside;
        speed = GetComponent<csBirdBase>().birdtype.speed;

        rigidbod = gameObject.GetComponent<Rigidbody2D>();
        rigidbod.velocity = new Vector2(-spawnside * speed, 0);

        InvokeRepeating("dropbomb", 1f, .2f);
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