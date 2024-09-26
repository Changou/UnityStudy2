using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NomalMonster : Monster, IDamage
{
    Animator _anim;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isCoolTime) return;

        Attack();
    }

    [Header("È­¿°±¸"), SerializeField] GameObject _fireball;
    bool _isCoolTime = false;

    void Attack()
    {
        _anim.SetTrigger("Attack");
        GameObject fireball = Instantiate(_fireball);
        fireball.transform.position = transform.position;
        Vector3 dir = Camera.main.transform.position - fireball.transform.position;

        fireball.transform.rotation = Quaternion.LookRotation(dir);

        fireball.GetComponent<FireBall>().SetStat(_power, _attackRange);
        StartCoroutine(CoolTime(_attackDelay));
    }

    IEnumerator CoolTime(float time)
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(time);
        _isCoolTime = false;
    }

    public void Damage(int power)
    {
        _hp -= power;
        StartCoroutine(CoolTime(1));

        if(_hp <= 0)
        {
            Die();
        }

        _anim.SetTrigger("Hit");
    }

    protected override void Die()
    {
        _anim.SetTrigger("Die");
        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
