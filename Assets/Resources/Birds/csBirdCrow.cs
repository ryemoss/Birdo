using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csBirdCrow : MonoBehaviour
{
    public GameObject bombpf;
    private GameObject bomb;

    private float speed;
    private Rigidbody2D rigidbod;
    private int spawnside;

    void Start()
    {
        GetComponent<csBirdBase>().birdtype = csBirdDatabase.birdDB["Crow"];
        GetComponent<csBirdBase>().Initializer();
        spawnside = GetComponent<csBirdBase>().spawnside;
        speed = GetComponent<csBirdBase>().birdtype.speed;

        rigidbod = gameObject.GetComponent<Rigidbody2D>();
        rigidbod.velocity = new Vector2(-spawnside * speed, 0);

        InvokeRepeating("dropbomb", 1f, .7f);
    }

    void dropbomb()
    {
        if (Random.Range(0, 2) == 0)
        {
            bomb = Instantiate(bombpf) as GameObject;
            bomb.transform.position = new Vector3(transform.position.x, transform.position.y, 1);
        }
    }
}