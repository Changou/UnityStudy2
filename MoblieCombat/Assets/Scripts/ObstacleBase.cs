using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _destroyZ;
    [SerializeField] float _obDamage;

    private void Awake()
    {
        _target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z + _destroyZ <= _target.position.z)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        JetHealth jet = other.GetComponent<JetHealth>();

        if(jet != null)
        {
            jet.Damage(_obDamage);
        }
    }
}
