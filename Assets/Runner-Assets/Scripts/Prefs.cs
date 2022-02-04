using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Prefs 
{
    public static int LevelNumber 
    {
        get => PlayerPrefs.GetInt("level"); 
        set => PlayerPrefs.SetInt("level", value );
    }
    public static int PointCount 
    {
        get => PlayerPrefs.GetInt("point");
    }
    public static int PointLastLevel
    {
        get => PlayerPrefs.GetInt("pointLastLevel");
    }
    public static void AddPoint(int count)
    {
        PlayerPrefs.SetInt("point", PointCount + count);
        PlayerPrefs.SetInt("pointLastLevel", count);
        EventManager.PointChange();
    }
}
