using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInvicibility : ItemBase
{
    [SerializeField] float _time;
    protected override void OnEffect(Collider other)
    {
        JetPlane jet = other.GetComponent<JetPlane>();
        if (jet != null)
        {
            jet.JetInvibility(_time);
        }
    }
}
