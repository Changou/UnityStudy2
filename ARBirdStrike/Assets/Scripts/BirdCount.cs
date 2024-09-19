using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdCount : MonoBehaviour
{
    [SerializeField] int _birdCnt = 0;

    Text _text;

    private void Awake()
    {
        _text = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _birdCnt = 0;
        _text.text = _birdCnt.ToString("00") + " / " + GameManager._Inst._targetBird.ToString("00");
    }

    public void CatchTheBird()
    {
        _text.text = (++_birdCnt).ToString("00") + " / " + GameManager._Inst._targetBird.ToString("00");
        if(_birdCnt >= GameManager._Inst._targetBird)
        {
            GameManager._Inst.GameClear();
        }
    }
}
