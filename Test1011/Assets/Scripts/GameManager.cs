using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    public bool _IsGameOver;

    [Header("Á¦ÇÑ½Ã°£(ºÐ)"), SerializeField] int _timeLimit;
    [Header("¸ñÇ¥ °ñ °¹¼ö"), SerializeField] int _targetCount;

    public int _TimeLimit => _timeLimit;
    public int _TargetCount => _targetCount;

    // Start is called before the first frame update
    void Start()
    {
        _IsGameOver = true;
    }

    public void GameStart()
    {
        _IsGameOver = false;
        UIManager._Inst.SetActiveUI(UIManager.UI.START, false);
    }

    public void GameOver(bool isClear)
    {
        _IsGameOver = true;
        UIManager._Inst.SetClearUI(isClear);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
