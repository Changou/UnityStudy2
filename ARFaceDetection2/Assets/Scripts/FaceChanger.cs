using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class FaceChanger : MonoBehaviour
{
    [SerializeField] Material[] _mats;

    public ARFaceManager _arfaceManager;

    [SerializeField] public Dropdown[] _mediaDrop;

    public TYPE _matType = TYPE.IMAGE;

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
        foreach(ARFace face in _arfaceManager.trackables)
        {
            if(face.trackingState == TrackingState.Tracking)
            {
                if(_matType == TYPE.IMAGE) 
                    face.GetComponent<Renderer>().material = _mats[num];
                else
                {
                    face.GetComponent<Renderer>().material = _videoMat;
                    _vPlayer.clip = _videos[num];
                }
            }
        }
    }

    [Header("영상 클립")]
    [SerializeField] VideoClip[] _videos;
    [SerializeField] VideoPlayer _vPlayer;
    [SerializeField] Material _videoMat;
}
