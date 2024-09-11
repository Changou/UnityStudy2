using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject _basketBall;
    [SerializeField] float _ballDelay;
    [SerializeField] Transform _spawnPoint;
    GameObject _ball;

    Vector3 _currentPos;
    Vector3 _dir;

    float _power;

    // Update is called once per frame
    void Update()
    {
        if (_ball != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _currentPos = Input.mousePosition;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 pos = Input.mousePosition;

                _power = Vector3.Distance(pos, _currentPos);
                _dir = new Vector3(pos.x - _currentPos.x, 0, pos.y - _currentPos.y);
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (_power > 100)
                {
                    Shoot();
                }
            }
        }
        if(_spawnPoint.childCount == 0)
        {
            NewBall();
        }
    }

    void Shoot()
    {
        Rigidbody rb = _ball.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce((_dir.normalized + _ball.transform.up) * _power);
    }

    void NewBall()
    {
        GameObject ball = Instantiate(_basketBall,_spawnPoint);
        _ball = ball;
    }
}
