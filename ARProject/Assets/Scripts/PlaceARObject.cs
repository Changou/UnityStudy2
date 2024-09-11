//==========================================================================
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;  //  ARRaycastManager
using UnityEngine.XR.ARSubsystems;  //  TrackableType
//==========================================================================
public class PlaceARObject : MonoBehaviour
{
    //-------------------------------
    [Header("����ĳ��Ʈ �Ŵ���"), SerializeField]
    protected ARRaycastManager _arRaycastManager;
    //-------------------------------
    [Header("����ĳ��Ʈ�� �����ϴ� AR ī�޶�"), SerializeField]
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
                //  -   3D �������� ��ġ�� ȸ������
                //      �Բ� �����ϴ� ����ü..
                //  -   �ַ� XR ���ø����̼��� ����ϴ�
                //      ����̽��� 3D�����󿡼�
                //      ���� ��ġ�� ȸ������
                //      ǥ���ϴµ� ���..
                var placementsPose = hits[0].pose;

                //  ȸ�� ���..
                var camForward = _cam.transform.forward;
                camForward.y = 0;
                camForward.Normalize();
                placementsPose.rotation = Quaternion.LookRotation(camForward);

                //  ��ġ ���..
                var newPose = placementsPose.position;
                newPose.y += 0.001f;

                //  �ε������Ϳ�
                //  ��ġ�� ȸ���� ����..
                transform.SetPositionAndRotation(newPose, placementsPose.rotation);

            }// if( hits.Count > 0 )

        }// if(_arRaycastManager.Raycast(screenPos, hits))

    }// void UpdateIndicator( Vector2 screenPos )
    //-------------------------------

}