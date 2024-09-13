using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    int _score;

    public bool _isGameOver;

    [Header("타임어택")]
    [SerializeField] CountDown _countDown;

    [Header("목표갯수")]
    [SerializeField] int _targetScore;
    [SerializeField] Text _scoreT;
    [SerializeField] Text _targetT;

    public Action _GameStart;

    private void Start()
    {
        _targetScore = StageArchive._Inst._targetScore;
        _isGameOver = false;
        _score = 0;
        _scoreT.text = _score.ToString("00");
        _targetT.text = "/ "+ _targetScore.ToString("00");
        _countDown._time = StageArchive._Inst._targetTime;
    }

    public void GameStartBtnOn()
    {
        _GameStart();
    }

    public void ScoreUp()
    {
        _scoreT.text = (++_score).ToString("00");
        if(_score == _targetScore)
        {
            GameClear();
        }
    }

    public void GameOver()
    {
        _isGameOver = true;

        _fire.Explosion();
        UIManager._Inst.Only_Show(UIManager.UI.OVER);
    }

    [SerializeField] Text _resultText;
    [SerializeField] FireWorkManager _fire;
    void GameClear()
    {
        _isGameOver = true;
        
        float result = _countDown._defalutTime - Mathf.Floor(_countDown._time);
        _resultText.text = ((int)(result / 60)).ToString("00") + " : "
            + ((int)(result % 60)).ToString("00");

        _fire.StartFireWork();
        UIManager._Inst.Only_Show(UIManager.UI.CLEAR);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextGame()
    {
        StageArchive._Inst.StageUp();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
