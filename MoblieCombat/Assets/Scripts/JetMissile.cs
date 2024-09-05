using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JetMissile : MonoBehaviour
{
    [Header("¹Ì»çÀÏ")]
    [SerializeField] GameObject _prefabs;
    [SerializeField] public int _haveMissile = 0;
    [SerializeField] protected int _maxHaveMissile = 1;
    [SerializeField] protected int _defaultMaxHave;
    [SerializeField] Transform _missileSlot;

    public void GetMissile()
    {
        if (_haveMissile + 1 > _maxHaveMissile) return;
        _haveMissile++;

        GameObject missile = Instantiate(_prefabs);
        missile.transform.SetParent(_missileSlot.GetChild(_haveMissile - 1));
        missile.transform.localPosition = Vector3.zero;
    }

    public void Fire()
    {
        _missileSlot.GetChild(_haveMissile - 1).GetChild(0).GetComponent<Missile>().Fire();
        _haveMissile--;
    }
}
