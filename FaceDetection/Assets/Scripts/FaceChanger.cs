using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum FACE_TYPE
{
    IMAGE,
    MOVIE,

    MAX
}

public class FaceChanger : MonoBehaviour
{
    [SerializeField] Material[] _mats;

    public ARFaceManager _arfaceManager;

    [SerializeField] public Dropdown[] _mediaDrop;

    public FACE_TYPE _matType = FACE_TYPE.IMAGE;

    private void Start()
    {
        foreach (Dropdown dropdown in _mediaDrop)
        {
            dropdown.value = 0;
            dropdown.onValueChanged.AddListener(delegate { ChangeFace(dropdown.value); });
        }
    }

    public void ChangeFace(int num)
    {
        Transform pos = transform;
        foreach (ARFace face in _arfaceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
            {
                pos = face.transform;
                if (_matType == FACE_TYPE.IMAGE)
                {
                    face.GetComponent<Renderer>().material = _mats[num];
                }
                else
                {
                    if (num == 0)
                    {
                        face.GetComponent<Renderer>().material = _mats[num];
                        return;
                    }
                    face.GetComponent<Renderer>().material = _videoMat;
                    _vPlayer.clip = _videos[num - 1];
                }
            }
        }

        _ui.ParticleOn(pos);
    }

    [Header("영상 클립")]
    [SerializeField] VideoClip[] _videos;
    [SerializeField] VideoPlayer _vPlayer;
    [SerializeField] Material _videoMat;

    [SerializeField] UI_Manager _ui;
}
