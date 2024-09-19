using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
{
    [SerializeField] float _time;
    [SerializeField] float _prevTime;
    Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _time = GameManager._Inst._targetTime;
        
        _text.text = _time.ToString("00") + " : 00";
        _time *= 60f;
        _prevTime = _time;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager._Inst._IsGameOver) return;

       CountDown();
    }
    void CountDown()
    {
        _time -= 1 * Time.deltaTime;

        if (_time <= 0)
        {
            GameManager._Inst.GameOver();
        }
        if (_prevTime - _time >= 30)
        {
            GameManager._Inst.DownDelay();
            _prevTime = _time;
        }

        int min = (int)(_time / 60);
        int sec = (int)(_time % 60);

        _text.text = min.ToString("00") + " : " + sec.ToString("00");
    }
}
