using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManagerEx2 : CarManagerEx
{
    public GameObject _myCarPref;               //  자동차 프리팹..
    protected GameObject _instancedCar = null;  //  자동차 프리팹을 이용하여
                                                //  생성된 자동차 게임 오브젝트..
                                                //-------------------------------------------
    protected override void Start()
    {
        _instancedCar = null;
        base.Start();
    }
    //-------------------------------------------
    protected override void Update()
    {
        base.Update();

        //  [ _indicator ]가
        //  활성화 상태일 때 화면을
        //  터치하면 자동차 모델링이 생성..

        //  [ _indicator ]가 활성화 &&
        //  화면에 터치한 상태인지 체크..
        if (_indicator.activeInHierarchy && Input.touchCount > 0)
        {
            //  첫번째 터치 정보를 가져옴..
            Touch touch = Input.GetTouch(0);

            //  터치가 시작된 상태인지 체크..
            /*  [ 참고 : https://docs.unity3d.com/kr/2020.3/ScriptReference/TouchPhase.html ]
                
                Began..
                -   손가락이 화면에
                    닿은 순간..

                Moved..
                -   손가락이 화면위에서
                    움직이는 상태..

                Stationary..
                -   손가락이 화면을 터치했지만
                    움직임은 없는 상태..

                Ended..
                -   손가락이 화면으로부터
                    벗어난 상태..
                    -   터치의 마지막..

                Canceled..
                -   시스템이 터치의 추적을
                    취소한 상태..
                    예)  사용자가 자신의 얼굴에
                         장치를 가져간 경우,
                         터치영역이 5개 이상 발생,
                         전화 수신 등..
            */
            if (touch.phase == TouchPhase.Began)
            {
                //  _myCar를 _indicator의
                //  위치, 회전값을 참고하여 생성..

                //  자동차 생성 여부에 체크..                
                if (_instancedCar == null)
                {
                    //  생성된 자동차가 없다면
                    //  새로 생성..
                    _instancedCar = Instantiate(
                        _myCarPref,
                        _indicator.transform.position,
                        _indicator.transform.rotation);

                }// if (_instancedCar == null)                
                else
                {
                    //  생성된 자동차가 있다면
                    //  _indicator의 트랜스폼에 따라 갱신..
                    _instancedCar.transform.SetPositionAndRotation(
                        _indicator.transform.position,
                        _indicator.transform.rotation);

                }// ~if (_instancedCar == null)

            }// if( touch.phase == TouchPhase.Began )

        }// if ( _indicator.activeInHierarchy && Input.touchCount > 0)

    }
}
