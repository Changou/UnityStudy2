using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject[] _gunBody;
    public Color32[] _colors;

    Material[] _gunMats;

    // Start is called before the first frame update
    void Start()
    {
        _gunMats = new Material[_gunBody.Length];

        for(int i = 0; i<_gunMats.Length; ++i)
        {
            _gunMats[i] = _gunBody[i].GetComponent<MeshRenderer>().material;
        }

        _colors[0] = _gunMats[0].color;
    }

    public void ChangeColor(int num)
    {
        for(int idx = 0; idx<_gunMats.Length; ++idx)
        {
            _gunMats[idx].color = _colors[num];
        }
    }
}
