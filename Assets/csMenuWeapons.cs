using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csMenuWeapons : MonoBehaviour
{
    public GameObject slots, oldwep, newwep;
    public Sprite unknown;

    private List<GameObject> wepslots = new List<GameObject>();
    private List<Staff> weps = new List<Staff>();

    public GameObject powergo;
    public GameObject fireratego;
    public GameObject reloadgo;
    public GameObject modp, modfr, modr;
    public GameObject wepname, descrip;
    public GameObject newpg, newfg, newrg, cost;
    public GameObject equipsymbol;

    private TextMesh powertm, fireratetm, reloadtm;
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
        foreach (KeyValuePair<int,Staff> pair in csWeaponDatabase.staffDB)
        {
            if (pair.Key < 200)
                weps.Add(pair.Value);
        }
        powertm = powergo.GetComponent<TextMesh>();
        fireratetm = fireratego.GetComponent<TextMesh>();
        reloadtm = reloadgo.GetComponent<TextMesh>();
        namet = wepname.GetComponent<TextMesh>();
        descript = descrip.GetComponent<TextMesh>();
        modtp = modp.GetComponent<TextMesh>();
        modtf = modfr.GetComponent<TextMesh>();
        modtr = modr.GetComponent<TextMesh>();
        newp = newpg.GetComponent<TextMesh>();
        newf = newfg.GetComponent<TextMesh>();
        newr = newrg.GetComponent<TextMesh>();
        cost.GetComponent<TextMesh>().text = "";
        ClearStats();
        ClearCompare();
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
        Staff temp = weps[wepslots.IndexOf(go)];

        if (go.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == unknown)
        {
            oldwep.GetComponent<SpriteRenderer>().sprite = unknown;
            newwep.GetComponent<SpriteRenderer>().sprite = temp.sprite;
            ShowStats(temp);
            ClearCompare();
            cost.GetComponent<TextMesh>().text = temp.cost.ToString();
        }
        else
        {
            doubleclick(go);
            if (temp.next != 0)
            {
                oldwep.GetComponent<SpriteRenderer>().sprite = temp.sprite;
                newwep.GetComponent<SpriteRenderer>().sprite = csWeaponDatabase.staffDB[temp.next].sprite;
                ShowStats(temp);
                CompareStats(temp, csWeaponDatabase.staffDB[temp.next]);
                cost.GetComponent<TextMesh>().text = csWeaponDatabase.staffDB[temp.next].cost.ToString();
            }
        }
    }

    private void ShowStats(Staff staff)
    {
        namet.text = staff.name;
        descript.text = staff.descrip;
        powertm.text = staff.power.ToString();
        fireratetm.text = staff.firerate.ToString();
        reloadtm.text = staff.reloadspeed.ToString();
    }

    private void CompareStats(Staff old, Staff upg)
    {
        newp.text = upg.power.ToString();
        newf.text = upg.firerate.ToString();
        newr.text = upg.reloadspeed.ToString();

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
        }
    }

    private void ClearStats()
    {
        namet.text = "";
        descript.text = "";
        powertm.text = "";
        fireratetm.text = "";
        reloadtm.text = "";
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
            equipsymbol.transform.parent = go.transform;
            equipsymbol.transform.localPosition = new Vector3(-.68f, equipsymbol.transform.localPosition.y, equipsymbol.transform.localPosition.z);

            Staff temp = weps[wepslots.IndexOf(go)];
            PlayerData.staff = temp;
            PlayerData.bullet = temp.bullet;
            PlayerData.UpdateStoredStats();
        }
    }

    private void PurchaseItem(GameObject go)
    {

    }
}