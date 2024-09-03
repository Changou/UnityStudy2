using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderSetting : MonoBehaviour
{
    [SerializeField] Transform _target;

    private void Update()
    {
        Vector3 pos = Vector3.zero;
        pos.z = _target.position.z;
        pos.y = 14;
        transform.position = pos;
    }
}
