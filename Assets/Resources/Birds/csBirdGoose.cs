using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csBirdGoose : MonoBehaviour
{
    private GameObject bombpf;
    private GameObject bomb;
    private GameObject coin;

    private float speed;
    private Rigidbody2D rigidbod;
    private int spawnside;

    void Start()
    {
        GetComponent<csBirdBase>().birdtype = csBirdDatabase.birdDB["Goose"];
        GetComponent<csBirdBase>().Initializer();
        spawnside = GetComponent<csBirdBase>().spawnside;
        speed = GetComponent<csBirdBase>().birdtype.speed;
        bombpf = csBombDatabase.bombDB[910].bombgo;

        rigidbod = gameObject.GetComponent<Rigidbody2D>();
        rigidbod.velocity = new Vector2(-spawnside * speed, 0);

        InvokeRepeating("dropbomb", 0f, .4f);
    }

    void dropbomb()
    {
        if (Random.Range(0, 3) == 0)
        {
            bomb = Instantiate(bombpf) as GameObject;
            bomb.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }
}