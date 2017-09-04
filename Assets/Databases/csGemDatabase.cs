using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class csGemDatabase : MonoBehaviour
{
    public static Dictionary<int, Gem> gemDB = new Dictionary<int, Gem>();

    void Start()
    {
        gemDB.Add(100, new Gem
        {
            name = "Stone",
            id = 100,
            health = 110,
            energy = 100,
            ownership = 2,
            descrip = "A Stone insert.",
            cost = 50,
            sprite = Resources.Load<Sprite>("Gems/gem_stone"),
        });
        gemDB.Add(101, new Gem
        {
            name = "Opal",
            id = 101,
            health = 120,
            energy = 100,
            ownership = 1,
            descrip = "An Opal gemstone.",
            cost = 100,
            sprite = Resources.Load<Sprite>("Gems/gem_opal"),
        });
        gemDB.Add(102, new Gem
        {
            name = "Garnet",
            id = 102,
            health = 130,
            energy = 110,
            ownership = 0,
            descrip = "A Garnet gemstone.",
            cost = 200,
            sprite = Resources.Load<Sprite>("Gems/gem_garnet"),
        });
        gemDB.Add(109, new Gem
        {
            name = "Diamond",
            id = 109,
            health = 200,
            energy = 200,
            ownership = 0,
            descrip = "A Diamond gemstone. Beautiful to many.",
            cost = 9000,
            sprite = Resources.Load<Sprite>("Gems/gem_diamond"),
        });
        gemDB.Add(110, new Gem
        {
            name = "Machalite Circlet",
            id = 110,
            health = 180,
            energy = 210,
            ownership = 0,
            descrip = "An impressive headpiece of fine craftsmanship.",
            cost = 15000,
            sprite = Resources.Load<Sprite>("Gems/gem_diamond"),
        });


        List<Serialobj> so = new List<Serialobj>();
        if (Serializer.Load<List<Serialobj>>("gemdata") != null)
            so = Serializer.Load<List<Serialobj>>("gemdata");

        foreach (Serialobj item in so)
        {
            gemDB[item.id].cost = item.val;
        }
    }
}

public class Gem : Equipment
{
    public float health;
    public float energy;
}
