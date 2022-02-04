using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public delegate void LevelStartedHandler();
public delegate void LevelCompletedHandler();
public delegate void LevelFailedHandler();
public delegate void PointChangedHandler();
public delegate void PowerUpHandler();
public delegate void PowerUpEndedHandler();
public static class EventManager 
{
    public static event LevelStartedHandler levelStarted;
    public static event LevelCompletedHandler levelCompleted;
    public static event LevelFailedHandler levelFailed;
    public static event PointChangedHandler pointChange;
    public static event PowerUpHandler powerUp;
    public static event PowerUpEndedHandler powerUpEnded;
    public static Action<int> CubeCountChanged;
    public static void LevelStarted()
    {
        levelStarted();
    }
    public static void LevelFailed()
    {
        levelFailed();
    }
    public static void LevelCompleted()
    {
        levelCompleted();
    }
    public static void PowerUp()
    {
        if (powerUp != null)
            powerUp();
    }
    public static void PowerUpEnded()
    {
        powerUpEnded();
    }
    public static void PointChange()
    {
        pointChange();
    }
}
