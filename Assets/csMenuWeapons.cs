using System;
using System.Collections.Generic;
using UnityEngine;

public class csMenuWeapons : MonoBehaviour
{
    public GameObject slots, oldwep, newwep;
    public Sprite unknown;
    public GameObject coins;

    private List<GameObject> wepslots = new List<GameObject>();
    private List<Staff> weps = new List<Staff>();
    private Staff thisstaff;
    private Staff nextstaff;
    private GameObject currentgo;

    public GameObject oldpg, oldfg, oldrg;
    public GameObject modp, modfr, modr;
    public GameObject wepname, descrip;
    public GameObject newpg, newfg, newrg, cost;
    public GameObject equipsymbol;

    private TextMesh oldp, oldf, oldr;
    private TextMesh modtp, modtf, modtr;
    private TextMesh namet, descript;
    private TextMesh newp, newf, newr;

    private bool singleclick;
    private GameObject prevclick;
    private float t = 0;

    void Start()
    {
        foreach (Transform child in slots.transform)
        {
            wepslots.Add(child.gameObject);
        }
        int x = 0;
        if (PlayerPrefs.GetInt("weps.count") == 0)
        {
            foreach (KeyValuePair<int, Staff> pair in csWeaponDatabase.staffDB)
            {
                if (pair.Key < 200)
                {
                    weps.Add(pair.Value);
                    if (pair.Value.ownership == 2)
                        wepslots[x].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = pair.Value.sprite;
                    else
                        wepslots[x].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = unknown;
                    x++;
                }
            }
        }
        else
        {
            for (int y = 0; y < PlayerPrefs.GetInt("weps.count"); y++)
            {
                weps.Add(csWeaponDatabase.staffDB[PlayerPrefs.GetInt("weps" + y)]);
                if (weps[y].ownership == 2)
                    wepslots[y].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = weps[y].sprite;
                else
                    wepslots[y].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = unknown;
            }
        }

        oldp = oldpg.GetComponent<TextMesh>();
        oldf = oldfg.GetComponent<TextMesh>();
        oldr = oldrg.GetComponent<TextMesh>();

        namet = wepname.GetComponent<TextMesh>();
        newp = newpg.GetComponent<TextMesh>();
        newf = newfg.GetComponent<TextMesh>();
        newr = newrg.GetComponent<TextMesh>();
        descript = descrip.GetComponent<TextMesh>();

        modtp = modp.GetComponent<TextMesh>();
        modtf = modfr.GetComponent<TextMesh>();
        modtr = modr.GetComponent<TextMesh>();

        Initialize();
    }

    void Initialize()
    {
        cost.GetComponent<TextMesh>().text = "";
        ClearStats();
        ClearCompare();
        oldwep.GetComponent<SpriteRenderer>().sprite = null;
        newwep.GetComponent<SpriteRenderer>().sprite = null;
        transform.Find("to").gameObject.SetActive(false);
        GameObject.Find("t_power").GetComponent<TextMesh>().text = "";
        GameObject.Find("t_firerate").GetComponent<TextMesh>().text = "";
        GameObject.Find("t_reload").GetComponent<TextMesh>().text = "";
        equipsymbol.transform.parent = wepslots[weps.IndexOf(PlayerData.staff)].transform;
        equipsymbol.transform.localPosition = new Vector3(-.68f, equipsymbol.transform.localPosition.y, equipsymbol.transform.localPosition.z);
    }

    void ShowText()
    {
        transform.Find("to").gameObject.SetActive(true);
        GameObject.Find("t_power").GetComponent<TextMesh>().text = "POWER:";
        GameObject.Find("t_firerate").GetComponent<TextMesh>().text = "FIRERATE:";
        GameObject.Find("t_reload").GetComponent<TextMesh>().text = "RELOAD:";
    }

    void Update()
    {
        if (singleclick)
        {
            t -= Time.deltaTime;
            if (t < 0)
                singleclick = false;
        }
    }

    public void slotSelected(GameObject go)
    {
        ShowText();
        currentgo = go;
        if (go.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == unknown)
        {
            thisstaff = null;
            nextstaff = weps[wepslots.IndexOf(go)];
            oldwep.GetComponent<SpriteRenderer>().sprite = unknown;
            newwep.GetComponent<SpriteRenderer>().sprite = nextstaff.sprite;
            CompareStats(nextstaff, nextstaff);
            cost.GetComponent<TextMesh>().text = nextstaff.cost.ToString();
        }
        else
        {
            thisstaff = weps[wepslots.IndexOf(go)];
            doubleclick(go);
            if (thisstaff.next != 0)
            {
                nextstaff = csWeaponDatabase.staffDB[thisstaff.next];
                oldwep.GetComponent<SpriteRenderer>().sprite = thisstaff.sprite;
                newwep.GetComponent<SpriteRenderer>().sprite = csWeaponDatabase.staffDB[thisstaff.next].sprite;
                CompareStats(thisstaff, nextstaff);
                cost.GetComponent<TextMesh>().text = nextstaff.cost.ToString();
            }
            else
            {
                oldwep.GetComponent<SpriteRenderer>().sprite = thisstaff.sprite;
                newwep.GetComponent<SpriteRenderer>().sprite = unknown;
                ClearCompare();
                ClearStats();
            }
        }
    }

    private void ShowStats(Staff staff)
    {
        namet.text = staff.name;
        descript.text = staff.descrip;
        //oldp.text = staff.power.ToString();
        //oldf.text = staff.firerate.ToString();
        //oldr.text = staff.reloadspeed.ToString();
    }

    private void CompareStats(Staff old, Staff upg)
    {
        namet.text = upg.name;
        descript.text = upg.descrip;
        newp.text = upg.power.ToString();
        newf.text = upg.firerate.ToString();
        newr.text = upg.reloadspeed.ToString();
        /*
        if (upg.power >= old.power)
        {
            modtp.color = Color.green;
            modtp.text = "+" + (upg.power - old.power);
        }
        else if (upg.power < old.power)
        {
            modtp.color = Color.red;
            modtp.text = "-" + (old.power - upg.power);
        }

        if (upg.firerate >= old.firerate)
        {
            modtf.color = Color.green;
            modtf.text = "+" + (upg.firerate - old.firerate).ToString("0.0");
        }
        else if (upg.firerate < old.firerate)
        {
            modtf.color = Color.red;
            modtf.text = "-" + (old.firerate - upg.firerate).ToString("0.0");
        }

        if (upg.reloadspeed >= old.reloadspeed)
        {
            modtr.color = Color.green;
            modtr.text = "+" + (upg.reloadspeed - old.reloadspeed).ToString("0.0");
        }
        else if (upg.reloadspeed < old.reloadspeed)
        {
            modtr.color = Color.red;
            modtr.text = "-" + (old.reloadspeed - upg.reloadspeed).ToString("0.0");
        }*/
    }

    private void ClearStats()
    {
        namet.text = "";
        descript.text = "";
        oldp.text = "";
        oldf.text = "";
        oldr.text = "";
    }

    private void ClearCompare()
    {
        modtp.text = "";
        modtf.text = "";
        modtr.text = "";
        newp.text = "";
        newf.text = "";
        newr.text = "";
    }

    private void doubleclick(GameObject go)
    {
        if (!singleclick)
        {
            singleclick = true;
            t = .6f;
            prevclick = go;
        }
        else if (go == prevclick)
        {
            EquipStaff(go);
            singleclick = false;
        }
    }

    private void EquipStaff(GameObject go)
    {
        Staff temp = weps[wepslots.IndexOf(go)];
        PlayerData.staff = temp;
        PlayerData.bullet = temp.bullet;
        PlayerData.UpdateStoredStats();

        equipsymbol.transform.parent = go.transform;
        equipsymbol.transform.localPosition = new Vector3(-.68f, equipsymbol.transform.localPosition.y, equipsymbol.transform.localPosition.z);
    }

    public void PurchaseItem()
    {
        Staff stafftobuy = nextstaff;
        if (stafftobuy.cost <= PlayerData.totalcoins)
        {
            //GetComponent<AudioSource>().Play();
            stafftobuy.ownership = 2;
            PlayerData.totalcoins -= stafftobuy.cost;
            coins.GetComponent<TextMesh>().text = PlayerData.totalcoins.ToString();
            thisstaff = stafftobuy;
            nextstaff = null;

            wepslots[wepslots.IndexOf(currentgo)].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = stafftobuy.sprite;
            weps[wepslots.IndexOf(currentgo)] = stafftobuy;
            EquipStaff(wepslots[weps.IndexOf(stafftobuy)]);
            slotSelected(wepslots[weps.IndexOf(stafftobuy)]);
            GameObject.Find("particles_minus").GetComponent<ParticleSystem>().Play();
            SaveData();
        }
    }

    private void SaveData()
    {
        for (int x = 0; x < weps.Count; x++)
        {
            PlayerPrefs.SetInt("weps" + x, weps[x].id);
            PlayerPrefs.SetInt("weps.count", x+1);
        }
    }
}