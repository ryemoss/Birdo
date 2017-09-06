using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csClickHandler : MonoBehaviour
{
    public GameObject achievementsgo, levelselectgo, settingsgo, coinsgo, charactermenu;
    public GameObject wepmenu;

    private csAchievements achievements;
    private RaycastHit2D hit;
    private GameObject lastwindow;

    void Start() 
	{
        achievements = achievementsgo.GetComponent<csAchievements>();
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                if (hit.collider.gameObject.name == "achievementsbutton")
                {
                    if (achievements.click_achievementsbutton())
                        toggleachievements();
                }
                if (hit.collider.gameObject.name == "winselection0")
                    togglelevelmenu();
                else if(hit.collider.gameObject.name == "winselection1")
                    togglecharmenu();
                else if (hit.collider.gameObject.name == "UpgradeStaff" || hit.collider.gameObject.name == "winselection2")
                    toggleweaponsmenu();
                else if(hit.collider.tag == "achievement")
                    achievements.clickAchievementNode(hit.collider.gameObject);
                else if(hit.collider.tag == "wepslot")
                    wepmenu.GetComponent<csMenuWeapons>().slotSelected(hit.collider.gameObject);
                else if(hit.collider.gameObject.name == "to")
                    wepmenu.GetComponent<csMenuWeapons>().PurchaseItem();
            }
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
            PlayerData.totalcoins += 1000;
    }

    void toggleachievements()
    {
        if (levelselectgo.activeInHierarchy)
            lastwindow = levelselectgo;
        else if (charactermenu.activeInHierarchy)
            lastwindow = charactermenu;

        lastwindow.SetActive(!lastwindow.activeInHierarchy);
        coinsgo.SetActive(!coinsgo.activeInHierarchy);
        settingsgo.SetActive(!settingsgo.activeInHierarchy);
    }

    void togglecharmenu()
    {
        if (!charactermenu.activeInHierarchy)
        {
            levelselectgo.SetActive(false);
            charactermenu.SetActive(true);
            wepmenu.SetActive(false);
            settingsgo.SetActive(true);
            achievementsgo.SetActive(true);
        }
    }

    void toggleweaponsmenu()
    {
        if (!wepmenu.activeInHierarchy)
        {
            wepmenu.SetActive(true);
            levelselectgo.SetActive(false);
            charactermenu.SetActive(false);
            settingsgo.SetActive(false);
            achievementsgo.SetActive(false);
        }
    }

    void togglelevelmenu()
    {
        if (!levelselectgo.activeInHierarchy)
        {
            levelselectgo.SetActive(true);
            wepmenu.SetActive(false);
            charactermenu.SetActive(false);
            settingsgo.SetActive(true);
            achievementsgo.SetActive(true);
        }
    }

}