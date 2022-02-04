using System.Collections;
using UnityEngine;

public class PoolObject : MonoBehaviour, IPoolObject
{
    GameObject IPoolObject.gameObject_ => gameObject;   
}
