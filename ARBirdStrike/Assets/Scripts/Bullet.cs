using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    Material[] _effects;

    Rigidbody _rb;

    TrailRenderer _trail;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _trail = GetComponent<TrailRenderer>();
    }

    private void Start()
    {
        _trail.material = _effects[Random.Range(0, _effects.Length)];
        Destroy(gameObject, 3f);
    }

    public void Shot(Vector3 dir, float power)
    {
        _rb.AddRelativeForce(dir.normalized * power);
    }

    private void OnTriggerEnter(Collider other)
    {
        IEntity target = other.GetComponent<IEntity>();

        if (target != null)
        {
            target.Catch(transform.position);
            Destroy(gameObject);
        }
    }
}
