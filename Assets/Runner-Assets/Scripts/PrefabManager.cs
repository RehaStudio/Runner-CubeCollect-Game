using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : SingletonScript<PrefabManager>
{
    [SerializeField] CubeObject cubeObject;
    [SerializeField] ParticleObject destroyParticle, collectParticle;

    public CubeObject CubeObjectPrefab => cubeObject;
    public ParticleObject DestroyParticle => destroyParticle;
    public ParticleObject CollectParticle => collectParticle;
}
