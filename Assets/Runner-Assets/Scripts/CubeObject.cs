using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CubeObject : PoolObject, IStackObject
{
    bool alreadyCollided;
    public float Scale { get => transform.localScale.y; }

    public bool AlreadyCollided => alreadyCollided;

    public void Destroy()
    {
        Destroy(gameObject); 
    }

    public void SetPosition(int index,Transform _parent,float time)
    {
        alreadyCollided = true;
        transform.parent = _parent;
        Vector3 target = new Vector3(0, -index * Scale + Scale/2, 0);
        transform.DOLocalMove(target, time);
    }
}
