using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoopMove : MonoBehaviour
{
    [SerializeField] string[] _DotWeenId;
    DOTweenAnimation _dotween;

    private void Awake()
    {
        _dotween = GetComponent<DOTweenAnimation>();

        if(StageArchive._Inst._stageInfo > 1)
        {
            GameManager._Inst._GameStart += () =>
            {
                if(Random.Range(0,2) == 0)
                    StartCoroutine(RandomPosition());
                else
                    _dotween.DORestartById(_DotWeenId[Random.Range(0, _DotWeenId.Length)]);
            };
        }
    }

    IEnumerator RandomPosition()
    {
        while (!GameManager._Inst._isGameOver)
        {

            int y = Random.Range(-5, 5);
            int x = Random.Range(-5, 5);

            int rot = Random.Range(0, 360);

            transform.position = new Vector3(x, y, 0);
            transform.rotation = Quaternion.Euler(0, rot, 0);

            yield return new WaitForSeconds(7f);

            yield return null;
        }
    }
}
