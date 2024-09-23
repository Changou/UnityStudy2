using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarManager : MonoBehaviour
{
    //---------------------------
    public GameObject _indicator;                   //  ǥ������ ����� ���� ������Ʈ..
    protected ARRaycastManager _arRaycastManager;   //  AR ���� ����ĳ��Ʈ..
    //---------------------------
    protected virtual void Start()
    {
        _indicator.SetActive(false);        //  ǥ�� ��Ȱ��ȭ..

        _arRaycastManager = GetComponent<ARRaycastManager>();   //  ARRaycastManager ����..
    }
    //---------------------------
    protected virtual void Update() { DetectGround(); }
    //---------------------------
    //  �ٴ� ����...
    protected virtual void DetectGround()
    {
        //  ȭ���� �߾� ���� ���..
        Vector2 screenCenter
            = new Vector2(
                Screen.width * 0.5f,
                Screen.height * 0.5f);

        //  ���̿� ����� ������
        //  ������ ����..
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();
        //List<ARRaycastHit> hitInfos = new();

        /*
         * -------------------------------------------------------------
         *  ARRaycastHit�� �ֿ� �������..
         * -------------------------------------------------------------
         *  -   distance    :   [ float ]..
         *      -   ���̸� �߻��� ��ġ
         *          ~ ���� ������ �Ÿ�..
         *      -   ���� ������
         *          �������� ����..
         *                      
         *  -   hitType     :   [ TrackableType ]..
         *      -   ���̿� ������
         *          ����� Ÿ��..
         *      -   �ֿ� Ÿ��..
         *          -   Plane   :   ���..
         *              Face    :   �� Ÿ��..
         *              All     :   ��� Ÿ��..
         *                       
         *  -   pose    :   [ Pose ]..
         *      -   ��ġ�� ȸ���� ���� ����
         *          �����ϴ� ����( ���� ��ǥ.. )
         *                      
         *  -   trackableId :   [ TrackableId ]..
         *      -   ���̿� ������ �����
         *          ID�� �����ϴ� ����..
         *                      
         *  -   sessionRelativePose :   [ Pose ]..
         *      -   ��ġ�� ȸ���� ���� ����
         *          �����ϴ� ����( ���� ��ǥ.. )
         *
         */


        //  ȭ���� �߾����κ���
        //  ���̸� �߻��Ͽ�
        //  [ TrackableType.Planes ] Ÿ����
        //  ����� �ִ��� üũ..
        //  -   ���� ����� �ٴ�...
        if (_arRaycastManager.Raycast(screenCenter, hitInfos, TrackableType.Planes))
        {
            /*
                -------------------------------------------------------------
                public bool Raycast(
                            Vector2 screenPoint,
                            List<ARRaycastHit> hitResults,
                            TrackableType trackableTypes = TrackableType.All
                            )
                -------------------------------------------------------------        
                -   screenPoint     :   ���̸� �߻���
                                        ȭ�� �ȼ� ��ǥ..

                -   hitResults      :   ���̿� ������
                                        ��� ������
                                        ���� ����ü ����Ʈ..
                                        ( ARRaycastHit ) 

                -   trackableTypes  :   ������ ����� Ÿ��.. 
                                        �⺻���� ��� ���..
            */

            _indicator.SetActive(true);
            //  ǥ�� ������Ʈ��
            //  ��ġ �� ȸ�� ����
            //  ���̰� ���� ������
            //  ��ġ��Ŵ..
            _indicator.transform.position = hitInfos[0].pose.position;
            _indicator.transform.rotation = hitInfos[0].pose.rotation;

            /*
             *  -   ���̿� ������
             *      ����� ������ ����
             *      [ hitInfos ]��
             *      2�� �̻��� ���� ����..
             *      
             *  -   ���� ���̿� ������ �����
             *      Plane Ÿ�� 1�����̹Ƿ�
             *      ù��° ����� [ hitInfos ]��
             *      0�� �ε��� ��ҿ� �����
             *      ������ Ȱ��..
             *      
             *  -   ǥ�� ������Ʈ��
             *      ��ġ�� ȸ������ �̿��ؼ�
             *      ���� �ؾ��ϹǷ�
             *      [ pose ]������ Ȱ��..
             *
             *  -   [ Pose ] ����ü�� �ֿ� �������..
             *      -   position    :   [ Vector3 ]
             *          -   ���̰� ����
             *              ������ ��ġ..
             *      -   rotation    :   [ Quaternion ]
             *          -   ���̰� ����
             *              ������ ȸ����..
             *      -   forward     :   [ Vector3 ]
             *          -   ���̰� ����
             *              ������ ���� ���� ����..
             *      -   right       :   [ Vector3 ]
             *          -   ���̰� ����
             *              ������ �������� ����..
             *      -   up          :   [ Vector3 ]
             *          -   ���̰� ����
             *              ������ �� ���� ����..
             */
        }
        else
            _indicator.SetActive(false);

    }
}
