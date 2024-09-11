using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceARObjectEx : PlaceARObject
{
    [Header("��ũ���� �� ������ ������ ������Ʈ "), SerializeField]
    GameObject _spawnObject;
    //---------------------------------------
    protected override void LateUpdate()
    {
        base.LateUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = transform.position + Vector3.up;

            if (_spawnObject)
                Instantiate(_spawnObject, pos, transform.rotation);

        }// if( Input.GetMouseButtonDown(0) )
    }
}
