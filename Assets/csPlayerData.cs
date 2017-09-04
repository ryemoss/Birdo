using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class csPlayerData : MonoBehaviour
{
    void Start()
    {
        PlayerData.Initialize();
    }
}

public static class PlayerData
{
    public static int roundbirds = 0;
    public static int roundcoins = 0;
    public static int roundexp = 0;
    public static float roundaccuracy = 0.0f;
    public static int roundshots = 0;

    public static int totalcoins = 10;
    public static int totalbirds = 0;

    public static Level currentlevel;
    public static int currentlevelnum;
    public static bool clearquit = false;

    public static string previousscene;
    public static bool prevlevelwin;
    public static Staff staff;
    public static Gem gem;
    public static GameObject bullet;
    public static float health = 100f;
    public static float energy = 100f;
    public static float power;
    public static float firerate;
    public static float reloadspd;

    public static void Initialize()
    {
        if (PlayerPrefs.GetInt("staffnum") < 100)
            staff = csWeaponDatabase.staffDB[100];
        else
            staff = csWeaponDatabase.staffDB[PlayerPrefs.GetInt("staffnum")];

        if (PlayerPrefs.GetInt("gemnum") < 100)
            gem = csGemDatabase.gemDB[100];
        else
            gem = csGemDatabase.gemDB[PlayerPrefs.GetInt("gemnum")];

        bullet = staff.bullet;
        totalcoins = PlayerPrefs.GetInt("totalcoins");

        UpdateStoredStats();
    }

    public static void Roundend()
    {
        totalbirds = totalbirds + roundbirds;
        if (roundshots > 0)
            roundaccuracy = (roundbirds / (float)roundshots) * 100;
    }

    public static void UpdateStoredStats()
    {
        power = staff.power;
        firerate = staff.firerate;
        reloadspd = staff.reloadspeed;
        health = gem.health;
        energy = gem.energy;
    }

}