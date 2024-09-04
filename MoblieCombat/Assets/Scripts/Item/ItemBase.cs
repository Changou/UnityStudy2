using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _destroyZ;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEffect(other);
            Destroy(gameObject);
        }
    }

    protected virtual void OnEffect(Collider other) { }
}
