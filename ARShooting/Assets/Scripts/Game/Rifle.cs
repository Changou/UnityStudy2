using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Gun2
{
    [Header("소총")]
    [SerializeField] int _currentMagazine;
    [SerializeField] int _currentBullet;

    [SerializeField] float _rifleDelay;

    // Start is called before the first frame update
    void Start()
    {
        _currentMagazine = _magazine;
        _currentBullet = _bulletCnt;
    }

    protected override void Fire()
    {
        if (!_isShot || (_currentBullet == 0 && _currentMagazine == 0)) return;

        --_currentMagazine;

        StartCoroutine(ShotLine());

        if (_currentMagazine == 0 && _currentBullet != 0)
        {
            Debug.Log("리로드");
            StartCoroutine(Delay(_reload, _currentBullet));
            return;
        }

        if(_currentMagazine == 5)
        {
            StartCoroutine(Delay(_rifleDelay));
            return;
        }
        StartCoroutine(Delay(_shotDelay));
    }

    IEnumerator ShotLine()
    {
        Debug.Log("발사");
        GameObject shotLine = Instantiate(_linePrefab.gameObject);
        LineRenderer line = shotLine.GetComponent<LineRenderer>();

        line.SetPosition(0, _shotPos.position);

        line.SetPosition(1, _shotPos.position + _shotPos.forward * _gunRange);
        RayShot();
        while (line.startColor.a > 0)
        {
            Color colorS = line.startColor;
            Color colorE = line.endColor;
            colorS.a -= 1 * Time.deltaTime;
            colorE.a -= 1 * Time.deltaTime;
            line.startColor = colorS;
            line.endColor = colorE;
            yield return null;
        }
        Destroy(shotLine);
    }
    void RayShot()
    {
        Debug.DrawRay(_shotPos.position, _shotPos.forward * _gunRange);
        Ray ray = new Ray(_shotPos.position, _shotPos.forward);

        RaycastHit hitInfo;
        int layerMask = 1 << LayerMask.NameToLayer("Monster");

        if (Physics.Raycast(ray, out hitInfo, _gunRange, layerMask))
        {
            IDamage target = hitInfo.collider.GetComponent<IDamage>();
            if (target != null)
            {
                target.Damage(_pow);
            }
        }
    }
    IEnumerator Delay(float time, int bullet)
    {
        _isShot = false;
        yield return new WaitForSeconds(time);
        if (bullet >= _magazine)
        {
            _currentMagazine = _magazine;
            _currentBullet -= _magazine;
        }
        else
        {
            _currentMagazine = bullet;
            _currentBullet = 0;
        }
        _isShot = true;
    }

    IEnumerator Delay(float time)
    {
        _isShot = false;
        yield return new WaitForSeconds(time);
        _isShot = true;
    }
}
