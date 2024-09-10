using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    public float _time;
    Text _timeText;

    private void Awake()
    {
        _timeText = GetComponent<Text>();
    }

    private void Start()
    {
        _timeText.text = _time.ToString("00") + " : 00";
        _time *= 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._Inst._isGameOver) return;

        CountDown();
    }

    void CountDown()
    {
        _time -= 1 * Time.deltaTime;

        int min = (int)(_time / 60);
        int sec = (int)(_time % 60);

        _timeText.text = min.ToString("00") + " : " + sec.ToString("00");

        if (_time <= 0)
            GameManager._Inst.GameOver();
    }
}
