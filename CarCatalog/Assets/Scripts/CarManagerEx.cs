using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarManagerEx : CarManager
{
    protected override void DetectGround()
    {
        //  화면의 중앙 지점 계산..
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        //  레이에 검출된
        //  대상들의 정보..
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();


        //  화면의 중앙으로부터
        //  레이를 발사하여
        //  [ TrackableType.Planes ] 타입의
        //  대상이 있는지 체크..
        //  -   감지 대상은 바닥...
        if (_arRaycastManager.Raycast(screenCenter, hitInfos, TrackableType.Planes))
        {
            _indicator.SetActive(true);

            _indicator.transform.position = hitInfos[0].pose.position;
            _indicator.transform.rotation = hitInfos[0].pose.rotation;

            _indicator.transform.position += _indicator.transform.up * 0.1f;

        }// if (_arRaycastManager.Raycast(screenCenter, hitInfos, TrackableType.Planes))
        else
            _indicator.SetActive(false);

    }
}
