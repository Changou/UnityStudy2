using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum TYPE
{
    GLASSES,
    BEARD,
    EARRING,

    MAX
}

public enum EAR
{
    RIGHT,LEFT, MAX
}

public class UI_Manager : MonoBehaviour
{
    [SerializeField] public TYPE _currentType;
    [SerializeField] public EAR _currentEar;

    public ARFaceManager _arfaceManager;

    [Header("파티클"), SerializeField] ParticleSystem[] _particles;
    [SerializeField] float _particleOffset;

    private void Start()
    {
        SetFaceMateiral((int)FACE_TYPE.IMAGE);
    }

    public void OnToggle_Mask(int num)
    {
        Transform pos = transform;
        foreach (ARFace face in _arfaceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
            {
                pos = face.transform;
                Transform accTrans = face.transform.GetChild((int)_currentType);

                if (_currentType == TYPE.EARRING)
                {
                    if (accTrans.GetChild((int)_currentEar).GetChild(num).gameObject.activeSelf)
                    {
                        accTrans.GetChild(num).gameObject.SetActive(false);
                        return;
                    }

                    AllHide(accTrans.GetChild((int)_currentEar));
                    accTrans.GetChild((int)_currentEar).GetChild(num).gameObject.SetActive(true);
                }
                else
                {
                    if (accTrans.GetChild(num).gameObject.activeSelf)
                    {
                        accTrans.GetChild(num).gameObject.SetActive(false);
                        return;
                    }

                    AllHide(accTrans);
                    accTrans.GetChild(num).gameObject.SetActive(true);
                }
            }
        }
        ParticleOn(pos);
    }

    [Header("얼굴 교체")]
    [SerializeField] FaceChanger _faceChanger;
    [SerializeField] Transform _face;

    public void SetFaceMateiral(int num)
    {
        SetActiveObject(num);

        _faceChanger._matType = (FACE_TYPE)num;

        _faceChanger.ChangeFace(_faceChanger._mediaDrop[num].value);
    }

    [SerializeField] AccessManager _accManager; 

    public void SetClear()
    {
        foreach (ARFace face in _arfaceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
            {
                for(int i = 0; i < face.transform.childCount; i++)
                {
                    AllHide(face.transform.GetChild(i));
                }
            }
        }
        _accManager.ResetMenu();

        foreach(Dropdown drop in _faceChanger._mediaDrop)
        {
            drop.value = 0;
        }

        SetFaceMateiral((int)FACE_TYPE.IMAGE);
    }

    public void SetActiveObject(int num)
    {
        for (int i = 0; i < _face.childCount; i++)
        {
            if (i == num)
            {
                _face.GetChild(i).gameObject.SetActive(true);
                continue;
            }
            _face.GetChild(i).gameObject.SetActive(false);
        }
    }


    public void ParticleOn(Transform position)
    {
        GameObject paritcle =
            Instantiate(_particles[Random.Range(0, _particles.Length)].gameObject, position);
        Vector3 pos = Vector3.zero;
        pos.z = _particleOffset;
        paritcle.transform.localPosition = pos;

        Vector3 scale = new Vector3(0.1f, 0.1f, 0.1f);
        paritcle.transform.localScale = scale;
    }

    void AllHide(Transform obj)
    {
        for(int i = 0;i< obj.childCount;i++)
        {
            obj.GetChild(i).gameObject.SetActive(false);
        }
    }
}
