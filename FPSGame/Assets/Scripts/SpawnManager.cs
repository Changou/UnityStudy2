using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] Transform[] _spawnPoints;
    [SerializeField] GameObject _prepabs;
    [SerializeField] float _spawnDelay;


    // Start is called before the first frame update
    void Start()
    {
        GameManager._Inst._GameStart += () => { StartCoroutine(Spawn()); };
    }

    IEnumerator Spawn()
    {
        while (!GameManager._Inst._isGameOver)
        {
            yield return new WaitForSeconds(_spawnDelay);

            GameObject monster = Instantiate(_prepabs);
            _prepabs.transform.position = _spawnPoints[Random.Range(0, _spawnPoints.Length)].position;

            yield return null;
        }
    }
}
