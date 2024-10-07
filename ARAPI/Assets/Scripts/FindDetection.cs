using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class FindDetection : MonoBehaviour
{
    public ARFaceManager _arFaceManager;

    //  얼굴의 특정부분을
    //  시각적으로 표시할때
    //  참조할 프리팹..
    public GameObject _cubePref;

    //  얼굴에 적용할 큐브 리스트..
    List<GameObject> _faceCubes = new List<GameObject>();
    //----------------------------------
    /*
        [ ARCoreFaceSubsystem ]

        -   얼굴의 특정부위에 대한
            데이터 관리..

            ->  [ ARFaceManager.subsystem ]를
                [ ARCoreFaceSubsystem ]으로
                캐스팅 하여 사용..

        [ ARFaceManager.subsystem ]

        -   [ AR Foundation API ]
            > [ XRFaceSubsystem ] 타입의 변수..

        -   디바이스의 카메라 장치와
            [ ARFaceManager ]사이의
            데이터 처리나 명령등을
            지원하는 함수 제공..
    */
    ARCoreFaceSubsystem _subSys;
    //----------------------------------    
    /*
        [ NativeArray ]
        -   [Job System]에서 사용하는
            ( 유니티의 멀티스레드 시스템 )
            특수한 형태의 콜렉션 변수..
    */
    //  얼굴을 인식하면
    //  특정부위에 대한
    //  정보를 관리할 변수..
    NativeArray<ARCoreFaceRegionData> _regionData;

    [SerializeField]
    UI_Manager _uiManager;

    void Start()
    {
        //  위치를 표시하기 위한
        //  큐브 3개 생성..
        for (int i = 0; i < 3; ++i)
        {
            GameObject go = Instantiate(_cubePref);
            _faceCubes.Add(go);
            go.SetActive(false);
        }

        /*  이벤트 등록..
         *  
         *  public event Action<ARFacesChangedEventArgs> facesChanged;
         *  [ 참고 ㅣ https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/api/UnityEngine.XR.ARFoundation.ARFaceManager.html ]
         *  -   [ AR Camera ]가
         *      사용자의 얼굴을
         *      인식할 때마다
         *      실행되는 Action 대리자..
         *      
         *  ARFacesChangedEventArgs
         *  [ 참고 : https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/api/UnityEngine.XR.ARFoundation.ARFacesChangedEventArgs.html ]
         *  -   [ AR Camera ]로 인식된
         *      얼굴에 대한 여러가지
         *      데이터 값 관리..
         *  
         *      -   [ updated ]
         *      
         *          -   얼굴의 변화를 감지하면
         *              인식된 얼굴 데이터가
         *              추가되는 리스트..
         *          
         *      -   [ removed ]
         *      
         *          -   얼굴 추적을 놓치면
         *              ( 예 : 얼굴이 AR 카메라 촬영 범위
         *                     밖으로 나가는 경우.. )
         *              updated에 추가되었던
         *              얼굴 데이터가 제거되고
         *              제거된 데이터가
         *              추가되는 리스트..
         *              
         *          -   현재 [ AR Core ]에서
         *              동시에 인식할 수 있는
         *              얼굴의 수가
         *              1개뿐이므로 [ updated ]나
         *              [ removed ]에는
         *              1개의 값만 있을 것..
         *              
         *              --> update.Count > 0
         *                  ->  현재 추적중인
         *                      얼굴이 있음..
         *                  
         *                  removed.Count > 0
         *                  ->  현재 얼굴이
         *                      추적되지 않고 있음..
        */
        _arFaceManager.facesChanged += OnDetectFaceAll;

        /*
         *      [ AR Foundation API ]
         *      > [ XRFaceSubsystem ] 타입의 변수를
         *      [ AR Core ]
         *      > [ ARCoreFaceSubsystem ]으로
         *      캐스팅하여 참조..
        */
        _subSys = (ARCoreFaceSubsystem)_arFaceManager.subsystem;

    }
    void OnDetectThreePts(ARFacesChangedEventArgs args)
    {
        //  얼굴 정보의 갱신을 인식..
        if (args.updated.Count > 0)
        {
            /*
                void ARCoreFaceSubsystem.GetRegionPoses(

                    //  추적된 얼굴의 ID
                    TrackableId trackableId,
                    
                    //  NativeArray 배열의
                    //  값 할당 방식..
                    Allocator allocator,
                    -   Allocator.Temp
                        -   가장 빠른 할당..
                        -   수명이 1프레임 이하인
                            할당에 적합..

                        Allocator.TempJob
                        -   Temp보다는 느리지만
                            Persistent보다는
                            빠른 할당..
                        -   권장 타입..

                        Allocator.Persistent
                        -   가장 느린 할당이지만,
                            애플리케이션의
                            전체 주기에 걸쳐
                            필요한 만큼
                            오래 지속됨..
                        -   성능이 중요한 상황에서는
                            비추..
                        
                    //  얼굴 부위 정보를
                    //  저장할 배열..
                    ref NativeArray<ARCoreFaceRegionData> regions                    
                );
                -   인식된 얼굴에서
                    특정 부분의 정보를 가져오는 함수..
            */
            _subSys.GetRegionPoses(
                args.updated[0].trackableId,
                Allocator.TempJob,
                ref _regionData);


            //  인식된 얼굴의
            //  특정 위치에 오브젝트 배치..
            //  0   :   코끝
            //  1   :   이마 좌측..
            //  2   :   이마 우측..
            for (int i = 0; i < _regionData.Length; ++i)
            {
                _faceCubes[i].transform.position =
                    _regionData[i].pose.position;

                _faceCubes[i].transform.rotation =
                    _regionData[i].pose.rotation;

                _faceCubes[i].SetActive(true);

            }// for( int i = 0; i < _regionData.Length; ++i )

        }// if( args.updated.Count > 0 )

        //  추적중이던
        //  얼굴 정보를 소실..
        else if (args.removed.Count > 0)
        {
            //  오브젝트 비활성화..
            for (int i = 0; i < _regionData.Length; ++i)
                _faceCubes[i].SetActive(false);

        }// else if( args.removed.Count > 0 )

    }
    void OnDetectFaceAll(ARFacesChangedEventArgs args)
    {
        //  얼굴 인식..
        if (args.updated.Count > 0)
        {
            int num = _uiManager._VertNum;
            //  얼굴 정점 배열에서
            //  특정 인덱스에 해당하는
            //  ( 100 )
            //  좌표를 참조..
            Vector3 vertPos = args.updated[0].vertices[num];

            //  정점 좌표는
            //  [ AR Face 오브젝트 ]를 기준으로
            //  로컬 좌표로 설정되어있음..
            //  ->  다른 게임 오브젝트에
            //      적용할 때는 월드좌표로
            //      변환해야할 수도 있음..
            vertPos = args.updated[0].transform.TransformPoint(vertPos);

            //  큐브 하나를 활성화,
            //  vertPos의 위치에 세팅..
            _faceCubes[0].SetActive(true);
            _faceCubes[0].transform.position = vertPos;
        }
        //  얼굴 인식 실패..
        else if (args.removed.Count > 0)
        {
            //  큐브 비활성화..
            _faceCubes[0].SetActive(false);
        }

    }
}
