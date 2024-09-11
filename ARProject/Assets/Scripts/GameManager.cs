using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    int _score;

    [SerializeField] Text _scoreT;

    private void Start()
    {
        _score = 0;
        _scoreT.text = _score.ToString("00");
    }

    public void ScoreUp()
    {
        _scoreT.text = (++_score).ToString("00");
    }
}
