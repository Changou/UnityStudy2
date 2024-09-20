using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("»õ")]
    [SerializeField] GameObject[] _prefabs;

    [Header("ÆøÅº")]
    [SerializeField] GameObject _prefab;
    [SerializeField] public float _bombDelay;
    [SerializeField] float _bombPosX;
    [SerializeField] float _bombPosY;
    [SerializeField] float _bombPosZ;

    [Header("½ºÆùÁöÁ¤(»õ)")]
    [SerializeField] int _posX;
    [SerializeField] float _posY;
    [SerializeField] float _posZ;
    [SerializeField] float _delay;

    // Start is called before the first frame update
    void Start()
    {
        GameManager._Inst._startGame += () =>
        {
            StartCoroutine(SpawnBird());
            StartCoroutine(SpawnBomb());
        };
    }

    IEnumerator SpawnBomb()
    {
        while (!GameManager._Inst._IsGameOver)
        {
            GameObject bomb = Instantiate(_prefab);
            Vector3 pos = new Vector3(
                Random.Range(-_bombPosX, _bombPosX), _bombPosY, Random.Range(_bombPosZ, _bombPosZ + 1));
            bomb.transform.position = pos;

            yield return new WaitForSeconds(_bombDelay);

            yield return null;
        }
    }

    IEnumerator SpawnBird()
    {
        while(!GameManager._Inst._IsGameOver)
        {
            GameObject bird = Instantiate(_prefabs[Random.Range(0, _prefabs.Length)]);

            int posX = Random.Range(0, 2);
            posX = posX > 0 ? _posX : -_posX;

            if (posX > 0)
                bird.transform.rotation = Quaternion.Euler(0, -90, 0);
            else
                bird.transform.rotation = Quaternion.Euler(0, 90, 0);

            float posY = Random.Range(-_posY, _posY);

            bird.transform.position = new Vector3(posX, posY, _posZ);
            yield return new WaitForSeconds(_delay);

            yield return null;
        }
    }
}
