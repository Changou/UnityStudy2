using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] GameObject[] _prefabs;
    [SerializeField] int _maxObstacleCnt;
    [SerializeField] JetPlane _jet;

    [Header("스폰 포인트")]
    [SerializeField] float _offsetSpawnPos;
    [SerializeField]

    enum OBSTACLETYPE
    {
        PLANE,
        JET1,
        JET2,
        OLIRIG,

        MAX
    }

    public void SpawnObstalce()
    {
        float posZ = transform.localScale.z * 10;
        int obstacleCnt = 0;
        float lastobstaclePosZ = transform.position.z - (posZ / 2);
        while(_maxObstacleCnt > obstacleCnt)
        {
            int index = Random.Range(0, _prefabs.Length);
            GameObject obstacle = Instantiate(_prefabs[index]);
            float y = Random.Range(_jet._minY, _jet._maxY);
            if (index == (int)OBSTACLETYPE.OLIRIG) y = 0;
            float x = Random.Range(-_jet._mMX, _jet._mMX);

            float startPosZ = lastobstaclePosZ + _offsetSpawnPos;
            float z = Random.Range(startPosZ, startPosZ + _offsetSpawnPos);
            obstacle.transform.position = new Vector3(x, y, z);
            if(z + _offsetSpawnPos >= transform.position.z + (posZ / 2))
            {
                break;
            }
            lastobstaclePosZ = z;

            obstacleCnt++;
        }
    }
}
