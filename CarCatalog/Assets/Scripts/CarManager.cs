using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CarManager : MonoBehaviour
{
    //---------------------------
    public GameObject _indicator;                   //  표식으로 사용할 게임 오브젝트..
    protected ARRaycastManager _arRaycastManager;   //  AR 전용 레이캐스트..
    //---------------------------
    protected virtual void Start()
    {
        _indicator.SetActive(false);        //  표식 비활성화..

        _arRaycastManager = GetComponent<ARRaycastManager>();   //  ARRaycastManager 참조..
    }
    //---------------------------
    protected virtual void Update() { DetectGround(); }
    //---------------------------
    //  바닥 감지...
    protected virtual void DetectGround()
    {
        //  화면의 중앙 지점 계산..
        Vector2 screenCenter
            = new Vector2(
                Screen.width * 0.5f,
                Screen.height * 0.5f);

        //  레이에 검출된 대상들의
        //  정보를 관리..
        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();
        //List<ARRaycastHit> hitInfos = new();

        /*
         * -------------------------------------------------------------
         *  ARRaycastHit의 주요 멤버변수..
         * -------------------------------------------------------------
         *  -   distance    :   [ float ]..
         *      -   레이를 발사한 위치
         *          ~ 감지 대상까지 거리..
         *      -   월드 공간을
         *          기준으로 측정..
         *                      
         *  -   hitType     :   [ TrackableType ]..
         *      -   레이에 감지된
         *          대상의 타입..
         *      -   주요 타입..
         *          -   Plane   :   평면..
         *              Face    :   얼굴 타입..
         *              All     :   모든 타입..
         *                       
         *  -   pose    :   [ Pose ]..
         *      -   위치와 회전에 대한 값을
         *          관리하는 변수( 월드 좌표.. )
         *                      
         *  -   trackableId :   [ TrackableId ]..
         *      -   레이에 감지된 대상의
         *          ID를 관리하는 변수..
         *                      
         *  -   sessionRelativePose :   [ Pose ]..
         *      -   위치와 회전에 대한 값을
         *          관리하는 변수( 로컬 좌표.. )
         *
         */


        //  화면의 중앙으로부터
        //  레이를 발사하여
        //  [ TrackableType.Planes ] 타입의
        //  대상이 있는지 체크..
        //  -   감지 대상은 바닥...
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
                -   screenPoint     :   레이를 발사할
                                        화면 픽셀 좌표..

                -   hitResults      :   레이에 감지된
                                        대상 정보를
                                        담을 구조체 리스트..
                                        ( ARRaycastHit ) 

                -   trackableTypes  :   감지할 대상의 타입.. 
                                        기본값은 모든 대상..
            */

            _indicator.SetActive(true);
            //  표식 오브젝트의
            //  위치 및 회전 값을
            //  레이가 닿은 지점에
            //  일치시킴..
            _indicator.transform.position = hitInfos[0].pose.position;
            _indicator.transform.rotation = hitInfos[0].pose.rotation;

            /*
             *  -   레이에 감지된
             *      대상의 갯수에 따라
             *      [ hitInfos ]는
             *      2개 이상일 수도 있음..
             *      
             *  -   현재 레이에 감지될 대상은
             *      Plane 타입 1개뿐이므로
             *      첫번째 대상인 [ hitInfos ]의
             *      0번 인덱스 요소에 저장된
             *      정보를 활용..
             *      
             *  -   표식 오브젝트는
             *      위치와 회전값을 이용해서
             *      세팅 해야하므로
             *      [ pose ]정보를 활용..
             *
             *  -   [ Pose ] 구조체의 주요 멤버변수..
             *      -   position    :   [ Vector3 ]
             *          -   레이가 닿은
             *              지점의 위치..
             *      -   rotation    :   [ Quaternion ]
             *          -   레이가 닿은
             *              지점의 회전값..
             *      -   forward     :   [ Vector3 ]
             *          -   레이가 닿은
             *              지점의 정면 방향 벡터..
             *      -   right       :   [ Vector3 ]
             *          -   레이가 닿은
             *              지점의 우측방향 벡터..
             *      -   up          :   [ Vector3 ]
             *          -   레이가 닿은
             *              지점의 위 방향 벡터..
             */
        }
        else
            _indicator.SetActive(false);

    }
}
