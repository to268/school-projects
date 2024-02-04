using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static float GetVolume()
    {
        return PlayerPrefs.GetFloat("volume");
    }

    public static void SetVolume(float amount)
    {
        PlayerPrefs.SetFloat("volume", amount);
        PlayerPrefs.Save();
    }
    
    public static bool GetHasBeatenBoss(String boss)
    {
        return PlayerPrefs.GetInt(boss) == 0 ? false : true;
    }

    public static void SetHasBeatenBoss(String boss, bool beaten)
    {
        PlayerPrefs.SetInt(boss, beaten ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static int GetResolution()
    {
        return PlayerPrefs.GetInt("resolution");
    }
    
    public static void SetResolution(int index)
    {
        PlayerPrefs.SetInt("resolution", index);
    }
}