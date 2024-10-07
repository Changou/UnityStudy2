using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class FindDetection : MonoBehaviour
{
    public ARFaceManager _arFaceManager;

    //  ���� Ư���κ���
    //  �ð������� ǥ���Ҷ�
    //  ������ ������..
    public GameObject _cubePref;

    //  �󱼿� ������ ť�� ����Ʈ..
    List<GameObject> _faceCubes = new List<GameObject>();
    //----------------------------------
    /*
        [ ARCoreFaceSubsystem ]

        -   ���� Ư�������� ����
            ������ ����..

            ->  [ ARFaceManager.subsystem ]��
                [ ARCoreFaceSubsystem ]����
                ĳ���� �Ͽ� ���..

        [ ARFaceManager.subsystem ]

        -   [ AR Foundation API ]
            > [ XRFaceSubsystem ] Ÿ���� ����..

        -   ����̽��� ī�޶� ��ġ��
            [ ARFaceManager ]������
            ������ ó���� ��ɵ���
            �����ϴ� �Լ� ����..
    */
    ARCoreFaceSubsystem _subSys;
    //----------------------------------    
    /*
        [ NativeArray ]
        -   [Job System]���� ����ϴ�
            ( ����Ƽ�� ��Ƽ������ �ý��� )
            Ư���� ������ �ݷ��� ����..
    */
    //  ���� �ν��ϸ�
    //  Ư�������� ����
    //  ������ ������ ����..
    NativeArray<ARCoreFaceRegionData> _regionData;

    [SerializeField]
    UI_Manager _uiManager;

    void Start()
    {
        //  ��ġ�� ǥ���ϱ� ����
        //  ť�� 3�� ����..
        for (int i = 0; i < 3; ++i)
        {
            GameObject go = Instantiate(_cubePref);
            _faceCubes.Add(go);
            go.SetActive(false);
        }

        /*  �̺�Ʈ ���..
         *  
         *  public event Action<ARFacesChangedEventArgs> facesChanged;
         *  [ ���� �� https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/api/UnityEngine.XR.ARFoundation.ARFaceManager.html ]
         *  -   [ AR Camera ]��
         *      ������� ����
         *      �ν��� ������
         *      ����Ǵ� Action �븮��..
         *      
         *  ARFacesChangedEventArgs
         *  [ ���� : https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@4.0/api/UnityEngine.XR.ARFoundation.ARFacesChangedEventArgs.html ]
         *  -   [ AR Camera ]�� �νĵ�
         *      �󱼿� ���� ��������
         *      ������ �� ����..
         *  
         *      -   [ updated ]
         *      
         *          -   ���� ��ȭ�� �����ϸ�
         *              �νĵ� �� �����Ͱ�
         *              �߰��Ǵ� ����Ʈ..
         *          
         *      -   [ removed ]
         *      
         *          -   �� ������ ��ġ��
         *              ( �� : ���� AR ī�޶� �Կ� ����
         *                     ������ ������ ���.. )
         *              updated�� �߰��Ǿ���
         *              �� �����Ͱ� ���ŵǰ�
         *              ���ŵ� �����Ͱ�
         *              �߰��Ǵ� ����Ʈ..
         *              
         *          -   ���� [ AR Core ]����
         *              ���ÿ� �ν��� �� �ִ�
         *              ���� ����
         *              1�����̹Ƿ� [ updated ]��
         *              [ removed ]����
         *              1���� ���� ���� ��..
         *              
         *              --> update.Count > 0
         *                  ->  ���� ��������
         *                      ���� ����..
         *                  
         *                  removed.Count > 0
         *                  ->  ���� ����
         *                      �������� �ʰ� ����..
        */
        _arFaceManager.facesChanged += OnDetectFaceAll;

        /*
         *      [ AR Foundation API ]
         *      > [ XRFaceSubsystem ] Ÿ���� ������
         *      [ AR Core ]
         *      > [ ARCoreFaceSubsystem ]����
         *      ĳ�����Ͽ� ����..
        */
        _subSys = (ARCoreFaceSubsystem)_arFaceManager.subsystem;

    }
    void OnDetectThreePts(ARFacesChangedEventArgs args)
    {
        //  �� ������ ������ �ν�..
        if (args.updated.Count > 0)
        {
            /*
                void ARCoreFaceSubsystem.GetRegionPoses(

                    //  ������ ���� ID
                    TrackableId trackableId,
                    
                    //  NativeArray �迭��
                    //  �� �Ҵ� ���..
                    Allocator allocator,
                    -   Allocator.Temp
                        -   ���� ���� �Ҵ�..
                        -   ������ 1������ ������
                            �Ҵ翡 ����..

                        Allocator.TempJob
                        -   Temp���ٴ� ��������
                            Persistent���ٴ�
                            ���� �Ҵ�..
                        -   ���� Ÿ��..

                        Allocator.Persistent
                        -   ���� ���� �Ҵ�������,
                            ���ø����̼���
                            ��ü �ֱ⿡ ����
                            �ʿ��� ��ŭ
                            ���� ���ӵ�..
                        -   ������ �߿��� ��Ȳ������
                            ����..
                        
                    //  �� ���� ������
                    //  ������ �迭..
                    ref NativeArray<ARCoreFaceRegionData> regions                    
                );
                -   �νĵ� �󱼿���
                    Ư�� �κ��� ������ �������� �Լ�..
            */
            _subSys.GetRegionPoses(
                args.updated[0].trackableId,
                Allocator.TempJob,
                ref _regionData);


            //  �νĵ� ����
            //  Ư�� ��ġ�� ������Ʈ ��ġ..
            //  0   :   �ڳ�
            //  1   :   �̸� ����..
            //  2   :   �̸� ����..
            for (int i = 0; i < _regionData.Length; ++i)
            {
                _faceCubes[i].transform.position =
                    _regionData[i].pose.position;

                _faceCubes[i].transform.rotation =
                    _regionData[i].pose.rotation;

                _faceCubes[i].SetActive(true);

            }// for( int i = 0; i < _regionData.Length; ++i )

        }// if( args.updated.Count > 0 )

        //  �������̴�
        //  �� ������ �ҽ�..
        else if (args.removed.Count > 0)
        {
            //  ������Ʈ ��Ȱ��ȭ..
            for (int i = 0; i < _regionData.Length; ++i)
                _faceCubes[i].SetActive(false);

        }// else if( args.removed.Count > 0 )

    }
    void OnDetectFaceAll(ARFacesChangedEventArgs args)
    {
        //  �� �ν�..
        if (args.updated.Count > 0)
        {
            int num = _uiManager._VertNum;
            //  �� ���� �迭����
            //  Ư�� �ε����� �ش��ϴ�
            //  ( 100 )
            //  ��ǥ�� ����..
            Vector3 vertPos = args.updated[0].vertices[num];

            //  ���� ��ǥ��
            //  [ AR Face ������Ʈ ]�� ��������
            //  ���� ��ǥ�� �����Ǿ�����..
            //  ->  �ٸ� ���� ������Ʈ��
            //      ������ ���� ������ǥ��
            //      ��ȯ�ؾ��� ���� ����..
            vertPos = args.updated[0].transform.TransformPoint(vertPos);

            //  ť�� �ϳ��� Ȱ��ȭ,
            //  vertPos�� ��ġ�� ����..
            _faceCubes[0].SetActive(true);
            _faceCubes[0].transform.position = vertPos;
        }
        //  �� �ν� ����..
        else if (args.removed.Count > 0)
        {
            //  ť�� ��Ȱ��ȭ..
            _faceCubes[0].SetActive(false);
        }

    }
}
