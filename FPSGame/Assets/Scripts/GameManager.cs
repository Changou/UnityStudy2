using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    public bool _isGameOver;

    public Action _GameStart;

    [SerializeField] public Transform _startPos;

    public void Awake()
    {
        _isGameOver = true;
        _Inst = this;
    }

    [Header("제한시간"), SerializeField] float _time;
    [SerializeField] TimeCount _timeC;

    // Start is called before the first frame update
    void Start()
    {
        _timeC._time = _time;
    }

    public void GameStart()
    {
        _isGameOver = false;
        UIManager._Inst.SetCtrlUI(true);
        UIManager._Inst.Only_Show(UIManager.UI.GAME);
        _GameStart();
    }

    public void GameOver()
    {
        Pause(true);
        UIManager._Inst.OverUI(UIManager.UI.OVER);
    }

    public void GameClear()
    {
        Pause(true);
        UIManager._Inst.OverUI(UIManager.UI.CLEAR);
    }

    private void Pause(bool isOn)
    {
        if (isOn) Time.timeScale = 0;
        else Time.timeScale = 1;
    }
}
