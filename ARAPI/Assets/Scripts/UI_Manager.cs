using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

public class UI_Manager : MonoBehaviour
{
    //--------------------------------------
    public ARFaceManager _arfaceManager;
    //--------------------------------------
    //  얼굴에 적용할 머터리얼..
    //  -   [ 에디터 ]
    //      > [ 인스펙터 ]에서 직접 링크..
    //--------------------------------------
    //  인덱스를 관리할 텍스트 UI..
    [SerializeField]
    Text _textIndex;

    //  현재 얼굴 메쉬의
    //  버텍스 인덱스..
    int _vertNum = 0;
    public int _VertNum => _vertNum;

    //  얼굴 메쉬의
    //  총 정점수..
    const int VERT_COUNT_MAX = 468;
    //--------------------------------------
    //  인덱스 텍스트 UI
    //  초기화..
    void Start() { _textIndex.text = _vertNum.ToString(); }

    //  인덱스 증가 후
    //  텍스트 갱신..
    public void AddIndex()
    {
        _vertNum = Mathf.Min(++_vertNum, VERT_COUNT_MAX - 1);
        _textIndex.text = _vertNum.ToString();
    }

    //  인덱스 차감 후
    //  텍스트 갱신..
    public void SubIndex()
    {
        _vertNum = Mathf.Max(--_vertNum, 0);
        _textIndex.text = _vertNum.ToString();
    }
    //--------------------------------------
}
