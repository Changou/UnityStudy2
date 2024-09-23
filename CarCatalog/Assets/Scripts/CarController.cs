using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //-------------------------------------
    public GameObject[] _carBody;   //  �� �ܰ� ������Ʈ..
    public Color32[] _colors;       //  �ڵ����� ������ ��..
    //-------------------------------------
    Material[] _carMats;            //  �� �ܰ� ���͸���..
    //-------------------------------------
    void Start()
    {
        //  �� �ܰ��� ����ŭ �ʱ�ȭ..
        _carMats = new Material[_carBody.Length];

        //  �� �ܰ��� ���͸�����
        //  _carMats�� ����..
        for (int count = 0; count < _carMats.Length; ++count)
            _carMats[count] = _carBody[count].GetComponent<MeshRenderer>().material;

        //  0��° �ε�����
        //  ���� �迭����
        //  ���͸�����
        //  �ʱ� ���� ����..
        _colors[0] = _carMats[0].color;

    }// void Start()
    //-------------------------------------
    public void ChangeColor(int num)
    {
        //  LOD ���͸����� ������
        //  ��ư�� ������ �������� ����..
        for (int idx = 0; idx < _carMats.Length; ++idx)
            _carMats[idx].color = _colors[num];

    }// public void ChangeColor( int num )
    //-------------------------------------
}
