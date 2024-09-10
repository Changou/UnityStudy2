using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRandomSpawn : MonoBehaviour
{
    [SerializeField] GameObject _target;
    [SerializeField] Transform[] _points;

    // Start is called before the first frame update
    void Start()
    {
        GameManager._Inst._GameStart += () => { Spawn(); };
    }

    void Spawn()
    {
        GameObject target = Instantiate(_target);
        target.transform.position = _points[Random.Range(0, _points.Length)].position;
    }
}
