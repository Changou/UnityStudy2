using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetHealth : MonoBehaviour
{
    [Header("����")]
    [SerializeField] float health;

    public void Damage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("���ӿ���");
    }
}
