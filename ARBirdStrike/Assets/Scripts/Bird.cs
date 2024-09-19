using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour, IEntity
{
    float _speed = 0.5f;

    Rigidbody _rb;
    Animator _anim;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * _speed * Time.deltaTime;

        CheckAlive();
    }

    void CheckAlive()
    {
        if(transform.position.x < -3 || transform.position.x > 3 || transform.position.y < -3)
            Destroy(gameObject);
    }

    public void Catch(Vector3 pos)
    {
        GameManager._Inst.CatchBird();
        _anim.SetTrigger("Die");
        _rb.useGravity = true;
    }
}
