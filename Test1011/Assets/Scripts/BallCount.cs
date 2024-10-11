using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BallCount : MonoBehaviour
{
    Text _countText;
    int _count;
    int _target;
    private void Awake()
    {
        _countText = GetComponent<Text>();
    }

    private void Start()
    {
        _target = GameManager._Inst._TargetCount;
        _count = 0;
        _countText.text = _target.ToString("00") + " / "+ _count.ToString("00");
    }

    public void BallCountUp()
    {
        _count++;
        _countText.text = _target.ToString("00") + " / " + _count.ToString("00");
        if (_count >= _target) GameManager._Inst.GameOver(true);
    }
}
