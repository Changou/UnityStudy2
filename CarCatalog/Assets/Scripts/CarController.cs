using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    //-------------------------------------
    public GameObject[] _carBody;   //  차 외관 오브젝트..
    public Color32[] _colors;       //  자동차에 적용할 색..
    //-------------------------------------
    Material[] _carMats;            //  차 외관 머터리얼..
    //-------------------------------------
    void Start()
    {
        //  차 외관의 수만큼 초기화..
        _carMats = new Material[_carBody.Length];

        //  차 외관의 머터리얼을
        //  _carMats에 참조..
        for (int count = 0; count < _carMats.Length; ++count)
            _carMats[count] = _carBody[count].GetComponent<MeshRenderer>().material;

        //  0번째 인덱스의
        //  색상 배열에는
        //  머터리얼의
        //  초기 색을 설정..
        _colors[0] = _carMats[0].color;

    }// void Start()
    //-------------------------------------
    public void ChangeColor(int num)
    {
        //  LOD 머터리얼의 색상을
        //  버튼에 지정된 색상으로 변경..
        for (int idx = 0; idx < _carMats.Length; ++idx)
            _carMats[idx].color = _colors[num];

    }// public void ChangeColor( int num )
    //-------------------------------------
}
