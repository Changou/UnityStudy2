//==========================================================================
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;  //  ARRaycastManager
using UnityEngine.XR.ARSubsystems;  //  TrackableType
//==========================================================================
public class PlaceARObject : MonoBehaviour
{
    //-------------------------------
    [Header("레이캐스트 매니져"), SerializeField]
    protected ARRaycastManager _arRaycastManager;
    //-------------------------------
    [Header("레이캐스트를 시작하는 AR 카메라"), SerializeField]
    protected Camera _cam;
    //-------------------------------
    protected virtual void LateUpdate()
    {
        Vector2 viewportCenter = new Vector2(0.5f, 0.5f);
        var screenCenter = _cam.ViewportToScreenPoint(viewportCenter);
        UpdateIndicator(screenCenter);
    }
    //-------------------------------
    void UpdateIndicator(Vector2 screenPos)
    {
        var hits = new List<ARRaycastHit>();

        if (_arRaycastManager.Raycast(screenPos, hits, TrackableType.Planes))
        {
            if (hits.Count > 0)
            {
                //  Pose..
                //  -   3D 공간에서 위치와 회전값을
                //      함께 관리하는 구조체..
                //  -   주로 XR 어플리케이션을 사용하는
                //      디바이스의 3D공간상에서
                //      현재 위치와 회전값을
                //      표현하는데 사용..
                var placementsPose = hits[0].pose;

                //  회전 계산..
                var camForward = _cam.transform.forward;
                camForward.y = 0;
                camForward.Normalize();
                placementsPose.rotation = Quaternion.LookRotation(camForward);

                //  위치 계산..
                var newPose = placementsPose.position;
                newPose.y += 0.001f;

                //  인디케이터에
                //  위치와 회전을 적용..
                transform.SetPositionAndRotation(newPose, placementsPose.rotation);

            }// if( hits.Count > 0 )

        }// if(_arRaycastManager.Raycast(screenPos, hits))

    }// void UpdateIndicator( Vector2 screenPos )
    //-------------------------------

}