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

    [SerializeField] ParticleManager _particle;

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
        _particle.ParticleOn(ParticleManager.TYPE.START);
        _startGame();
    }

    [SerializeField] SpawnManager _spawn;

    public void DownDelay()
    {
        _spawn._bombDelay -= 0.5f;
    }

    public void GameClear()
    {
        _IsGameOver = true;
        _particle.ParticleOn(ParticleManager.TYPE.CLEAR);
        _UI.Show_Only(UIManager.UI.CLEAR);
    }

    public void GameOver()
    {
        _IsGameOver = true;
        _particle.ParticleOn(ParticleManager.TYPE.OVER);
        _UI.Show_Only(UIManager.UI.OVER);
    }
}
