using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void NextLevel()
    {
        Prefs.LevelNumber++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
