using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class csMPstats: MonoBehaviour
{
    public GameObject gobirdskilled, gocoinscollected, goaccuracy, goexpgained, goscore;
    public GameObject coinsdisplay, coinparticles;

    private float t;
    private bool accum = false;
    private float tempcoins;
    private float tempscore;

	void Start ()
	{
        t = 0;
        gocoinscollected.GetComponent<TextMesh>().text = "";
        gobirdskilled.GetComponent<TextMesh>().text = "";
        goaccuracy.GetComponent<TextMesh>().text = "";
        //goexpgained.GetComponent<TextMesh>().text = PlayerData.roundexp.ToString();
        goscore.GetComponent<TextMesh>().text = "";
    }

    void Update()
    {
        t += Time.deltaTime;

        if (t > 2.0f && gobirdskilled.GetComponent<TextMesh>().text == "")
            gobirdskilled.GetComponent<TextMesh>().text = PlayerData.roundbirds.ToString();
        if (t > 1.0f && gocoinscollected.GetComponent<TextMesh>().text == "")
            gocoinscollected.GetComponent<TextMesh>().text = PlayerData.roundcoins.ToString();
        if (t > 3.0f && goaccuracy.GetComponent<TextMesh>().text == "")
            goaccuracy.GetComponent<TextMesh>().text = PlayerData.roundaccuracy.ToString("0.0") + "%";
        if (t > 4.0f && goscore.GetComponent<TextMesh>().text == "")
        {
            tempscore = PlayerData.roundcoins + PlayerData.roundbirds * .25f;
            tempscore = tempscore + tempscore * PlayerData.roundaccuracy/100f * .1f;
            goscore.GetComponent<TextMesh>().text = Mathf.Round(tempscore).ToString();
            tempcoins = PlayerData.totalcoins;
        }
        if (t > 5.0f && accum == false)
        {
            accum = true;
            t = 0;
            ParticleSystem.EmissionModule emod = coinparticles.GetComponent<ParticleSystem>().emission;
            ParticleSystem.MainModule mmod = coinparticles.GetComponent<ParticleSystem>().main;
            ParticleSystem psys = coinparticles.GetComponent<ParticleSystem>();
            emod.rateOverTimeMultiplier = tempscore + 1f;
            psys.Play();
        }

        if (accum == true)
        {
            PlayerData.totalcoins = Mathf.RoundToInt(Mathf.Lerp(tempcoins, (tempcoins + tempscore), t / 1f));
            coinsdisplay.GetComponent<TextMesh>().text = PlayerData.totalcoins.ToString();
        }

        if (accum == false && Input.GetKeyDown(KeyCode.Mouse0))
            t = 4.9f;
        if (accum == true && t > 1f && Input.GetKeyDown(KeyCode.Mouse0))
            gameObject.SetActive(false);
    }
}