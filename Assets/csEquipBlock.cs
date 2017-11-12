using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class csEquipBlock : MonoBehaviour
{
    public string itemtype;
    public GameObject icon;
    public GameObject bg;
    public GameObject itemname;
    public GameObject cost;
    public int itemnum;
    private Equipment item;
    private GameObject playerstats;
    private csStatsDisplay statsdisplay;

    void Start()
    {
        playerstats = GameObject.Find("Stats");
        statsdisplay = playerstats.GetComponent<csStatsDisplay>();
    }

    public IEnumerator setattributes(int dictnum, string type)
    {
        yield return new WaitForSeconds(.02f);
        if (type == "staff")
            item = csWeaponDatabase.staffDB[dictnum];
        else if (type == "gem")
            item = csGemDatabase.gemDB[dictnum];
        itemname.GetComponent<TextMesh>().text = item.name;
        cost.GetComponent<TextMesh>().text = item.cost.ToString();
        icon.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    public void PurchaseItem(string type)
    {
        if (type == "staff")
        {
            Staff tempi = (Staff)item;
            GameObject.Find("UpgradeStaff").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = item.sprite;
            PlayerData.staff = tempi;
            PlayerData.bullet = tempi.bullet;
            csWeaponDatabase.staffDB[item.id].ownership = 2;
            csWeaponDatabase.staffDB[item.id].cost = 0;
        }
        else if (type == "gem")
        {
            Gem temp = (Gem)item;
            GameObject.Find("UpgradeGem").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = item.sprite;
            PlayerData.gem = temp;
            csGemDatabase.gemDB[item.id].ownership = 2;
            csGemDatabase.gemDB[item.id].cost = 0;
        }
        PlayerData.UpdateStoredStats();
        PlayerData.totalcoins -= Int32.Parse(cost.GetComponent<TextMesh>().text);
        PlayerPrefs.SetInt("totalcoins", PlayerData.totalcoins);
        playerstats.GetComponent<csStatsDisplay>().UpdateStats();

        cost.GetComponent<TextMesh>().text = "0";
    }

    public void OnMouseDown()
    {
        statsdisplay.CompareStaff(item);
    }

}
