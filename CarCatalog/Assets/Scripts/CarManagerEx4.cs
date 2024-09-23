using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarManagerEx4 : CarManagerEx3
{
    //---------------------------------
    public float _relocationDist = 1f;  //  최소 이동 범위..
    //---------------------------------
    protected override void Update()
    {
        DetectGround();

        //  현재 클릭( 터치 )한 오브젝트가
        //  [ UI 오브젝트 ]라면
        //  [ Update 함수 ] 종료..
        if (EventSystem.current.currentSelectedGameObject)
            return;

        if (_indicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (_instancedCar == null)
                    _instancedCar = Instantiate(
                        _myCarPref,
                        _indicator.transform.position,
                        _indicator.transform.rotation);
                else
                {
                    //  생성된 오브젝트와
                    //  [ Indicator ]오브젝트의
                    //  거리가 최소 이동 범위( 1m )를
                    //  초과했는지 체크..
                    if (Vector3.Distance(
                         _instancedCar.transform.position,
                         _indicator.transform.position)
                         > _relocationDist)
                    {
                        //  _instancedCar의 위치, 회전정보 갱신..
                        _instancedCar.transform.SetPositionAndRotation(
                            _indicator.transform.position,
                            _indicator.transform.rotation);
                    }

                }// ~if(_instancedCar == null)
            }

        }// if ( _indicator.activeInHierarchy && Input.touchCount > 0)

    }
}
