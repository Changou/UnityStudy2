using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public interface IDamage
{
    void Damage(int power);
}

public class Monster : MonoBehaviour
{
    [Header("∏ÛΩ∫≈Õ")]
    [SerializeField] protected int _hp;
    [SerializeField] protected int _attackRange;
    [SerializeField] protected float _attackDelay;
    [SerializeField] protected int _power;

    Vector3 _dir;

    protected virtual void Start()
    {
        _dir = Camera.main.transform.position - transform.position;


        transform.rotation = Quaternion.LookRotation(_dir.normalized);

        Quaternion look = transform.rotation;
        transform.rotation = look;
    }

    protected virtual void Die() { }
}
