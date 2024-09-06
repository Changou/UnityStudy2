using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] JoyStick _joy;

    Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Start() 
    {
        Input.gyro.enabled = true;
    }

    private void Update()
    {
        PlayerRot();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 frontBack = transform.forward * _joy._Vert;
        Vector3 leftRight = transform.right * _joy._Horizon;
        _rb.velocity = (frontBack + leftRight) * _speed;
    }

    void PlayerRot()
    {
        Vector3 gyro = Input.gyro.rotationRate;

        Camera.main.transform.Rotate(-(gyro.x * 1.5f), 0, 0);
        transform.Rotate(0, -(gyro.y * 1.5f), 0);
    }
}
