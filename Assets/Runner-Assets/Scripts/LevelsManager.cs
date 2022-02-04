using System.Collections.Generic;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> levels;
    int levelId;
    private void Awake()
    {
        levelId = Prefs.LevelNumber;
        if (levelId >= levels.Count)
        {
            levelId = 0;
            Prefs.LevelNumber = levelId;
        }
        GameObject level = levels[levelId];
        Instantiate(level, transform);
    }
}
