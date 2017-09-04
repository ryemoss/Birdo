using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class csLevelDatabase : MonoBehaviour
{

    public static Dictionary<int, Level> levelDB = new Dictionary<int, Level>();

    void Start()
    {
        /*levelDB.Add(1000, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 3 } }),
            spawnrate = 1,
            loc = -1,
            next = 1001,
            complete = 0
        });*/
        levelDB.Add(1001, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 3 } }),
            spawnrate = 1.2f,
            loc = -1,
            next = 1002,
            complete = 1
        });
        levelDB.Add(1002, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 6 } }),
            spawnrate = 1.5f,
            loc = -1,
            next = 1003,
            complete = 0
        });
        levelDB.Add(1003, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 10 } }),
            spawnrate = 1.5f,
            loc = -1,
            next = 1004,
            complete = 0
        });
        levelDB.Add(1004, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 15 } }),
            spawnrate = 1.5f,
            loc = -1,
            next = 1005,
            complete = 0
        });
        levelDB.Add(1005, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 12 } }),
            spawnrate = 1.8f,
            loc = 0,
            next = 1006,
            complete = 0
        });
        levelDB.Add(1006, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 10 }, { "Goose", 1 } }),
            spawnrate = 1.8f,
            loc = -1,
            next = 1007,
            complete = 0
        });
        levelDB.Add(1007, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 10 }, { "Goose", 3 } }),
            spawnrate = 1.8f,
            loc = -1,
            next = 1008,
            complete = 0
        });
        levelDB.Add(1008, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 10 }, { "Goose", 6 } }),
            spawnrate = 2f,
            loc = -1,
            next = 1009,
            complete = 0
        });
        levelDB.Add(1009, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 5 }, { "Goose", 9 } }),
            spawnrate = 2f,
            loc = -1,
            next = 1010,
            complete = 0
        });
        levelDB.Add(1010, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Megabird", 1 } }),
            spawnrate = 1f,
            loc = 0,
            next = 2001,
            complete = 0
        });
        levelDB.Add(2001, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 22 } }),
            spawnrate = 4f,
            loc = -1,
            next = 2002,
            complete = 0
        });
        levelDB.Add(2002, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 5 }, { "Goose", 5 }, { "Hummingbird", 2 } }),
            spawnrate = 3f,
            loc = -1,
            next = 2003,
            complete = 0
        });
        levelDB.Add(2003, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Pigeon", 5 }, { "Goose", 5 }, { "Hummingbird", 5 } }),
            spawnrate = 3f,
            loc = -1,
            next = 2004,
            complete = 0
        });
        levelDB.Add(2004, new Level
        {
            birds = addbirds(new Dictionary<string, int> { { "Hummingbird", 10 } }),
            spawnrate = 3f,
            loc = -1,
            next = 2005,
            complete = 0
        });

        List<Serialobj> so = new List<Serialobj>();
        if (Serializer.Load<List<Serialobj>>("leveldata") != null)
            so = Serializer.Load<List<Serialobj>>("leveldata");

        foreach (Serialobj item in so)
        {
            levelDB[item.id].complete = item.val;
        }
    }

    private Dictionary<Bird, int> addbirds(Dictionary<string, int> pairs)
    {
        Dictionary<Bird, int> templist = new Dictionary<Bird, int>();
        foreach (KeyValuePair<string,int> pair in pairs)
        {
            templist.Add(csBirdDatabase.birdDB[pair.Key], pair.Value);
        }
        return templist;
    }
}

public class Level
{
    public static Level instance = new Level();

    public int index;
    public int complete;
    public int loc;
    public float spawnrate;
    public int maxvis;
    public int next;
    public Dictionary<Bird, int> birds;

    public static Level Instance
    {
        get
        {
            return instance;
        }
    }
}


