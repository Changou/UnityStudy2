using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorkManager : MonoBehaviour
{
    [SerializeField] GameObject[] _fireWordPrefabs;
    [SerializeField] Vector3 _offset;

    public void StartFireWork()
    {
        StartCoroutine(FireWork());
    }

    IEnumerator FireWork()
    {
        for (int i = 0; i < 3; i++)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject fire = Instantiate(_fireWordPrefabs[Random.Range(0, _fireWordPrefabs.Length)], Camera.main.transform);
            _offset.x = -(2 - (i * 2));
            fire.transform.localPosition = _offset;
            Destroy(fire, 3f);
            yield return null;
        }
    }

    [SerializeField] GameObject _ExPrefab;
    public void Explosion()
    {
        GameObject explosion = Instantiate(_ExPrefab, Camera.main.transform);
        _offset.y = 0;
        explosion.transform.localPosition = _offset;
    }
}
