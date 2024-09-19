using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    public bool _IsGameOver;

    public Action _startGame;

    [SerializeField] UIManager _UI;

    [Header("∏Ò«•")]
    [SerializeField] public float _targetTime;
    [SerializeField] public int _targetBird;
    [SerializeField] BirdCount _birdCount;

    // Start is called before the first frame update
    void Start()
    {
        _IsGameOver = true;
    }

    public void CatchBird()
    {
        _birdCount.CatchTheBird();
    }

    public void StartBtn()
    {
        _IsGameOver = false;
        _UI.Show_Only(UIManager.UI.GAME);
        _startGame();
    }

    public void DownDelay()
    {

    }

    public void GameClear()
    {
        _IsGameOver = true;
        _UI.Show_Only(UIManager.UI.CLEAR);
    }

    public void GameOver()
    {
        _IsGameOver = true;
        _UI.Show_Only(UIManager.UI.OVER);
    }
}
