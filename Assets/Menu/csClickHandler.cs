using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class csClickHandler : MonoBehaviour
{
    public GameObject achievementsgo, levelselectgo, settingsgo, coinsgo, shopgo;
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
                if (hit.collider.gameObject.name == "UpgradeStaff" || hit.collider.gameObject.name == "weaponsback")
                    toggleweapons();
                if (hit.collider.tag == "achievement")
                    achievements.clickAchievementNode(hit.collider.gameObject);
                if (hit.collider.tag == "wepslot")
                    wepmenu.GetComponent<csMenuWeapons>().slotSelected(hit.collider.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightBracket))
            PlayerData.totalcoins += 1000;
    }

    void toggleachievements()
    {
        if (levelselectgo.activeInHierarchy)
            lastwindow = levelselectgo;
        else if (shopgo.activeInHierarchy)
            lastwindow = shopgo;

        lastwindow.SetActive(!lastwindow.activeInHierarchy);
        coinsgo.SetActive(!coinsgo.activeInHierarchy);
        settingsgo.SetActive(!settingsgo.activeInHierarchy);
    }

    void toggleweapons()
    {
        wepmenu.SetActive(!wepmenu.activeInHierarchy);
        shopgo.SetActive(!shopgo.activeInHierarchy);
        settingsgo.SetActive(!settingsgo.activeInHierarchy);
        achievementsgo.SetActive(!achievementsgo.activeInHierarchy);
    }

}