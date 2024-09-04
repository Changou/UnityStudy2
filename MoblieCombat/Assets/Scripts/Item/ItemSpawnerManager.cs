using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerManager : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabs;
    [SerializeField] GameObject _prefabCoin;
    [SerializeField] float _spawnDelay;
    [SerializeField] float _spawnCoinDelay;
    [SerializeField] JetPlane _jet;
    [SerializeField] float _minOffset;
    [SerializeField] float _maxOffset;

    private void OnEnable()
    {
        StartCoroutine(SpawnItem());
        StartCoroutine(SpawnCoin());
    }

    Vector3 RandomPos()
    {
        float x = Random.Range(-_jet._mMX, _jet._mMX);
        float y = Random.Range(_jet._minY, _jet._maxY);
        float z = _jet.transform.position.z + Random.Range(_minOffset, _maxOffset);

        return new Vector3(x, y, z);
    }

    IEnumerator SpawnItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);
            GameObject item = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)]);

            item.transform.position = RandomPos();
            yield return null;
        }
    }

    IEnumerator SpawnCoin()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnCoinDelay);
            GameObject item = Instantiate(_prefabCoin);

            item.transform.position = RandomPos();
            yield return null;
        }
    }

}
