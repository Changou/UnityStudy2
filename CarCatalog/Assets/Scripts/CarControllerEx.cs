using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerEx : CarController
{
    //----------------------------------
    [Header("회전 속도"), SerializeField]
    protected float _rotSpeed = 0.1f;
    //----------------------------------
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //  터치 상태가 움직이는 중..
            if (touch.phase == TouchPhase.Moved)
            {
                //  카메라 위치 --> 카메라 정면 방향으로 가상의 선 발사..
                Ray ray = new Ray(Camera.main.transform.position,
                                    Camera.main.transform.forward);

                RaycastHit hitInfo;
                //  가상의 선에 충돌한 오브젝트의 레이어가
                //  "MyCar"인지 체크..
                //  -   https://dallcom-forever2620.tistory.com/18

                int layerMask = 1 << LayerMask.NameToLayer("MyCar");
                Debug.Log(layerMask);
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
                {
                    //  터치 이동량 구하기..
                    //  -   좌측 방향   :   음수..
                    //      우측 방향   :   양수..
                    Vector3 deltaPos = touch.deltaPosition;

                    //  로컬 Y 축 방향으로 회전..
                    //  -   우회전 방향  :   음수..
                    //      좌회전 방향  :   양수..
                    //  -   드래그 방향과 반대임..
                    transform.Rotate(
                        transform.up,
                        deltaPos.x * -1f * _rotSpeed);

                }// if ( Physics.Raycast( ray, out hitInfo, Mathf.Infinity, 1 << 6 ))

            }// if( touch.phase == TouchPhase.Moved )

        }// if (Input.touchCount > 0)

    }
}
