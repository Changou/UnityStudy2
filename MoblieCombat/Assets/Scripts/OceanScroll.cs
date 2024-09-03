using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanScroll : MonoBehaviour
{
    [SerializeField] Transform _anotherOceans;
    [SerializeField] Transform _target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 pos = transform.parent.position;
            pos.z += _anotherOceans.localScale.z * 10;
            _anotherOceans.position = pos;
        }
    }
}
