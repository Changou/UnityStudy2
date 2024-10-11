using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    Text _timeText;

    float _time;


    private void Awake()
    {
        _timeText = GetComponent<Text>();
    }

    private void Start()
    {
        _time = GameManager._Inst._TimeLimit;
        _timeText.text = _time.ToString("00") + " : 00";
        _time *= 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._Inst._IsGameOver) return;

        TimeCountDown();
    }

    void TimeCountDown()
    {
        _time -= Time.deltaTime;
        int min = (int)(_time / 60f);
        int sec = (int)(_time % 60f);

        _timeText.text = min.ToString("00") + " : " + sec.ToString("00");
        if(_time <= 0)
        {
            GameManager._Inst.GameOver(false);
        }
    }
}
