using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : PoolObject
{
    ParticleSystem particleSystem;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
       
    }
    private void OnEnable()
    {
        particleSystem.Play();
        if (!particleSystem.main.loop)
            StartCoroutine(Destroying(particleSystem.main.duration));
    }
    IEnumerator Destroying(float time)
    {
        yield return new WaitForSeconds(time);
        this.DestroyPool();
    }
}
