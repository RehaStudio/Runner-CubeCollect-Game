using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSphere : MonoBehaviour, IObstacleObject
{
    [SerializeField] int power;
    bool alreadyCollided;
    public void Collide()
    {
        alreadyCollided = true;
        Destroy(gameObject);
    }
    public bool AlreadyCollided => alreadyCollided;

    public int Power => power;
}
