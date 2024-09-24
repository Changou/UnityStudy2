using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("รั"), SerializeField] GameObject[] _prefabs;
    [SerializeField] Transform _gunPos;
    int _currentGunNumber;

    GameObject _currentGun;

    // Start is called before the first frame update
    void Start()
    {
        _currentGunNumber = 0;
        _currentGun = Instantiate(_prefabs[_currentGunNumber], _gunPos);
    }

    public void PrevGun()
    {
        AllClear();

        _currentGunNumber = --_currentGunNumber < 0 ? _prefabs.Length - 1 : _currentGunNumber;

        _currentGun = Instantiate(_prefabs[_currentGunNumber], _gunPos);
    }

    public void NextGun()
    {
        AllClear();

        _currentGunNumber = ++_currentGunNumber > _prefabs.Length - 1 ? 0 : _currentGunNumber;

        _currentGun = Instantiate(_prefabs[_currentGunNumber], _gunPos);
    }

    void AllClear()
    {
        Destroy(_currentGun);
    }
    
}
