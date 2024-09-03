using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPlane : MonoBehaviour
{
    [Header("기체 관련")]
    [SerializeField] float _speed;
    [SerializeField] float _mMX;
    [SerializeField] float _minY;
    [SerializeField] float _maxY;
    [SerializeField] float _mMRZ;
    [SerializeField] float _mMRX;

    [Header("테스트"), SerializeField] bool _isTest = false;

    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTest)
            TestMove();
        else
            GyroMove();
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
        pos.x = Mathf.Clamp(pos.x += gyroRot.x, -_mMX, _mMX);
        pos.y = Mathf.Clamp(pos.y += gyroRot.y, _minY, _maxY);

        float x = Mathf.InverseLerp(-_mMX, _mMX, pos.x);
        float rotZ = Mathf.Lerp(-_mMRZ, _mMRZ, x);

        transform.rotation = Quaternion.Euler(0, 0, -rotZ);

        transform.position = pos;
    }

    void AutoMove()
    {
        transform.position += Vector3.forward * _speed * Time.deltaTime;
    }
}
