using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class DataCentral : MonoBehaviour
{
    public static DataCentral _Inst;

    private void Awake()
    {
        var obj = FindObjectsOfType<DataCentral>();
        if(obj.Length == 1)
        {
            _Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public enum LEVEL
    {
        HP,
        SPEED,
        MISSILE,

        MAX
    }

    public int _lv = 1;
    public int _hpLV = 1;
    public int _speedLV = 1;
    public int _maxSpeedLV;
    public int _missileLV = 1;
    public int _maxMissileLV;

    public bool _isGameClear;

    public void LevelUp(int num)
    {
        _lv++;

        switch ((LEVEL)num)
        {
            case LEVEL.HP:
                _hpLV++;
                break;
            case LEVEL.SPEED: 
                _speedLV = ++_speedLV > _maxSpeedLV? _speedLV-- : _speedLV;
                break;
            case LEVEL.MISSILE:
                _missileLV = ++_missileLV > _maxMissileLV ? _missileLV-- : _missileLV;
                break;
        }
    }
}
