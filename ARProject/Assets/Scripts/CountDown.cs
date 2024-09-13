using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] Text _timeText;
    public float _time;
    public float _defalutTime;

    private void Awake()
    {
        GameManager._Inst._GameStart += () => { StartCoroutine(TimeCount()); };
    }

    private void Start()
    {
        _timeText.text = _time.ToString("00") + " : 00";
       
        _time = Mathf.Round(_time);

        _time *= 60f;
        _defalutTime = _time;
    }

    IEnumerator TimeCount()
    {
        while (!GameManager._Inst._isGameOver)
        {
            _time -= 1 * Time.deltaTime;
            int min = (int)(_time / 60);
            int sec = (int)(_time % 60);

            _timeText.text = min.ToString("00") + " : " + sec.ToString("00");

            if(_time <= 0)
            {
                GameManager._Inst.GameOver();
                break;
            }
            yield return null;
        }
    }
}
