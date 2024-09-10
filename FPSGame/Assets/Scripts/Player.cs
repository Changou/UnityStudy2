using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WEAPON
{
    GUN,
    GRANADE,

    MAX
}

public enum STATE
{
    LOW,
    UP,
    JUMP,

    MAX
}

public class Player : MonoBehaviour
{
    [Header("플레이어")]
    [SerializeField] int _hp;
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;
    [SerializeField] float _maxDistance;
    [SerializeField] int _damage;
    [SerializeField] bool _isDead;
    [SerializeField] STATE _state = STATE.UP;

    [Header("기능들")]
    [SerializeField] JoyStick _joy;
    [SerializeField] CinemachineVirtualCamera _mainCam;
    [SerializeField] CinemachineVirtualCamera _zoomInCam;
    [SerializeField] Transform _gun;
    [SerializeField] DOTweenAnimation _playerBow;
    [SerializeField] Transform _firePoint;
    [SerializeField] LineRenderer _shotLine;
    [SerializeField] Transform _granade;
    [SerializeField] StateUI _stateui;

    public int _HP => _hp;

    Vector3 _defaultGunPos;

    Rigidbody _rb;

    bool _isBowDown = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _defaultGunPos = _gun.localPosition;
    }

    void Start() 
    {
        transform.position = GameManager._Inst._startPos.position;
        Input.gyro.enabled = true;
        _isBowDown = false;
        _isDead = false;
    }

    private void Update()
    {
        if (_isDead || GameManager._Inst._isGameOver) return;

        PlayerRot();

        if (Input.GetKey(KeyCode.D))
        {
            TestRot(1);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            TestRot(-1);
        }

        BowCheck();

        _prevAccel = Input.acceleration.x;
    }

    public void WeaponChange(int num)
    {
        if(num == (int)WEAPON.GUN)
        {
            _granade.gameObject.SetActive(false);
            _gun.gameObject.SetActive(true);
        }
        else if(num == (int)WEAPON.GRANADE)
        {
            _granade.gameObject.SetActive(true);
            _gun.gameObject.SetActive(false);
        }
    }

    public void OnDamame(int damage)
    {
        _hp -= damage;

        UIManager._Inst.SetHP();

        if(_hp <= 0)
        {
            _isDead = true;
            GameManager._Inst.GameOver();
        }
    }
    public void Shot()
    {
        RaycastHit hit;

        Vector3 hitPos = Vector3.zero;

        if (Physics.Raycast(_firePoint.position, _firePoint.forward, out hit, _maxDistance))
        {
            Damagable target = hit.collider.GetComponent<Damagable>();

            if (target != null)
            {
                target.Damage(_damage);
            }

            hitPos = hit.point;
        }
        else
            hitPos = _firePoint.position + _firePoint.forward * _maxDistance;

        StartCoroutine(ShotEffect(hitPos));

    }

    IEnumerator ShotEffect(Vector3 hitPos)
    {
        _shotLine.SetPosition(0,_firePoint.position);
        _shotLine.SetPosition(1, hitPos);
        _shotLine.enabled = true;

        yield return new WaitForSeconds(0.03f);
        _shotLine.enabled = false;
    }

    void TestRot(int num)
    {
        transform.Rotate(0, num, 0);
    }

    public void ZoomOut()
    {
        _gun.localPosition = _defaultGunPos;
        _zoomInCam.Priority = 10;
    }

    public void ZoomIn()
    {
        _defaultGunPos = _gun.localPosition;
        _gun.localPosition = new Vector3(0, _gun.localPosition.y, _gun.localPosition.z);
        _zoomInCam.Priority = 12;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
    }

    void BowCheck()
    {
        if (Input.acceleration.x > _prevAccel + 0.2f && _rb.velocity.y <= 0.1f && !_isBowDown)
        //if(Input.GetKeyDown(KeyCode.S) &&_rb.velocity.y <= 0.1f &&!_isBowDown)
        {
            _playerBow.DORestartById("Down");
            _state = STATE.LOW;
            _isBowDown = true;
        }
        else if (Input.acceleration.x  < _prevAccel - 0.2f && _rb.velocity.y <= 0.1f)
        //if (Input.GetKeyDown(KeyCode.Space) && _rb.velocity.y <= 0.1f) 
        {
            if (_isBowDown)
            {
                _playerBow.DORestartById("Up");
                _isBowDown = false;
                _state = STATE.UP;
                return;
            }
            _state = STATE.JUMP;
            _rb.AddForce(Vector3.up * _jumpPower);
        }

        if(_rb.velocity.y <= 0.1f)
        {
            _state = STATE.UP;
        }

        _stateui.SetState(_state);
    }

    float _prevAccel = 0;

    void PlayerMove()
    {
        Vector3 frontBack = transform.forward * _joy._Vert;
        Vector3 leftRight = transform.right * _joy._Horizon;
        Vector3 up = new Vector3(0, _rb.velocity.y, 0);

        _rb.velocity = ((frontBack + leftRight) * _speed) + up;
    }

    void PlayerRot()
    {
        Vector3 gyro = Input.gyro.rotationRate;

        _mainCam.transform.Rotate(-(gyro.x * 2f), 0, 0);
        transform.Rotate(0, -(gyro.y * 2f), 0);
    }
}
