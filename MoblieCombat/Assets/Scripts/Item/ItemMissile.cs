using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMissile : ItemBase
{
    protected override void OnEffect(Collider other)
    {
        JetMissile jet = other.GetComponent<JetMissile>();

        if(jet != null)
        {
            jet.GetMissile();
        }
    }
}
