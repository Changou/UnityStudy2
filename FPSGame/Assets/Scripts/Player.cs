using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpPower;

    [SerializeField] JoyStick _joy;
    [SerializeField] CinemachineVirtualCamera _mainCam;
    [SerializeField] CinemachineVirtualCamera _zoomInCam;
    [SerializeField] Transform _gun;
    [SerializeField] DOTweenAnimation _playerBow;

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
        Input.gyro.enabled = true;
        _isBowDown = false;
    }

    private void Update()
    {
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
        //if (Input.acceleration.x > _prevAccel + 0.3f && _rb.velocity.y <= 0.1f && !_isBowDown)
        if(Input.GetKeyDown(KeyCode.S) &&_rb.velocity.y <= 0.1f &&!_isBowDown)
        {
            _playerBow.DORestartById("Down");
            _isBowDown = true;
        }
        //if (Input.acceleration.x  < _prevAccel - 0.3f && _rb.velocity.y <= 0.1f)
        if (Input.GetKeyDown(KeyCode.Space) && _rb.velocity.y <= 0.1f) 
        {
            if (_isBowDown)
            {
                _playerBow.DORestartById("Up");
                _isBowDown = false;
                return;
            }
            _rb.AddForce(Vector3.up * _jumpPower);
        }
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
