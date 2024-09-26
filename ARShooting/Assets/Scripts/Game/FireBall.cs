using Microsoft.Win32;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    int _power;
    int _range;

    [SerializeField] float _speed;

    public void SetStat(int power, int range)
    {
        _power = power;
        _range = range;
    }
    
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
        _rb.AddForce(transform.forward * _speed, ForceMode.Impulse);
    }

    private void Update()
    {
        if(Vector3.Distance(_startPos, transform.position) >= _range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamage target = other.GetComponent<IDamage>();

        if(target != null)
        {
            target.Damage(_power);
        }
    }
}
