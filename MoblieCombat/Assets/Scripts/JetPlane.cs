using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPlane : JetHealth
{
    [Header("기체 관련")]
    [SerializeField] float _speed;
    [SerializeField] float _defaultSpeed;
    [SerializeField] public float _mMX;
    [SerializeField] public float _minY;
    [SerializeField] public float _maxY;
    [SerializeField] float _mMRZ;
    [SerializeField] float _mMRX;
    [SerializeField] Renderer _jetRenderer;
    [SerializeField] ParticleSystem[] _jetEffects;

    [Header("테스트"), SerializeField] bool _isTest = false;

    Color _defaultColor;
    public bool _onInvibility = false;

    private void Awake()
    {
        _defaultColor = _jetRenderer.material.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        _maxHaveMissile = _defaultMaxHave + (DataCentral._Inst._missileLV - 1);
        _health = _defaultHP + (DataCentral._Inst._hpLV - 1);
        _speed = _defaultSpeed + ((_defaultSpeed * 0.05f) * (DataCentral._Inst._speedLV - 1));

        GameManager._Inst.StartGameFunc += () =>
        {
            foreach(ParticleSystem p in _jetEffects)
            {
                p.gameObject.SetActive(true);
            }
        };

        Input.gyro.enabled = true;
    }

    private void Update()
    {
        if(GameManager._Inst._startJetPos + GameManager._Inst._targetDist < transform.position.z &&
            !GameManager._Inst._isGameOver)
        {
            GameManager._Inst.GameClear();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameManager._Inst._isGameOver) return;

        if (_isTest)
            TestMove();
        else
            GyroMove();

        UIManager._Inst.UpdateSpeed((int)_speed);
        UIManager._Inst.UpdateHp((int)_health);

        AutoMove();
    }

    void TestMove()
    {
        float posX = Input.GetAxis("Horizontal");
        float posY = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x += posX, -_mMX, _mMX);
        pos.y = Mathf.Clamp(pos.y += posY, _minY, _maxY);

        float x = Mathf.InverseLerp(-_mMX, _mMX, pos.x);
        float rotZ = Mathf.Lerp(-_mMRZ, _mMRZ, x);

        transform.rotation = Quaternion.Euler(0, 0, -rotZ);

        transform.position = pos;
    }

    void GyroMove()
    {
        Vector3 gyroRot = Input.gyro.rotationRate;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x += gyroRot.y, -_mMX, _mMX);
        pos.y = Mathf.Clamp(pos.y += -gyroRot.x, _minY, _maxY);

        float x = Mathf.InverseLerp(-_mMX, _mMX, pos.x);
        float rotZ = Mathf.Lerp(-_mMRZ, _mMRZ, x);

        transform.rotation = Quaternion.Euler(0, 0, -rotZ);

        transform.position = pos;
    }

    void AutoMove()
    {
        transform.position += Vector3.forward * _speed * Time.deltaTime;
    }

    public void JetSpeedUp(float time)
    {
        StartCoroutine(SpeedUp(time));
    }

    IEnumerator SpeedUp(float time)
    {
        float defaultSpeed = _speed;
        _speed += _speed * 0.2f;
        _jetRenderer.material.color = Color.blue;
        yield return new WaitForSeconds(time);
        _jetRenderer.material.color = _defaultColor;
        _speed = defaultSpeed;
    }

    public void JetInvibility(float time)
    {
        StartCoroutine(InvicibilityOn(time));
    }

    IEnumerator InvicibilityOn(float time)
    { 
        _onInvibility = true;
        _jetRenderer.material.color = Color.yellow;
        yield return new WaitForSeconds(time);
        _jetRenderer.material.color = _defaultColor;
        _onInvibility = false;
    }
}
