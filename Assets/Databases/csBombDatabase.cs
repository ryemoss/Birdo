using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class csBombDatabase : MonoBehaviour
{
    public static Dictionary<int, Bomb> bombDB = new Dictionary<int, Bomb>();

    public GameObject Btiny;
    public GameObject Bsmall;
    public GameObject Bmed;
    public GameObject Blarge;

    void Start()
    {
        bombDB.Add(905, new Bomb
        {
            damage = .05f,
            bombgo = Btiny
    });
        bombDB.Add(910, new Bomb
        {
            damage = .1f,
            bombgo = Bsmall
        });
        bombDB.Add(920, new Bomb
        {
            damage = .2f,
            bombgo = Bmed
        });
        bombDB.Add(930, new Bomb
        {
            damage = .3f,
            bombgo = Blarge
        });
    }
}

public class Bomb
{
    public static Bomb instance = new Bomb();

    public float damage;
    public GameObject bombgo;

    public static Bomb Temp
    {
        get
        {
            return instance;
        }
    }
}
