using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class Serializer
{
    public static T Load<T>(string filename) where T : class
    {
        if (File.Exists(filename))
        {
            try
            {
                using (Stream stream = File.OpenRead(filename))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    return formatter.Deserialize(stream) as T;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
        return default(T);
    }

    public static void Save<T>(string filename, T data) where T : class
    {
        using (Stream stream = File.OpenWrite(filename))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, data);
        }
    }

    public static void Delete(string filename)
    {
        if (File.Exists(filename))
        {
            try
            {
                File.Delete(filename);
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }
    }
}

[System.Serializable]
public class csSerializer : MonoBehaviour
{
    [SerializeField]
    private List<Serialobj> lso;

    void OnApplicationQuit()
    {
        if (PlayerData.clearquit == false)
        {
            SaveData_StaffDB();

            lso = new List<Serialobj>();
            foreach (KeyValuePair<int, Level> pair in csLevelDatabase.levelDB)
            {
                Serialobj so = new Serialobj();
                so.id = pair.Key;
                so.val = pair.Value.complete;
                lso.Add(so);
            }
            Serializer.Save("leveldata", lso);

            lso = new List<Serialobj>();
            foreach (KeyValuePair<int, Gem> pair in csGemDatabase.gemDB)
            {
                Serialobj so = new Serialobj();
                so.id = pair.Key;
                so.val = pair.Value.cost;
                lso.Add(so);
            }
            Serializer.Save("gemdata", lso);

            PlayerPrefs.SetInt("totalcoins", PlayerData.totalcoins);
            PlayerPrefs.SetInt("staffnum", PlayerData.staff.id);
            PlayerPrefs.SetInt("gemnum", PlayerData.gem.id);
        }
        /*if (Application.platform != RuntimePlatform.WindowsEditor)
            System.Diagnostics.Process.GetCurrentProcess().Kill();*/
    }
    void OnApplicationPause()
    {
        if (Application.platform != RuntimePlatform.WindowsEditor)
            OnApplicationQuit();
    }
    int x = 0;
    void SaveData_StaffDB()
    {
        foreach (KeyValuePair<int, Staff> pair in csWeaponDatabase.staffDB)
        {
            PlayerPrefs.SetInt("staff" + pair.Key + "ownership", pair.Value.ownership);
            x++;
            PlayerPrefs.SetInt("staffDB.count", x + 1);
        }
    }
}

[System.Serializable]
public class Serialobj
{
    public int id;
    public int val;
}