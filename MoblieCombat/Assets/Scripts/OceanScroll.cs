using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanScroll : MonoBehaviour
{
    [SerializeField] Transform _anotherOceans;
    [SerializeField] Transform _target;
    [SerializeField] float _offsetZ;
    
    // Update is called once per frame
    void Update()
    {
        if(_target.transform.position.z >= transform.position.z + _offsetZ)
        {
            _anotherOceans.transform.position = new Vector3(0, 0, transform.position.z + transform.localScale.z * 10);
        }
    }
}
