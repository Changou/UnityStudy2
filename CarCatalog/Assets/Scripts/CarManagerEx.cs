using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarManagerEx : CarManager
{
    protected override void DetectGround()
    {
        //  ȭ���� �߾� ���� ���..
        Vector2 screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        //  ���̿� �����
        //  ������ ����..
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();


        //  ȭ���� �߾����κ���
        //  ���̸� �߻��Ͽ�
        //  [ TrackableType.Planes ] Ÿ����
        //  ����� �ִ��� üũ..
        //  -   ���� ����� �ٴ�...
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
