using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject[] _gunBody;

    Material[] _gunMats;

    public void ChanedColor(Color32 color)
    {
        _gunMats = new Material[_gunBody.Length];

        for (int i = 0; i < _gunMats.Length; ++i)
        {
            _gunMats[i] = _gunBody[i].GetComponent<MeshRenderer>().material;
        }

        for (int idx = 0; idx < _gunMats.Length; ++idx)
        {
            _gunMats[idx].color = color;
        }
    }
}
