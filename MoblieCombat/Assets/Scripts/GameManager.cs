using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    public bool _isGameOver = true;

    public Action StartGameFunc;

    [Header("인게임 정보")]
    [SerializeField] int _coin = 0;
    [SerializeField] JetPlane _jet;
    [SerializeField] public float _targetDist;
    [SerializeField] float _defaultTargetDist;

    public float _startJetPos;

    private void Awake()
    {
        _isGameOver = true;
        _Inst = this; 
    }

    private void Start()
    {
        if (DataCentral._Inst._isGameClear)
        {
            _jet.GetComponent<DOTweenAnimation>().DOPlay();
            UIManager._Inst.AllHide();
        }
        _targetDist = _defaultTargetDist + (500 * (DataCentral._Inst._lv - 1));
        
        UIManager._Inst.FadeIn();
    }

    public void GameClear()
    {
        _isGameOver = true;
        DataCentral._Inst._isGameClear = true;
        UIManager._Inst.Show_Only(UIManager.UI.GAMECLEAR);
    }
    public void StartGame()
    {
        _isGameOver = false;
        StartGameFunc();
        _startJetPos = _jet.transform.position.z;
    }

    public void CoinUp(int up)
    {
        _coin += up;
        UIManager._Inst.UpdateCoin(_coin);
    }

    public void GameOver()
    {
        _isGameOver = true;
        DataCentral._Inst._isGameClear = false;
        UIManager._Inst.Show_Only(UIManager.UI.GAMEOVER);
    }
}
