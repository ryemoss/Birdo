using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class csLevelManager: MonoBehaviour
{
    private float spawnrate; //Bird, count
    private int currentlevelnum;
    public int totalbirds = 0;
    public int birdsonscreen = 0;
    private Dictionary<Bird, int> birdoftypeos;
    private Dictionary<Bird, int> birdqtys;

    public GameObject lvlcomplete;
    public GameObject lvlfailed;
    private bool lvlend = false;
    private bool begin = false;

    void Start()
    {
        birdoftypeos = new Dictionary<Bird, int>();
        birdqtys = new Dictionary<Bird, int>();
        currentlevelnum = PlayerData.currentlevelnum;
        dictcopy();

        spawnrate = csLevelDatabase.levelDB[currentlevelnum].spawnrate;

        foreach (KeyValuePair<Bird, int> pair in birdqtys)
        {
            totalbirds = totalbirds + pair.Value;
            birdoftypeos.Add(pair.Key, 0);  
        }

        StartCoroutine(BeginCountdown());
    }

    void Update()
    {
        if (begin == true)
        { 
            if (totalbirds > 0 && birdsonscreen < totalbirds)
            {
                foreach (KeyValuePair<Bird, int> pair in birdqtys)
                {
                    if (birdsonscreen < totalbirds && birdoftypeos[pair.Key] < pair.Value && pair.Value > 0)
                        Spawnbird(pair.Key);
                }
            }
            else if (totalbirds == 0 && lvlend == false)
            {
                LevelCompleted();
            }
        }
    }

    void Spawnbird(Bird btype)
    {
        if (birdsonscreen == 0)
        {
            Instantiate(btype.birdgo, transform);
            birdsonscreen++;
            birdoftypeos[btype]++;
        }
        else if (Random.Range(1f, 500f) < spawnrate * 5)
        {
            Instantiate(btype.birdgo, transform);
            birdsonscreen++;
            birdoftypeos[btype]++;
        }
    }

    public void RemoveBird(Bird btype)
    {
        birdqtys[btype]--;
        totalbirds--;
        birdsonscreen--;
        birdoftypeos[btype]--;
    }

    public void ReplaceBird(Bird btype)
    {
        birdoftypeos[btype]--;
        birdsonscreen--;
    }

    private void LevelCompleted()
    {
        lvlend = true;
        lvlcomplete.SetActive(true);
        lvlcomplete.GetComponent<AudioSource>().Play();
        lvlcomplete.transform.GetChild(0).GetComponent<MeshRenderer>().sortingOrder = 6;
        csLevelDatabase.levelDB[currentlevelnum].complete = 2;
        if (csLevelDatabase.levelDB[csLevelDatabase.levelDB[currentlevelnum].next].complete == 0) // if next level isnt already available
            csLevelDatabase.levelDB[csLevelDatabase.levelDB[currentlevelnum].next].complete = 1; //make next level available
        StartCoroutine(SceneReturn());
    }

    public void LevelFailed()
    {
        lvlend = true;
        lvlfailed.SetActive(true);
        StartCoroutine(SceneReturn());
    }

    IEnumerator BeginCountdown()    // start level countdown
    {
        yield return new WaitForSeconds(2f);
        begin = true;
    }

    IEnumerator SceneReturn()   // return to menus
    {
        PlayerData.previousscene = SceneManager.GetActiveScene().name;
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Scene_Upgrades");
    }

    void dictcopy()
    {
        foreach (KeyValuePair<Bird, int> pair in csLevelDatabase.levelDB[currentlevelnum].birds)
        {
            birdqtys.Add(pair.Key, pair.Value);
        }
    }
}