using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgress : MonoBehaviour
{
    Text textLevel;
    void Start()
    {
        textLevel = GetComponent<Text>();
        textLevel.text = "Level " + (Prefs.LevelNumber + 1);
    }
}
