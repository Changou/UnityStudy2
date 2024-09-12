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
        UIManager._Inst.Only_Show(UIManager.UI.OVER);
    }

    void GameClear()
    {
        _isGameOver = true;
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
