using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class csBirdDatabase : MonoBehaviour
{
    public static Dictionary<string, Bird> birdDB = new Dictionary<string, Bird>();
    public GameObject dove;
    public GameObject goose;
    public GameObject humming;
    public GameObject pigeon, crow;
    public GameObject megabird;

    void Start()
    {
        birdDB.Add("Dove", new Bird
        {
            health = 2,
            speed = 2.5f,
            birdgo = dove
        });
        birdDB.Add("Goose", new Bird
        {
            health = 3,
            speed = 2f,
            birdgo = goose
        });
        birdDB.Add("Hummingbird", new Bird
        {
            health = 1,
            speed = 10f,
            birdgo = humming
        });
        birdDB.Add("Pigeon", new Bird
        {
            health = 2,
            speed = 2.5f,
            birdgo = pigeon
        });
        birdDB.Add("Crow", new Bird
        {
            health = 2,
            speed = 2.5f,
            birdgo = crow
        });
        birdDB.Add("Megabird", new Bird
        {
            health = 5,
            speed = 0f,
            birdgo = megabird
        });
    }
}

public class Bird
{
    public static Bird instance = new Bird();

    public int health;
    public float speed;
    public int coin;
    public int bomb;
    public GameObject birdgo;

    public static Bird Temp
    {
        get
        {
            return instance;
        }
    }
}
