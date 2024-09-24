using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerEx : CarController
{
    //----------------------------------
    [Header("ȸ�� �ӵ�"), SerializeField]
    protected float _rotSpeed = 0.1f;
    //----------------------------------
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //  ��ġ ���°� �����̴� ��..
            if (touch.phase == TouchPhase.Moved)
            {
                //  ī�޶� ��ġ --> ī�޶� ���� �������� ������ �� �߻�..
                Ray ray = new Ray(Camera.main.transform.position,
                                    Camera.main.transform.forward);

                RaycastHit hitInfo;
                //  ������ ���� �浹�� ������Ʈ�� ���̾
                //  "MyCar"���� üũ..
                //  -   https://dallcom-forever2620.tistory.com/18

                int layerMask = 1 << LayerMask.NameToLayer("MyCar");
                Debug.Log(layerMask);
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
                {
                    //  ��ġ �̵��� ���ϱ�..
                    //  -   ���� ����   :   ����..
                    //      ���� ����   :   ���..
                    Vector3 deltaPos = touch.deltaPosition;

                    //  ���� Y �� �������� ȸ��..
                    //  -   ��ȸ�� ����  :   ����..
                    //      ��ȸ�� ����  :   ���..
                    //  -   �巡�� ����� �ݴ���..
                    transform.Rotate(
                        transform.up,
                        deltaPos.x * -1f * _rotSpeed);

                }// if ( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << 6 ))

            }// if( touch.phase == TouchPhase.Moved )

        }// if (Input.touchCount > 0)

    }
}
