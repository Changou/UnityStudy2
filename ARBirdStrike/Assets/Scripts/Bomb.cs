using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour, IEntity
{
    [SerializeField] ParticleSystem _particle;

    private void Update()
    {
        CheckAlive();
    }

    void CheckAlive()
    {
        if (transform.position.y < -3f)
        {
            Destroy(gameObject);
        }
    }

    public void Catch(Vector3 pos)
    {
        GameObject explosion = Instantiate(_particle.gameObject);
        explosion.transform.position = pos;

        Bird[] everyObj = GameObject.FindObjectsByType<Bird>(FindObjectsSortMode.None);
        for (int i = 0; i < everyObj.Length; i++)
        {
            Destroy(everyObj[i].gameObject);
        }
        GameManager._Inst.GameOver();

        Destroy(gameObject);
    }
}
