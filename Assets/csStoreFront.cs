using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class csStoreFront: MonoBehaviour
{
    public GameObject blockgo;
    private GameObject block;
    public GameObject staffcon, gemcon;
    public string type;

    private int yind = 0;

    void Start()
    {
        foreach (KeyValuePair<int, Staff> wep in csWeaponDatabase.staffDB)
        {
            block = Instantiate(blockgo, staffcon.transform) as GameObject;
            block.transform.localPosition = new Vector2(0, -yind * 3);
            StartCoroutine(block.GetComponent<csEquipBlock>().setattributes(wep.Key, "staff"));
            yind++;
        }
        yind = 0;
        foreach (KeyValuePair<int, Gem> gem in csGemDatabase.gemDB)
        {
            if (gem.Key >= 100)
            {   
                block = Instantiate(blockgo, gemcon.transform) as GameObject;
                block.transform.localPosition = new Vector2(0, -yind * 3);
                StartCoroutine(block.GetComponent<csEquipBlock>().setattributes(gem.Key, "gem"));
                yind++;
            }
        }
    }

    public void displaytype(string type)
    {
        if (type == "staff")
        {
            staffcon.SetActive(true);
            gemcon.SetActive(false);
        }
        else if (type == "gem")
        {
            staffcon.SetActive(false);
            gemcon.SetActive(true);
        }
    }
}