using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class Boss : Monster, IDamage
{
    [SerializeField] Renderer _ren;
    Animator _anim;

    bool _isAction = true;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        _isAction = true;

        Quaternion rot = transform.rotation;
        rot.x = 0;
        transform.rotation = rot;

        transform.DOLocalMove(Camera.main.transform.forward * 7f, 3f).OnComplete(()=>
        {
            _isAction = false;
        });
    }

    private void Update()
    {
        if (_isCoolTime || _isAction) return;

        Attack();
    }

    [Header("È­¿°±¸"), SerializeField] GameObject _fireball;
    bool _isCoolTime = false;
    [SerializeField] Transform _attackPos;

    void Attack()
    {
        _anim.SetTrigger("Attack");

        GameObject fireball = Instantiate(_fireball,_attackPos);
        fireball.transform.localPosition = Vector3.zero;
        fireball.transform.SetParent(null);
        fireball.transform.localScale = new Vector3(0.5f,0.5f,0.5f);

        Vector3 dir = Camera.main.transform.position - fireball.transform.position;

        fireball.transform.rotation = Quaternion.LookRotation(dir);

        fireball.GetComponent<FireBall>().SetStat(_power, _attackRange);
        StartCoroutine(CoolTime());
    }

    IEnumerator CoolTime()
    {
        _isCoolTime = true;
        yield return new WaitForSeconds(_attackDelay);
        _isCoolTime = false;
    }

    Coroutine _cor;
    public void Damage(int power)
    {
        _hp -= power;

        if (_hp <= 0)
        {
            Die();
            return;
        }

        if(_cor != null)
        {
            StopCoroutine(_cor);
        }
        _cor = StartCoroutine(HitRenderer());
    }

    IEnumerator HitRenderer()
    {
        Color dColor = _ren.material.color;
        _ren.material.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _ren.material.color = dColor;
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
