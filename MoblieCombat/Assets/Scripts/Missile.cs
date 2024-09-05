using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] ParticleSystem[] _effects;
    [SerializeField] ParticleSystem[] _bomb;
    [SerializeField] float _speed;
    [SerializeField] float _lifeTime;
    public bool _isFire = false;

    private void Awake()
    {
        _isFire = false;
    }

    private void Update()
    {
        if (!_isFire) return;

        transform.localPosition += Vector3.forward * _speed * Time.deltaTime;
    }

    public void Fire()
    {
        _isFire = true;
        transform.SetParent(null);
        foreach(ParticleSystem p in _effects)
        {
            p.Play();
        }
        StartCoroutine(Explosion());
    }
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(_lifeTime);
        foreach(ParticleSystem p in _bomb)
        {
            p.Play();
        }
        ObstacleBase[] obstacles = GameObject.FindObjectsOfType<ObstacleBase>();
        if(obstacles.Length > 0)
        {
            for(int i = 0; i < obstacles.Length; i++)
            {
                Destroy(obstacles[i].gameObject);
            }
        }
        Destroy(gameObject, 1f);
    }
}
