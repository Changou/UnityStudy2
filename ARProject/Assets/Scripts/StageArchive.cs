using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageArchive : MonoBehaviour
{
    public static StageArchive _Inst;

    private void Awake()
    {
        var obj = FindObjectsOfType<StageArchive>();
        if(obj.Length == 1)
        {
            _Inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    [SerializeField] public int _targetScore;
    [SerializeField] public float _targetTime;
    [SerializeField] public int _stageInfo = 1;

    public void StageUp()
    {
        ++_stageInfo;
        _targetTime = _targetTime <= 1 ? _targetTime : _targetTime - 0.5f;
    }
}
