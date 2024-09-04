using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    public bool _isGameStart = false;

    private void Awake()
    {
        _Inst = this; 
    }

    public void StartGame()
    {
        _isGameStart = true;
    }
}
