using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JetHealth : JetMissile
{
    [Header("Ω∫≈»")]
    [SerializeField] protected float _health;
    [SerializeField] protected float _defaultHP;

    [SerializeField] CinemachineVirtualCamera _cam;

    [SerializeField] GameObject _dieExplosion;

    private void Awake()
    {
        _dieExplosion.SetActive(false);
    }

    public void Damage(float damage)
    {
        _health -= damage;
        StartCoroutine(CameraShake());
        if(_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        _dieExplosion.SetActive(true);
        GameManager._Inst.GameOver();
    }

    IEnumerator CameraShake()
    {
        CinemachineBasicMultiChannelPerlin perlin = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 5;
        perlin.m_FrequencyGain = 1;
        yield return new WaitForSeconds(0.5f);
        perlin.m_AmplitudeGain = 0;
        perlin.m_FrequencyGain = 0;
    }
}
