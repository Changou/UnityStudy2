using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [SerializeField] Transform[] _linePoints;
    [SerializeField] GameObject _prefab;

    [SerializeField] Transform _bulletPoint;

    LineRenderer _line;

    Vector3 mousePos;
    Vector3 _currentPos;
    Vector3 _dir;

    float _power;

    public enum STATE
    {
        IDLE,
        CHARGE,

        MAX
    }

    [SerializeField] STATE _state = STATE.IDLE;

    private void Awake()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._Inst._IsGameOver) return;

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("SlingOn"))
                {
                    _currentPos = Input.mousePosition;
                    _state = STATE.CHARGE;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = Input.mousePosition;
            
            _power = Vector3.Distance(pos,_currentPos);
            _dir = new Vector3(_currentPos.x - pos.x, 0, _currentPos.y - pos.y);

            mousePos = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z));
        }

        if (Input.GetMouseButtonUp(0) && _state == STATE.CHARGE)
        {
            Shot();
            _state = STATE.IDLE;
        }

        if(_state == STATE.IDLE) 
            Default();

        else if(_state == STATE.CHARGE)
            ChargeSling();
    }

    void Shot()
    {
        GameObject bullet = Instantiate(_prefab, _bulletPoint);
        bullet.transform.localPosition = Vector3.zero;
        bullet.transform.SetParent(null);
        bullet.GetComponent<Bullet>().Shot(_dir, _power);
    }

    void ChargeSling()
    {
        List<Vector3> vectorlist = new List<Vector3>();


        for (int i = 0; i < _linePoints.Length; i++)
        {
            vectorlist.Add(mousePos);
            vectorlist.Add(_linePoints[i].position);
        }
        _line.positionCount = vectorlist.Count;
        _line.SetPositions(vectorlist.ToArray());
    }

    void Default()
    {
        _line.positionCount = _linePoints.Length;
        _line.SetPosition(0, _linePoints[0].position);
        _line.SetPosition(1, _linePoints[1].position);
    }
}
