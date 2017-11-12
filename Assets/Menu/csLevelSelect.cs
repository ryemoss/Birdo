using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class csLevelSelect : MonoBehaviour
{
    private List<int> levellist;
    public GameObject nodes, nodepf, nodehl, region, nextregion, prevregion;
    public GameObject shopmenu, winPostStats, coins, start, bgstart;
    public Sprite nodecompletesprite;

    private GameObject node;
    public int ind, regionnum = 1, maxregion;
    private RaycastHit2D hit;
    private int ncnt = 0;
    private csAnimations anims;
    private bool fadein;

    void Start()
    {
        anims = gameObject.AddComponent<csAnimations>();
        anims.GetParams(start, "pulse", .4f, .03f);

        if (PlayerData.previousscene == "Scene_Play" && PlayerData.prevlevelwin == true)
        {
            winPostStats.SetActive(true);
            regionnum = PlayerData.currentlevelnum / 1000;
            PlayerData.Roundend();
        }
        PopulateNodes();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
                ClickEvent(hit.collider);
        }
        if (fadein == true)
            FadeInStart();
    }

    void ClickEvent(Collider2D col)
    {
        if (winPostStats.activeInHierarchy == false)
        {
            if (col.name == "start")
                StartLevel();
            else if (col.name == "shop")
            {
                shopmenu.SetActive(true);
                gameObject.SetActive(false);
            }
            else if (col.name == "nextregion")
            {
                regionnum++;
                PopulateNodes();
            }
            else if (col.name == "prevregion")
            {
                regionnum--;
                PopulateNodes();
            }
        }
    }

    private void PopulateNodes()
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (Transform child in nodes.transform)
            temp.Add(child.gameObject);
        foreach (GameObject child in temp)
            DestroyImmediate(child);

        region.GetComponent<TextMesh>().text = "Region " + regionnum;
        coins.GetComponent<TextMesh>().text = PlayerData.totalcoins.ToString();

        ncnt = 0;
        foreach (KeyValuePair<int, Level> lvl in csLevelDatabase.levelDB)
        {
            if (lvl.Value.complete > 0)
            {
                if (lvl.Key / 1000 > maxregion)
                    maxregion = lvl.Key / 1000;

                if (lvl.Key / 1000 == regionnum)
                {
                    node = Instantiate(nodepf, nodes.transform) as GameObject;
                    node.GetComponent<csLevelNode>().levelindex = lvl.Key;
                    node.transform.GetChild(0).GetComponent<TextMesh>().text = (lvl.Key % 1000).ToString();

                    if (lvl.Value.complete == 2)
                        node.GetComponent<SpriteRenderer>().sprite = nodecompletesprite;
                    ncnt++;
                    ind = lvl.Key;
                }
            }
        }

        for (int j = 0; j < ncnt; j++)
        {
            Transform child = nodes.transform.GetChild(j);
            child.position = new Vector2(j * 2f - (ncnt - 1 - j) * 2f, 0); // position nodes evenly
            child.gameObject.name = "node " + j;
            nodehl.transform.position = child.position + Vector3.back * .1f;
        }

        if (regionnum == 1)
            prevregion.SetActive(false);
        else
            prevregion.SetActive(true);

        if (regionnum == maxregion)
            nextregion.SetActive(false);
        else
            nextregion.SetActive(true);
    }

    public void NodeClicked(Collider2D col)
    {
        ind = col.GetComponent<csLevelNode>().levelindex;
        nodehl.transform.position = col.transform.position + Vector3.back*.1f;
    }

    public void StartLevel()
    {
        PlayerData.roundcoins = 0;
        PlayerData.roundbirds = 0;
        PlayerData.roundexp = 0;
        PlayerData.roundaccuracy = 00.0f;
        PlayerData.roundshots = 0;

        PlayerData.currentlevelnum = ind;
        PlayerData.currentlevel = csLevelDatabase.levelDB[ind];

        fadein = true;
    }

    float t = 0;
    public void FadeInStart()
    {
        t += Time.deltaTime;

        bgstart.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t / .5f));
        if (t > .5f)
        {
            t = 0;
            fadein = false;
            SceneManager.LoadScene("Scene_Play");
        }
    }
}