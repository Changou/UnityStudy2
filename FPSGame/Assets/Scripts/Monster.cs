using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

interface Damagable
{
    void Damage(int damage);
}

public class Monster : MonoBehaviour, Damagable
{
    [SerializeField] int _hp;
    [SerializeField] int _damage;
    [SerializeField] NavMeshAgent _agent;
    bool _isDead;

    private void Awake()
    {
        _isDead = false;
    }

    void Start()
    {
        _agent.SetDestination(GameObject.Find("Player").transform.position);
    }

    void Die()
    {
        _isDead = true;
        Destroy(gameObject);
    }

    public void Damage(int damage)
    {
        if (_isDead) return;

        _hp -= damage;

        if(_hp <= 0)
        {
            Die();
        }
    }

    bool _stayDamage = false;
    Coroutine _damageCor;

    private void OnCollisionEnter(Collision collision)
    {
        Player target = collision.collider.GetComponent<Player>();

        if (target != null || !GameManager._Inst._isGameOver)
        {
            target.OnDamame(_damage);
            _stayDamage = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Player target = collision.collider.GetComponent<Player>();

        if (target != null || !GameManager._Inst._isGameOver)
        {
            if (_stayDamage)
            {
                target.OnDamame(_damage);
                _stayDamage = false;
            }
            else
            {
                if (_damageCor == null)
                    _damageCor = StartCoroutine(DamageDelay());
            }
        }
    }

    IEnumerator DamageDelay()
    {
        yield return new WaitForSeconds(2);
        _stayDamage = true;
        _damageCor = null;
    }

}
