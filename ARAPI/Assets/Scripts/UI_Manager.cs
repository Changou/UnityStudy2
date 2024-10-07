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
    //  �󱼿� ������ ���͸���..
    //  -   [ ������ ]
    //      > [ �ν����� ]���� ���� ��ũ..
    //--------------------------------------
    //  �ε����� ������ �ؽ�Ʈ UI..
    [SerializeField]
    Text _textIndex;

    //  ���� �� �޽���
    //  ���ؽ� �ε���..
    int _vertNum = 0;
    public int _VertNum => _vertNum;

    //  �� �޽���
    //  �� ������..
    const int VERT_COUNT_MAX = 468;
    //--------------------------------------
    //  �ε��� �ؽ�Ʈ UI
    //  �ʱ�ȭ..
    void Start() { _textIndex.text = _vertNum.ToString(); }

    //  �ε��� ���� ��
    //  �ؽ�Ʈ ����..
    public void AddIndex()
    {
        _vertNum = Mathf.Min(++_vertNum, VERT_COUNT_MAX - 1);
        _textIndex.text = _vertNum.ToString();
    }

    //  �ε��� ���� ��
    //  �ؽ�Ʈ ����..
    public void SubIndex()
    {
        _vertNum = Mathf.Max(--_vertNum, 0);
        _textIndex.text = _vertNum.ToString();
    }
    //--------------------------------------
}
