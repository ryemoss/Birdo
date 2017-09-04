using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class csAchieveDatabase : MonoBehaviour
{
    public static Dictionary<int, Achievement> achieveDB = new Dictionary<int, Achievement>();

    void Start()
    {
        achieveDB.Add(0, new Achievement
        {
            name = "Bird Conquerer I",
            description = "Eliminate 100 birds",
            reward = 10,
            complete = 1
        });
        achieveDB.Add(1, new Achievement
        {
            name = "Bird Conquerer II",
            description = "Eliminate 500 birds",
            reward = 20,
            complete = 1
        });
        achieveDB.Add(2, new Achievement
        {
            name = "Bird Conquerer III",
            description = "Eliminate 1000 birds",
            reward = 30,
            complete = 1
        });
        achieveDB.Add(3, new Achievement
        {
            name = "Bird Conquerer IV",
            description = "Eliminate 5000 birds",
            reward = 40,
            complete = 1
        });
        achieveDB.Add(4, new Achievement
        {
            name = "Bird Conquerer V",
            description = "Eliminate 9999 birds",
            reward = 50,
            complete = 1
        });

        /*
        List<Serialobj> so = new List<Serialobj>();
        if (Serializer.Load<List<Serialobj>>("gemdata") != null)
            so = Serializer.Load<List<Serialobj>>("gemdata");

        foreach (Serialobj item in so)
        {
            gemDB[item.id].cost = item.val;
        }
        */
    }
}

public class Achievement
{
    public static Achievement instance = new Achievement();

    public string name;
    public string description;
    public int reward;
    public int complete; //0 == hidden, 1==no, 2==yes

    public static Achievement Temp
    {
        get
        {
            return instance;
        }
    }
}
