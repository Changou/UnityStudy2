using System.Collections;
using System.Collections.Generic;
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

public class UI_Manager : MonoBehaviour
{
    [SerializeField] public TYPE _currentType;

    public ARFaceManager _arfaceManager;

    [SerializeField] Dropdown _dropType;
    [SerializeField] Dropdown _dropDir;

    public void OnToggle_Mask(int num)
    {
        foreach (ARFace face in _arfaceManager.trackables)
        {
            if (face.trackingState == TrackingState.Tracking)
            {
                if (_currentType == TYPE.EARRING)
                {

                }
                else
                {
                    if (face.transform.GetChild((int)_currentType).GetChild(num).gameObject.activeSelf)
                    {
                        face.transform.GetChild((int)_currentType).GetChild(num).gameObject.SetActive(false);
                        return;
                    }

                    AllHide(face.transform.GetChild((int)_currentType));
                    face.transform.GetChild((int)_currentType).GetChild(num).gameObject.SetActive(true);
                }
            }
        }
    }

    void AllHide(Transform obj)
    {
        for(int i = 0;i< obj.childCount;i++)
        {
            obj.GetChild(i).gameObject.SetActive(false);
        }
    }
}
