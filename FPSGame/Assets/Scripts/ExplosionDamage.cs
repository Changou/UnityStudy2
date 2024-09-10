using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    [SerializeField] int _damage;

    private void OnTriggerEnter(Collider other)
    {
        Damagable target = other.GetComponent<Damagable>();

        if(target != null )
        {
            target.Damage(_damage);
        }
    }
}
