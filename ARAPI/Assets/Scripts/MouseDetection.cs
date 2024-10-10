using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARCore;
using UnityEngine.XR.ARFoundation;

public class MouseDetection : MonoBehaviour
{
    public ARFaceManager _arFaceManager;

    public GameObject _cubePref;

    List<GameObject> _faceCubes = new List<GameObject>();

    ARCoreFaceSubsystem _subSys;

    NativeArray<ARCoreFaceRegionData> _regionData;

    [Header("�� ���ؽ� �ε���"), SerializeField] int[] _index;

    GameObject _mouseMiddle;

    [Header("��ƼŬ"), SerializeField] GameObject _particle;

    enum MOUSE
    {
        A,
        I,
        O,
        UM,

        MAX
    }

    MOUSE _mouse = MOUSE.UM;

    [Header("���� ��ƼŬ"), SerializeField] EmotionParticle _ep;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < _index.Length; i++)
        {
            GameObject go = Instantiate(_cubePref);
            _faceCubes.Add(go);
            go.SetActive(false);
        }

        _mouseMiddle = Instantiate(_particle);
        _mouseMiddle.SetActive(false);

        for (int i = 0; i < _ep._IndexCnt; i++)
        {
            GameObject go = Instantiate(_cubePref);
            _ep.AddParticleObj(go);
        }

        _arFaceManager.facesChanged += OnDetectFaceAll;

        _subSys = (ARCoreFaceSubsystem)_arFaceManager.subsystem;
    }

    [Header("UI"), SerializeField] UI_Manager2 _ui;

    void OnDetectFaceAll(ARFacesChangedEventArgs args)
    {
        //  �� �ν�..
        if (args.updated.Count > 0)
        {
            _mouseMiddle.SetActive(true);
            for (int i = 0; i < _faceCubes.Count; i++)
            {
                Vector3 vertPos = args.updated[0].vertices[_index[i]];

                vertPos = args.updated[0].transform.TransformPoint(vertPos);

                //_faceCubes[i].SetActive(true);
                _faceCubes[i].transform.position = vertPos;
            }

            int num = 0;
            for (int i = 0; i < _ep._emotions.Count; i++) 
            {
                for (int j = 0; j < _ep._emotions[i].Length; j++)
                {
                    Vector3 vertPos = args.updated[0].vertices[_ep._emotions[i][j]];

                    vertPos = args.updated[0].transform.TransformPoint(vertPos);

                    _ep._particleOBJ[num++].transform.position = vertPos;
                }
            }
            
            float udDist = Vector3.Distance(_faceCubes[0].transform.position, _faceCubes[1].transform.position) * 100;
            float lrDist = Vector3.Distance(_faceCubes[2].transform.position, _faceCubes[3].transform.position) * 100;

            //Debug.Log(" �� ���� �Ÿ� :" + udDist);
            //Debug.Log(" �� �¿� �Ÿ� :" + lrDist);

            MouseShape(udDist, lrDist);
            ParticleOn(_mouse);

            Vector3 midPos = new Vector3(_faceCubes[0].transform.position.x,
                                        _faceCubes[0].transform.position.y - (udDist / 200),
                                        _faceCubes[0].transform.position.z);

            _mouseMiddle.transform.position = midPos;
        }
        //  �� �ν� ����..
        else if (args.removed.Count > 0)
        {
            //  ť�� ��Ȱ��ȭ..
            for(int i = 0;i < _faceCubes.Count; i++)
                _faceCubes[i].SetActive(false);
            _mouseMiddle.SetActive(false);
            _ui.SetText("None");
        }
    }

    void ParticleOn(MOUSE mouse)
    {
        for(int i = 0;i<_mouseMiddle.transform.childCount;i++)
        {
            if(i == (int)mouse)
            {
                _mouseMiddle.transform.GetChild(i).gameObject.SetActive(true);
                continue;
            }
            _mouseMiddle.transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    void MouseShape(float ud, float lr)
    {
        if (lr > 4 && ud > 3)
        {
            _ui.SetText("��");
            _mouse = MOUSE.A;
        }
        else if (lr > 5 && (ud > 0.5f && ud < 2f))
        {
            _ui.SetText("��");
            _mouse = MOUSE.I;
        }
        else if ((lr < 4 && lr > 3) && ud < 1f)
        {
            _ui.SetText("��");
            _mouse = MOUSE.O;
        }
        else if (lr > 3 && ud < 0.2f)
        {
            _ui.SetText("��");
            _mouse = MOUSE.UM;
        }
    }
}
