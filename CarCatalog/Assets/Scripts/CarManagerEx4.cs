using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CarManagerEx4 : CarManagerEx3
{
    //---------------------------------
    public float _relocationDist = 1f;  //  �ּ� �̵� ����..
    //---------------------------------
    protected override void Update()
    {
        DetectGround();

        //  ���� Ŭ��( ��ġ )�� ������Ʈ��
        //  [ UI ������Ʈ ]���
        //  [ Update �Լ� ] ����..
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
                    //  ������ ������Ʈ��
                    //  [ Indicator ]������Ʈ��
                    //  �Ÿ��� �ּ� �̵� ����( 1m )��
                    //  �ʰ��ߴ��� üũ..
                    if (Vector3.Distance(
                         _instancedCar.transform.position,
                         _indicator.transform.position)
                         > _relocationDist)
                    {
                        //  _instancedCar�� ��ġ, ȸ������ ����..
                        _instancedCar.transform.SetPositionAndRotation(
                            _indicator.transform.position,
                            _indicator.transform.rotation);
                    }

                }// ~if(_instancedCar == null)
            }

        }// if ( _indicator.activeInHierarchy && Input.touchCount > 0)

    }
}
