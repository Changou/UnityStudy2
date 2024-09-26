using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [Header("일반 몬스터"), SerializeField] GameObject _normal;
    [SerializeField] float _normalDelayMax;
    [SerializeField] float _normalDelayMin;
    [Header("중간보스 몬스터"), SerializeField] GameObject _midleBoss;
    [SerializeField] float _middlelDelayMax;
    [SerializeField] float _middlelDelayMin;
    [Header("보스 몬스터"), SerializeField] GameObject _boss;

    [Header("생성위치")]
    [SerializeField] float _max;
    [SerializeField] float _min;


    private void Start()
    {
        StartCoroutine(NormalSpawn());
        StartCoroutine(MiddleSpawn());

    }

    public void SpawnBoss()
    {
        StartCoroutine(Boss());
    }

    IEnumerator Boss()
    {
        yield return new WaitForSeconds(3);

        GameObject boss = Instantiate(_boss);
        boss.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 12f;
    }

    IEnumerator NormalSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomNum(_normalDelayMin, _normalDelayMax));
            GameObject normal = Instantiate(_normal);
            Vector3 pos = RandomPos();
            normal.transform.position = Camera.main.transform.position + pos;

            yield return null;
        }
    }

    IEnumerator MiddleSpawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(RandomNum(_middlelDelayMin, _middlelDelayMax));
            GameObject middle = Instantiate(_midleBoss);
            Vector3 pos = RandomPos();
            middle.transform.position = Camera.main.transform.position + pos;

            yield return null;
        }
    }

    Vector3 RandomPos()
    {
        Vector2 ranPos = Random.insideUnitCircle.normalized * Random.Range(_min, _max);

        return new Vector3(ranPos.x, 0, ranPos.y);
    }

    public float RandomNum(float min, float max)
    {
        
        return Random.Range(min, max);
    }
}
