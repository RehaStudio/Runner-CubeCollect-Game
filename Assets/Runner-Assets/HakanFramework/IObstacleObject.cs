using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleObject 
{
    public void Collide();
    public bool AlreadyCollided { get; }
    public int Power { get; }
}
