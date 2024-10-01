using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public enum TYPE
{
    IMAGE,
    MOVIE,

    MAX
}

public class UI_Manager : MonoBehaviour
{
    [SerializeField] FaceChanger _faceChanger;

    

    public void SetFaceMateiral(int num)
    {
        SetActiveObject(num);

        _faceChanger._matType = (TYPE)num;

        _faceChanger.ChangeFace(_faceChanger._mediaDrop[num].value);
    }

    public void SetActiveObject(int num)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i == num)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
