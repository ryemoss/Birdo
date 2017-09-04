using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;

public class csMenuHandler : MonoBehaviour
{
    public GameObject menulvlsel;
    public GameObject coins;
    private RaycastHit2D hit;
    public GameObject itemblockhl;
    private List<GameObject> menus;
    public GameObject storemenu;
    public GameObject viewstaffgo, viewgemgo, storecurrstaff, storecurrgem;
    public GameObject stats;

    private Collider2D colliderorig;
    private Collider2D col;
    private bool holdbegin;
    private GameObject selitemblock;
    private csStatsDisplay statsdisplay;

    void Start()
    {
        coins.GetComponent<TextMesh>().text = PlayerData.totalcoins.ToString();
        statsdisplay = stats.GetComponent<csStatsDisplay>();
        UpdatePlayerView();

        menus = new List<GameObject>();
        menus.Add(itemblockhl);
        menus.Add(storemenu);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            holdbegin = true;
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                GameObject hitobj = hit.collider.gameObject;
                string hitname = hitobj.name;

                /*if (hitname == "UpgradeStaff")
                {
                    col = hit.collider;
                    hitobj.transform.GetChild(0).GetComponent<AudioSource>().Play();
                    showmenu("staff");
                }*/
                if (hitname == "UpgradeGem")
                {
                    col = hit.collider;
                    hitobj.transform.GetChild(0).GetComponent<AudioSource>().Play();
                    showmenu("gem");//
                }
                else if (hit.collider.tag == "itemblock")
                {
                    hitobj.GetComponent<AudioSource>().Play();
                    colliderorig = hit.collider;
                    itemblockhl.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    itemblockhl.SetActive(true);
                    itemblockhl.transform.position = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, hit.collider.transform.position.z - 1);
                }
                else if (hitname == "backbutton")
                {
                    menulvlsel.SetActive(true);
                    gameObject.SetActive(false);
                }
                else if (hit.collider.tag == "shopbg")
                {
                    hidemenus();
                }
            }
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null && hit.collider == colliderorig && Int64.Parse(hit.collider.transform.GetChild(3).GetComponent<TextMesh>().text) <= PlayerData.totalcoins && holdbegin == true)
                itemblockhl.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, itemblockhl.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a + .015f);
            else
                itemblockhl.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            if (itemblockhl.transform.GetChild(0).GetComponent<SpriteRenderer>().color.a >= .99f)
            {
                PurchaseItem(hit.collider.gameObject);
                itemblockhl.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                holdbegin = false;
            }
        }
        if (holdbegin == true && Input.GetKeyUp(KeyCode.Mouse0))
        {
            itemblockhl.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            holdbegin = false;
        }
    }

    void showmenu(string type)
    {
        hidemenus();
        storemenu.SetActive(true);
        storemenu.GetComponent<csStoreFront>().displaytype(type);
    }

    void hidemenus()
    {
        foreach (GameObject go in menus)
            go.SetActive(false);
        statsdisplay.ClearCompare();
    }

    void PurchaseItem(GameObject block)
    {
        GetComponent<AudioSource>().Play();
        if (col.gameObject.name == "UpgradeStaff")
        {
            block.GetComponent<csEquipBlock>().PurchaseItem("staff");
        }
        else if (col.gameObject.name == "UpgradeGem")
        {
            block.GetComponent<csEquipBlock>().PurchaseItem("gem");
        }
        coins.GetComponent<TextMesh>().text = PlayerData.totalcoins.ToString();
        UpdatePlayerView();
    }

    void OnEnable()
    {
        UpdatePlayerView();
    }

    public void UpdatePlayerView()
    {
        GameObject.Find("UpgradeStaff").transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = PlayerData.staff.sprite;
        viewstaffgo.GetComponent<SpriteRenderer>().sprite = PlayerData.staff.sprite;
        storecurrstaff.GetComponent<SpriteRenderer>().sprite = PlayerData.staff.sprite;
        viewgemgo.GetComponent<SpriteRenderer>().sprite = PlayerData.gem.sprite;
        storecurrgem.GetComponent<SpriteRenderer>().sprite = PlayerData.gem.sprite;
    }
}