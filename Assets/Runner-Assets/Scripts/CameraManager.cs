using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.2f;
    public float soomtPosDam = 5;

    private void OnEnable()
    {
        EventManager.CubeCountChanged += UpdatePosZ;    
    }
    void UpdatePosZ(int count)
    {
        target.localPosition -= Vector3.forward * count * .5f;
    }
    private void Start()
    {
        transform.position = target.position;
    }

    private void FixedUpdate()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * soomtPosDam);
        }
    }
    private void OnDisable()
    {
        EventManager.CubeCountChanged -= UpdatePosZ;
    }
}
