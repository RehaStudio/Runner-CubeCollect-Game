using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScript<T> : MonoBehaviour where T: SingletonScript<T>
{
    static SingletonScript<T> ins;
    public void Awake()
    {
        ins = this;
    }
    public static T Instance
    {
        get => (T)ins;
    }
}
