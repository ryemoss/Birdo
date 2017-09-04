using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csStatsDisplay : MonoBehaviour
{
    public GameObject healthgo;
    public GameObject energygo;
    public GameObject powergo;
    public GameObject fireratego;
    public GameObject reloadgo;
    public GameObject modh, mode, modp, modfr, modr;

    private TextMesh healthtm, energytm, powertm, fireratetm, reloadtm;
    private TextMesh modth, modte, modtp, modtf, modtr;

    private Staff playerstaff;
    private Gem playergem;

    void Start() 
	{
        healthtm = healthgo.GetComponent<TextMesh>();
        energytm = energygo.GetComponent<TextMesh>();
        powertm = powergo.GetComponent<TextMesh>();
        fireratetm = fireratego.GetComponent<TextMesh>();
        reloadtm = reloadgo.GetComponent<TextMesh>();
        modth = modh.GetComponent<TextMesh>();
        modte = mode.GetComponent<TextMesh>();
        modtp = modp.GetComponent<TextMesh>();
        modtf = modfr.GetComponent<TextMesh>();
        modtr = modr.GetComponent<TextMesh>();

        UpdateStats();
    }

	public void UpdateStats()
	{
        playerstaff = PlayerData.staff;
        playergem = PlayerData.gem;
        healthtm.text = PlayerData.health.ToString();
        energytm.text = PlayerData.energy.ToString();
        powertm.text = PlayerData.power.ToString();
        fireratetm.text = PlayerData.firerate.ToString();
        reloadtm.text = PlayerData.reloadspd.ToString();
        ClearCompare();
    }

    public void ClearCompare()
    {
        modth.text = "";
        modte.text = "";
        modtp.text = "";
        modtf.text = "";
        modtr.text = "";
    }

    public void CompareStaff(Equipment equip)
    {
        if (equip is Staff && (Staff)equip != playerstaff)
        {
            Staff staff = (Staff)equip;

            if (staff.power >= playerstaff.power)
            {
                modtp.color = Color.green;
                modtp.text = "+" + (staff.power - playerstaff.power);
            }
            else if (staff.power < playerstaff.power)
            {
                modtp.color = Color.red;
                modtp.text = "-" + (playerstaff.power - staff.power);
            }

            if (staff.firerate >= playerstaff.firerate)
            {
                modtf.color = Color.green;
                modtf.text = "+" + (staff.firerate - playerstaff.firerate).ToString("0.0");
            }
            else if (staff.firerate < playerstaff.firerate)
            {
                modtf.color = Color.red;
                modtf.text = "-" + (playerstaff.firerate - staff.firerate).ToString("0.0");
            }

            if (staff.reloadspeed >= playerstaff.reloadspeed)
            {
                modtr.color = Color.green;
                modtr.text = "+" + (staff.reloadspeed - playerstaff.reloadspeed).ToString("0.0");
            }
            else if (staff.reloadspeed < playerstaff.reloadspeed)
            {
                modtr.color = Color.red;
                modtr.text = "-" + (playerstaff.reloadspeed - staff.reloadspeed).ToString("0.0");
            }
        }
        else if (equip is Gem && (Gem)equip != playergem)
        {
            Gem gem = (Gem)equip;

            if (gem.health >= playergem.health)
            {
                modth.color = Color.green;
                modth.text = "+" + (gem.health - playergem.health);
            }
            else if (gem.health < playerstaff.power)
            {
                modth.color = Color.red;
                modth.text = "-" + (playergem.health - gem.health);
            }

            if (gem.energy >= playergem.energy)
            {
                modte.color = Color.green;
                modte.text = "+" + (gem.energy - playergem.energy).ToString("0");
            }
            else if (gem.energy < playergem.energy)
            {
                modte.color = Color.red;
                modte.text = "-" + (playergem.energy - gem.energy).ToString("0");
            }
        }
        else
            ClearCompare();
    }
}