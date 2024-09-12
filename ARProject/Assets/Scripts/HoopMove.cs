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
                _dotween.DORestartById(_DotWeenId[Random.Range(0, _DotWeenId.Length)]);
            };
        }
    }
}
