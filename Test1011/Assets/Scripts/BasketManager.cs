using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketManager : MonoBehaviour
{
    [SerializeField] GameObject _basketball;
    [SerializeField] Transform _spawnPos;
    [SerializeField] float _addForce;

    GameObject _ballslot;

    private void Start()
    {
        BasketBallSpawn();   
    }

    Vector3 _startPos;
    Vector3 _endPos;
    Vector3 _dir;
    float _distPower;

    private void Update()
    {
        if (GameManager._Inst._IsGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            _startPos = Input.mousePosition;
        }
        if(Input.GetMouseButton(0)) 
        {
            _endPos = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0)) 
        {
            _distPower = Vector3.Distance(GetMouseWorldPosition(_startPos)
                                           ,GetMouseWorldPosition(_endPos));
            _distPower *= _addForce;

            if (_distPower < 1) return;

            _dir = _endPos - _startPos;

            ThrowBall();
        }
        CheckBall();
    }

    void ThrowBall()
    {
        if(_ballslot != null)
        {
            Vector3 dir = new Vector3(_dir.normalized.x, 1, 1);

            _ballslot.GetComponent<BasketBall>().ThrowingBall(dir, _distPower);
            _ballslot = null;
        }
    }

    Vector3 GetMouseWorldPosition(Vector3 pos)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 1));
        return point;
    }

    void CheckBall()
    {
        BasketBall basket = FindObjectOfType<BasketBall>();

        if (basket == null) BasketBallSpawn();
    }

    void BasketBallSpawn()
    {
        GameObject basketball = Instantiate(_basketball, _spawnPos);
        _ballslot = basketball;
    }
}
