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
        ///////// IRON RODS
        staffDB.Add(100, new Staff
        {
            name = "Rod",
            id = 100,
            next = 200,
            ownership = 2,
            power = .8f,
            firerate = .9f,
            reloadspeed = 1f,
            energy = 1 / 10f + .001f,
            type = "normal",
            descrip = "A really simple rod.",
            cost = 0,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron1"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_iron"),

        });
        staffDB.Add(200, new Staff
        {
            name = "Iron Rod",
            id = 200,
            next = 300,
            ownership = 1,
            power = 1f,
            firerate = 1.0f,
            reloadspeed = 1f,
            energy = 1/10f + .001f,
            type = "normal",
            descrip = "A basic rod made from iron.",
            cost = 1,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron1"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_iron"),

        });
        staffDB.Add(300, new Staff
        {
            name = "Iron Rod+",
            id = 300,
            next = 400,
            ownership = 0,
            power = 2f,
            firerate = 1.1f,
            reloadspeed = 1.1f,
            energy = 1 / 10f + .001f,
            type = "normal",
            descrip = "A more refined rod made from iron.",
            cost = 20,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron2"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_iron"),

        });
        staffDB.Add(400, new Staff
        {
            name = "Metal Staff",
            id = 400,
            next = 500,
            ownership = 0,
            power = 3f,
            firerate = 1.2f,
            reloadspeed = 1.2f,
            energy = 1 / 12f + .001f,
            type = "normal",
            descrip = "A resilient metal staff.",
            cost = 60,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron3"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_metal"),
        });
        staffDB.Add(500, new Staff
        {
            name = "Forged Staff",
            id = 500,
            next = 600,
            ownership = 0,
            power = 4f,
            firerate = 1.3f,
            reloadspeed = 1.3f,
            energy = 1 / 12f + .001f,
            type = "normal",
            descrip = "A metal staff of the highest grade.",
            cost = 100,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron4"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_metal"),
        });
        staffDB.Add(600, new Staff
        {
            name = "Meteorite Staff",
            id = 600,
            next = 0,
            ownership = 0,
            power = 5f,
            firerate = 1.5f,
            reloadspeed = 1.5f,
            energy = 1 / 14f + .001f,
            type = "normal",
            descrip = "From metals not of this earth.",
            cost = 200,
            sprite = Resources.Load<Sprite>("Weapons/staff_iron5"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_meteor"),
        });
        //////////// WOOD RODS
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
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone1"),
        });
        staffDB.Add(201, new Staff
        {
            name = "Wooden Branch",
            id = 201,
            next = 301,
            ownership = 0,
            descrip = "Just a wooden branch.",
            power = 3f,
            firerate = .8f,
            reloadspeed = 1.2f,
            energy = 1 / 8f + .001f,
            type = "normal",
            cost = 50,
            sprite = Resources.Load<Sprite>("Weapons/staff_wood2"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone2"),
        });
        staffDB.Add(301, new Staff
        {
            name = "Tree Bough",
            id = 301,
            next = 401,
            ownership = 0,
            descrip = "Looks like a formidable tree limb.",
            power = 4f,
            firerate = .75f,
            reloadspeed = 1.3f,
            energy = 1 / 8f + .001f,
            type = "normal",
            cost = 120,
            sprite = Resources.Load<Sprite>("Weapons/staff_wood3"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone3"),
        });
        staffDB.Add(401, new Staff
        {
            name = "Nature's Bough",
            id = 301,
            next = 501,
            ownership = 0,
            descrip = "A mighty display of nature's power.",
            power = 5f,
            firerate = .75f,
            reloadspeed = 1.3f,
            energy = 1 / 8f + .001f,
            type = "normal",
            cost = 250,
            sprite = Resources.Load<Sprite>("Weapons/staff_wood4"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone3"),
        });
        staffDB.Add(501, new Staff
        {
            name = "Gaia's Arm",
            id = 501,
            ownership = 0,
            descrip = "An appendage of Mother Earth herself.",
            power = 6f,
            firerate = .75f,
            reloadspeed = 1.3f,
            energy = 1 / 8f + .001f,
            type = "normal",
            cost = 400,
            sprite = Resources.Load<Sprite>("Weapons/staff_wood5"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_stone3"),
        });
        ////////// STEEL RODS
        staffDB.Add(102, new Staff
        {
            name = "Steel Rod",
            id = 102,
            next = 202,
            ownership = 1,
            power = 3f,
            firerate = 0.9f,
            reloadspeed = 0.9f,
            energy = 1/8f + .001f,
            type = "sniper",
            descrip = "A precisely crafted rod.",
            cost = 30,
            sprite = Resources.Load<Sprite>("Weapons/staff_steel1"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_musket1"),
        });
        staffDB.Add(202, new Staff
        {
            name = "Hardened Steel Rod",
            id = 202,
            next = 302,
            ownership = 0,
            power = 3f,
            firerate = 0.9f,
            reloadspeed = 0.9f,
            energy = 1 / 8f + .001f,
            type = "sniper",
            descrip = "A precisely crafted rod.",
            cost = 60,
            sprite = Resources.Load<Sprite>("Weapons/staff_steel2"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_musket2"),
        });
        staffDB.Add(302, new Staff
        {
            name = "Machined Steel Staff",
            id = 302,
            next = 402,
            ownership = 0,
            power = 3f,
            firerate = 0.9f,
            reloadspeed = 0.9f,
            energy = 1 / 8f + .001f,
            type = "sniper",
            descrip = "A precisely crafted rod.",
            cost = 160,
            sprite = Resources.Load<Sprite>("Weapons/staff_steel3"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_musket3"),
        });
        staffDB.Add(402, new Staff
        {
            name = "Machined Steel Staff+",
            id = 402,
            next = 502,
            ownership = 0,
            power = 4f,
            firerate = 0.9f,
            reloadspeed = 0.9f,
            energy = 1 / 8f + .001f,
            type = "sniper",
            descrip = "A precisely crafted rod.",
            cost = 280,
            sprite = Resources.Load<Sprite>("Weapons/staff_steel4"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_musket3"),
        });
        staffDB.Add(502, new Staff
        {
            name = "Vector Staff",
            id = 502,
            ownership = 0,
            power = 4f,
            firerate = 0.9f,
            reloadspeed = 0.9f,
            energy = 1 / 8f + .001f,
            type = "sniper",
            descrip = "A precisely crafted rod.",
            cost = 500,
            sprite = Resources.Load<Sprite>("Weapons/staff_steel5"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_musket3"),
        });
        //////// QUICK RODS
        staffDB.Add(103, new Staff
        {
            name = "Spindled Staff",
            id = 103,
            ownership = 1,
            power = 1f,
            firerate = 3f,
            reloadspeed = 1f,
            energy = 1/15f + .001f,
            type = "normal",
            descrip = "A tightly wrapped staff boasting speed",
            cost = 60,
            sprite = Resources.Load<Sprite>("Weapons/staff_spindled"),
            bullet = Resources.Load<GameObject>("Bullets/bullet_steel"),
        });
        ///////// ANIMAL RODS
        staffDB.Add(104, new Staff
        {
            name = "Staff of the Frog",
            id = 104,
            ownership = 1,
            power = 1f,
            firerate = .6f,
            reloadspeed = 1.5f,
            energy = 1/5f + .001f,
            type = "shotgun",
            descrip = "Crafted from the throat of a frog",
            cost = 100,
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

        foreach (KeyValuePair<int, Staff> pair in staffDB)
        {
            pair.Value.ownership = PlayerPrefs.GetInt("staff" + pair.Key + "ownership");
        }
        staffDB[100].ownership = 2;
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
