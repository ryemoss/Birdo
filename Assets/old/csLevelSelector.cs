using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
/*
public class csLevelSelector: MonoBehaviour
{
    private List<int> levellist;
    public GameObject currlvl;
    public GameObject nextlvl;
    public GameObject prevlvl;
    public GameObject backbutton;
    public GameObject forwardbutton;

    private int ind;
    private RaycastHit2D hit;

    void Start()
    {
        levellist = new List<int>();

        foreach (KeyValuePair<int, Level> lvl in csLevelDatabase.levelDB)
        {
            if (lvl.Value.complete > 0)
            {
                levellist.Add(lvl.Key);
            }
        }

        ind = levellist.Count - 1;
        Carousel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider.tag == "level")
            {
                if (hit.collider.name == "prevlvl")
                {
                    if (ind > 0)
                        ind--;
                }
                else if (hit.collider.name == "nextlvl")
                {
                    if (ind < levellist.Count - 1)
                        ind++;
                }
                Carousel();
            }
        }
    }

    void Carousel()
    {
        currlvl.GetComponent<TextMesh>().text = levellist[ind].ToString();

        if (ind > 0)
            prevlvl.GetComponent<TextMesh>().text = levellist[ind - 1].ToString();
        else
            prevlvl.GetComponent<TextMesh>().text = "";
        if (ind < levellist.Count - 1)
            nextlvl.GetComponent<TextMesh>().text = levellist[ind + 1].ToString();
        else
            nextlvl.GetComponent<TextMesh>().text = "";

        PlayerData.currentlevelnum = levellist[ind];
        PlayerData.currentlevel = csLevelDatabase.levelDB[levellist[ind]];
    }

}
*/