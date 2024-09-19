using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
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
