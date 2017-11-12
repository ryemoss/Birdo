using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csClickHandler : MonoBehaviour
{
    public GameObject achievementsgo, levelselectgo, settingsgo, coinsgo, charactermenu;
    public GameObject wepmenu, menu0, menu1, menu2;

    private csAchievements achievements;
    private RaycastHit2D hit;
    private GameObject lastwindow;

    void Start() 
	{
        PlayerData.saveable = true;
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
        else if (wepmenu.activeInHierarchy)
            lastwindow = wepmenu;

        menu0.SetActive(!menu0.activeInHierarchy);
        menu1.SetActive(!menu1.activeInHierarchy);
        menu2.SetActive(!menu2.activeInHierarchy);
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
            selecttab(menu1);
        }
    }

    void toggleweaponsmenu()
    {
        if (!wepmenu.activeInHierarchy)
        {
            wepmenu.SetActive(true);
            levelselectgo.SetActive(false);
            charactermenu.SetActive(false);
            selecttab(menu2);
        }
    }

    void togglelevelmenu()
    {
        if (!levelselectgo.activeInHierarchy)
        {
            levelselectgo.SetActive(true);
            wepmenu.SetActive(false);
            charactermenu.SetActive(false);
            selecttab(menu0);
        }
    }

    void selecttab(GameObject tab)
    {
        menu0.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        menu1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        menu2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        tab.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, .2f);
    }

}