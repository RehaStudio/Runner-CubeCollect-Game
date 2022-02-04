using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelManager : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject levelCompleteMenu;
    [SerializeField] GameObject levelFailedMenu;
    [SerializeField] GameObject infoMoveMenu;
    [SerializeField] GameObject powerUpMenu;
    [Header("Level Completed UI Objects")]
    [SerializeField] Text textLevelCompleted;
    [SerializeField] Text textCollectedPoint;
    private void OnEnable()
    {
        EventManager.levelFailed += EventManagerLevelFailed;
        EventManager.levelCompleted += EventManagerLevelCompleted;
        EventManager.powerUpEnded += EventManagerPowerUpEnded;
    }

    private void OnDisable()
    {
        EventManager.levelFailed -= EventManagerLevelFailed;
        EventManager.levelCompleted -= EventManagerLevelCompleted;
        EventManager.powerUpEnded -= EventManagerPowerUpEnded;
    }

    private void EventManagerPowerUpEnded()
    {
        powerUpMenu.SetActive(true);
    }

    private void EventManagerLevelCompleted()
    {
        WaitAndAction(1f, OpenLevelComplete);
    }

    private void EventManagerLevelFailed()
    {
        WaitAndAction(1f, OpenLevelFailed);
    }

    void WaitAndAction(float time, Action act)
    {
        StartCoroutine(WaitAndActionIE(time, act));
    }
    IEnumerator WaitAndActionIE(float time, Action act)
    {
        yield return new WaitForSeconds(time);
        act.Invoke();
    }
    public void OpenLevelComplete()
    {
        textLevelCompleted.text = "LEVEL " + (Prefs.LevelNumber + 1) + " COMPLETED";
        textCollectedPoint.text = "Collected Point +" + Prefs.PointLastLevel;
        levelCompleteMenu.SetActive(true);
    }
    public void OpenLevelFailed()
    {
        levelFailedMenu.SetActive(true);
    }
    public void CloseInfoMove()
    {
        infoMoveMenu.SetActive(false);
    }
    public void PowerUp()
    {
        EventManager.PowerUp();
        powerUpMenu.SetActive(false);
    }
    public void LevelStart()
    {
        EventManager.LevelStarted();
        CloseInfoMove();
        powerUpMenu.SetActive(true);
    }
}
