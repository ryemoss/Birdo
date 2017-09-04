using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class csWeaponDatabase : MonoBehaviour
{
    public static Dictionary<int, Staff> staffDB = new Dictionary<int, Staff>();

    void Start()
    {
        /////////// HAFT DATABASE BEGINS ///////////
        staffDB.Add(100, new Staff
        {
            name = "Iron Rod",
            id = 100,
            next = 200,
            ownership = 2,
            power = 1f,
            firerate = 1.0f,
            reloadspeed = 1f,
            energy = 1/10f + .001f,
            type = "normal",
            descrip = "A basic rod made from iron.",
            cost = 0,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_iron"),

        });
        staffDB.Add(200, new Staff
        {
            name = "Iron Rod+",
            id = 200,
            ownership = 0,
            power = 2f,
            firerate = 1.1f,
            reloadspeed = 1.1f,
            energy = 1 / 12f + .001f,
            type = "normal",
            descrip = "A more refined rod made from iron.",
            cost = 50,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_iron"),

        });
        staffDB.Add(101, new Staff
        {
            name = "Wooden Stick",
            id = 101,
            next = 201,
            ownership = 1,
            descrip = "Just a wooden stick.",
            power = 2f,
            firerate = .8f,
            reloadspeed = 1.2f,
            energy = 1/8f + .001f,
            type = "normal",
            cost = 20,
            sprite = Resources.Load<Sprite>("Weapons/staff_wood1"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone"),
        });
        staffDB.Add(201, new Staff
        {
            name = "Wooden Branch",
            id = 201,
            ownership = 0,
            descrip = "Just a wooden branch.",
            power = 3f,
            firerate = .8f,
            reloadspeed = 1.2f,
            energy = 1 / 8f + .001f,
            type = "normal",
            cost = 60,
            sprite = Resources.Load<Sprite>("Weapons/staff_wood2"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone"),
        });
        staffDB.Add(301, new Staff
        {
            name = "",
            id = 301,
            ownership = 0,
            descrip = "Looks like a formidable tree limb.",
            power = 4f,
            firerate = .75f,
            reloadspeed = 1.3f,
            energy = 1 / 8f + .001f,
            type = "normal",
            cost = 150,
            sprite = Resources.Load<Sprite>("Weapons/staff_wood3"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone"),
        });
        staffDB.Add(102, new Staff
        {
            name = "Steel Rod",
            id = 102,
            ownership = 1,
            power = 3f,
            firerate = 0.9f,
            reloadspeed = 0.9f,
            energy = 1/8f + .001f,
            type = "sniper",
            descrip = "A precisely crafted rod.",
            cost = 50,
            sprite = Resources.Load<Sprite>("Weapons/staff_steel"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_musket"),
        });
        staffDB.Add(103, new Staff
        {
            name = "Spindled Staff",
            id = 103,
            ownership = 0,
            power = 1f,
            firerate = 3f,
            reloadspeed = 1f,
            energy = 1/15f + .001f,
            type = "normal",
            descrip = "A tightly wrapped staff boasting speed",
            cost = 100,
            sprite = Resources.Load<Sprite>("Weapons/staff_spindled"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_steel"),
        });
        staffDB.Add(104, new Staff
        {
            name = "Staff of the Frog",
            id = 104,
            ownership = 0,
            power = 1f,
            firerate = .6f,
            reloadspeed = 1.5f,
            energy = 1/5f + .001f,
            type = "shotgun",
            descrip = "Crafted from the throat of a frog",
            cost = 150,
            sprite = Resources.Load<Sprite>("Weapons/staff_frog"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_steel"),
        });
        staffDB.Add(999, new Staff
        {
            name = "Null",
            id = 999,
            ownership = 0,
            power = 99f,
            firerate = 99f,
            reloadspeed = 99f,
            energy = 1/99f + .001f,
            type = "laser",
            descrip = "[][][][]",
            cost = 5000,
            sprite = Resources.Load<Sprite>("Weapons/staff_ultima"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_null"),
        });

        List<Serialobj> so = new List<Serialobj>();
        if (Serializer.Load<List<Serialobj>>("savedata") != null)
            so = Serializer.Load<List<Serialobj>>("savedata");

        foreach (Serialobj item in so)
        {
            staffDB[item.id].cost = item.val;
        }
       // if (Serializer.Load<Dictionary<int, Orb>>("savedata") != null)
          //  orbDB = Serializer.Load<Dictionary<int, Orb>>("savedata"); 
    }
}

public class Equipment
{
    public static Equipment instance = new Equipment();
    public string name;
    public int id;
    public string descrip;
    public int ownership; // 2=own, 1=visible in shop, 0=not visible
    public Sprite sprite;
    public int cost;

    public static Equipment Instance
    {
        get
        {
            return instance;
        }
    }
}

public class Staff : Equipment
{
    public float power;
    public float firerate;
    public float reloadspeed;
    public float energy; // energy cost for bullet
    public string type; // "", shotgun, laser
    public int next; // id of next upgrade
    public GameObject bullet;
}
