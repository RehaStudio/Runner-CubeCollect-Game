using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
    [SerializeField] GameObject confettis;
    void OnEnable()
    {
        EventManager.levelCompleted += EventManagerLevelCompleted;
    }

    private void EventManagerLevelCompleted()
    {
        confettis.SetActive(true);
    }

    void OnDisable()
    {
        EventManager.levelCompleted -= EventManagerLevelCompleted;
    }
}
