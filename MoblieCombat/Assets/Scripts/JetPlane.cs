using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JetPlane : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _maxPosZ;
    [SerializeField] Vector3 _rootPos;

    
    // Start is called before the first frame update
    void Start()
    {
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gyroRot = Input.gyro.rotationRate;


        AutoMove();
    }

    void GyroMove()
    {

    }

    void AutoMove()
    {
        transform.position += Vector3.forward * _speed * Time.deltaTime;
    }
}
