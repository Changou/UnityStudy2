using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotGun : Gun2
{
    [Header("샷건")]
    [SerializeField] int _currentMagazine;
    [SerializeField] int _currentBullet;
    [SerializeField] int _radiation;
    [SerializeField] float _shotGunDelay;

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

        if (_currentMagazine % _radiation == 0)
        {
            StartCoroutine(Delay(_shotGunDelay));
            return;
        }
        StartCoroutine(Delay(_shotDelay));
    }

    [SerializeField] float _range;

    IEnumerator ShotLine()
    {
        Debug.Log("발사");
        GameObject shotLine = Instantiate(_linePrefab.gameObject);
        LineRenderer line = shotLine.GetComponent<LineRenderer>();

        line.SetPosition(0, _shotPos.position);
        Vector3 endPos = _shotPos.position + _shotPos.forward * _gunRange;
        endPos.x += Random.Range(-_range, _range);
        endPos.y += Random.Range(-_range, _range);
        line.SetPosition(1, endPos);

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
