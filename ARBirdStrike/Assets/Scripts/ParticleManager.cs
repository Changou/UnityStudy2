using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _particles;

    public enum TYPE
    {
        START,
        CLEAR,
        OVER,

        MAX
    }
    
    public void ParticleOn(TYPE type)
    {
        GameObject particle = Instantiate(_particles[(int)type].gameObject, transform);
    }
}
