using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackObject 
{
    public void SetPosition(int index,Transform _parent,float time);

    public void Destroy();
    public float Scale { get; }
    public bool AlreadyCollided { get; }
}
