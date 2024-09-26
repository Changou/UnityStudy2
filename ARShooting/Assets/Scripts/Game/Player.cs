using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour,IDamage
{
    [SerializeField] int _hp;

    Collider _cor;

    private void Awake()
    {
        _cor = GetComponent<Collider>();
    }

    public void Damage(int power)
    {
        _hp -= power;
        UIManager._Inst.UpdateHP(_hp);

        if(_hp <= 0)
        {
            Die();
            return;
        }
    }

    void Die()
    {
        _cor.enabled = false;
    }
}
