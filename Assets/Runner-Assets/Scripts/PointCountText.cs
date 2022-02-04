using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointCountText : MonoBehaviour
{
    Text textPoint;
    int coinCount;
    void Start()
    {
        textPoint = GetComponent<Text>();
        coinCount = Prefs.PointCount;
        textPoint.text = coinCount.ToString();
    }
    private void OnEnable()
    {
        EventManager.pointChange += EventManagerPointChanged;
    }

    private void EventManagerPointChanged()
    {
            DOTween.To(() => coinCount, x => coinCount = x, Prefs.PointCount, 1).OnUpdate(() => 
            {
                textPoint.text = coinCount.ToString();
            });
    }

    private void OnDisable()
    {
        EventManager.pointChange -= EventManagerPointChanged;
    }
}
