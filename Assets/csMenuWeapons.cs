using System;
using System.Collections.Generic;
using UnityEngine;

public class csMenuWeapons : MonoBehaviour
{
    public GameObject slots, oldwep, newwep, slothightlight;
    public Sprite unknown;
    public GameObject coins;

    private List<GameObject> wepslots = new List<GameObject>();
    private List<Staff> weps = new List<Staff>();
    private Staff thisstaff;
    private Staff nextstaff;
    private GameObject currentgo;

    public GameObject oldpg, oldfg, oldrg, oldptextg, oldftextg, oldrtextg, OldText;
    public GameObject oldnameg, olddescripg, wepname, descrip;
    public GameObject modp, modfr, modr;
    public GameObject newpg, newfg, newrg, newptextg, newftextg, newrtextg, cost;
    public GameObject equipsymbol;

    private TextMesh oldname, olddescrip;
    private TextMesh oldp, oldf, oldr;
    private TextMesh modtp, modtf, modtr;
    private TextMesh newname, newdescrip;
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

        oldname = oldnameg.GetComponent<TextMesh>();
        oldp = oldpg.GetComponent<TextMesh>();
        oldf = oldfg.GetComponent<TextMesh>();
        oldr = oldrg.GetComponent<TextMesh>();
        olddescrip = olddescripg.GetComponent<TextMesh>();

        newname = wepname.GetComponent<TextMesh>();
        newp = newpg.GetComponent<TextMesh>();
        newf = newfg.GetComponent<TextMesh>();
        newr = newrg.GetComponent<TextMesh>();
        newdescrip = descrip.GetComponent<TextMesh>();

        modtp = modp.GetComponent<TextMesh>();
        modtf = modfr.GetComponent<TextMesh>();
        modtr = modr.GetComponent<TextMesh>();

        Initialize();
    }

    void Initialize()
    {
        cost.GetComponent<TextMesh>().text = "";
        ClearNewStats();
        ClearOldStats();
        oldwep.GetComponent<SpriteRenderer>().sprite = null;
        newwep.GetComponent<SpriteRenderer>().sprite = null;
        transform.Find("to").gameObject.SetActive(false);
        thisstaff = PlayerData.staff;
        equipsymbol.transform.parent = wepslots[weps.IndexOf(PlayerData.staff)].transform;
        equipsymbol.transform.localPosition = new Vector3(-.68f, equipsymbol.transform.localPosition.y, equipsymbol.transform.localPosition.z);
        slothightlight.SetActive(false);
    }

    void ShowText()
    {
        transform.Find("to").gameObject.SetActive(true);
        newptextg.GetComponent<TextMesh>().text = "POWER:";
        newftextg.GetComponent<TextMesh>().text = "FIRERATE:";
        newrtextg.GetComponent<TextMesh>().text = "RELOAD:";
        OldText.SetActive(true);
        slothightlight.SetActive(true);

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
        slothightlight.transform.position = new Vector3(go.transform.position.x, go.transform.position.y, -2);

        if (go.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == unknown)
        {
            thisstaff = null;
            nextstaff = weps[wepslots.IndexOf(go)];
            oldwep.GetComponent<SpriteRenderer>().sprite = unknown;
            newwep.GetComponent<SpriteRenderer>().sprite = nextstaff.sprite;
            CompareStats(null, nextstaff);
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
                ClearNewStats();
                ClearOldStats();
                ShowOldStats(thisstaff);
            }
        }
    }

    private void ShowOldStats(Staff old)
    {
        oldname.text = old.name;
        olddescrip.text = WrapText(old.descrip, 20);
        oldp.text = old.power.ToString();
        oldf.text = old.firerate.ToString();
        oldr.text = old.reloadspeed.ToString();
        OldText.SetActive(true);
    }

    private void CompareStats(Staff old, Staff upg)
    {
        if (old != null)
        {
            oldname.text = old.name;
            olddescrip.text = WrapText(old.descrip, 20);
            oldp.text = old.power.ToString();
            oldf.text = old.firerate.ToString();
            oldr.text = old.reloadspeed.ToString();
        }
        else
            ClearOldStats();

        newname.text = upg.name;
        newdescrip.text = WrapText(upg.descrip, 18);
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

    private void ClearOldStats()
    {
        OldText.SetActive(false);
    }

    private void ClearNewStats()
    {
        cost.GetComponent<TextMesh>().text = "";
        newname.text = "";
        newdescrip.text = "";
        newp.text = "";
        newf.text = "";
        newr.text = "";
        newptextg.GetComponent<TextMesh>().text = "";
        newftextg.GetComponent<TextMesh>().text = "";
        newrtextg.GetComponent<TextMesh>().text = "";
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
        if (thisstaff == null || thisstaff.next != 0)
        {
            Staff stafftobuy = nextstaff;
            if (stafftobuy.cost <= PlayerData.totalcoins)
            {
                //GetComponent<AudioSource>().Play();
                stafftobuy.ownership = 2;
                PlayerData.totalcoins -= stafftobuy.cost;
                PlayerPrefs.SetInt("totalcoins", PlayerData.totalcoins);
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
    }

    private void SaveData()
    {
        for (int x = 0; x < weps.Count; x++)
        {
            PlayerPrefs.SetInt("weps" + x, weps[x].id);
            PlayerPrefs.SetInt("weps.count", x+1);
        }
    }

    private string WrapText(string p, int m)
    {
        int max = m;
        if (p.Length > max)
        {
            string n = "";
            while (n != " ")
            {
                n = p.Substring(max - 1, 1);
                max--;
                if (max <= 0)
                    break;
            }

            string temp = p.Substring(0, max) + "\n";
            p = temp + p.Substring(max+1);
        }
        return p;
    }
}