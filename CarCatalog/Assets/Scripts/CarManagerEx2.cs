using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManagerEx2 : CarManagerEx
{
    public GameObject _myCarPref;               //  �ڵ��� ������..
    protected GameObject _instancedCar = null;  //  �ڵ��� �������� �̿��Ͽ�
                                                //  ������ �ڵ��� ���� ������Ʈ..
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

        //  [ _indicator ]��
        //  Ȱ��ȭ ������ �� ȭ����
        //  ��ġ�ϸ� �ڵ��� �𵨸��� ����..

        //  [ _indicator ]�� Ȱ��ȭ &&
        //  ȭ�鿡 ��ġ�� �������� üũ..
        if (_indicator.activeInHierarchy && Input.touchCount > 0)
        {
            //  ù��° ��ġ ������ ������..
            Touch touch = Input.GetTouch(0);

            //  ��ġ�� ���۵� �������� üũ..
            /*  [ ���� : https://docs.unity3d.com/kr/2020.3/ScriptReference/TouchPhase.html ]
                
                Began..
                -   �հ����� ȭ�鿡
                    ���� ����..

                Moved..
                -   �հ����� ȭ��������
                    �����̴� ����..

                Stationary..
                -   �հ����� ȭ���� ��ġ������
                    �������� ���� ����..

                Ended..
                -   �հ����� ȭ�����κ���
                    ��� ����..
                    -   ��ġ�� ������..

                Canceled..
                -   �ý����� ��ġ�� ������
                    ����� ����..
                    ��)  ����ڰ� �ڽ��� �󱼿�
                         ��ġ�� ������ ���,
                         ��ġ������ 5�� �̻� �߻�,
                         ��ȭ ���� ��..
            */
            if (touch.phase == TouchPhase.Began)
            {
                //  _myCar�� _indicator��
                //  ��ġ, ȸ������ �����Ͽ� ����..

                //  �ڵ��� ���� ���ο� üũ..                
                if (_instancedCar == null)
                {
                    //  ������ �ڵ����� ���ٸ�
                    //  ���� ����..
                    _instancedCar = Instantiate(
                        _myCarPref,
                        _indicator.transform.position,
                        _indicator.transform.rotation);

                }// if (_instancedCar == null)                
                else
                {
                    //  ������ �ڵ����� �ִٸ�
                    //  _indicator�� Ʈ�������� ���� ����..
                    _instancedCar.transform.SetPositionAndRotation(
                        _indicator.transform.position,
                        _indicator.transform.rotation);

                }// ~if (_instancedCar == null)

            }// if( touch.phase == TouchPhase.Began )

        }// if ( _indicator.activeInHierarchy && Input.touchCount > 0)

    }
}
