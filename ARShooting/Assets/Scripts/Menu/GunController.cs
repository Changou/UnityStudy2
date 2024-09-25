using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum GUN
{
    PISTOL,
    RIFLE,
    SHOTGUN,

    MAX
}

public class GunController : MonoBehaviour
{
    [SerializeField] GUN _gunType;

    public GameObject[] _gunBody;
    public Color32[] _colors;

    Material[] _gunMats;

    int _colorIndex = 0;
    int _scopeIndex = 0;

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
        _colorIndex = num;
        for(int idx = 0; idx<_gunMats.Length; ++idx)
        {
            _gunMats[idx].color = _colors[num];
        }
    }

    [Header("회전 속도"), SerializeField]
    float _rotSpeed = 0.1f;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
                RaycastHit hitInfo;

                int layerMask = 1 << LayerMask.NameToLayer("Gun");
        
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, layerMask))
                {
                    Vector3 deltaPos = touch.deltaPosition;
                    transform.Rotate(Vector3.up, deltaPos.x * -1f * _rotSpeed);
                }
            }
        }
    }

    [Header("스코프"), SerializeField]
    GameObject[] _prefabsScope;
    [SerializeField] Transform _ScopePos;

    public void SetScope(int num)
    {
        if(_ScopePos.childCount != 0)
        {
            for(int i = 0; i< _ScopePos.childCount; i++)
            {
                Destroy(_ScopePos.GetChild(i).gameObject);
            }
        }

        _scopeIndex = num;

        if (num == 0) return;

        GameObject scope = Instantiate(_prefabsScope[num - 1], _ScopePos);
    }

    public void SelectGun()
    {
        //총기 저장
        PlayerPrefs.SetInt("Gun", (int)_gunType);

        //컬러 저장
        PlayerPrefs.SetFloat("ColorR", _colors[_colorIndex].r);
        PlayerPrefs.SetFloat("ColorG", _colors[_colorIndex].g);
        PlayerPrefs.SetFloat("ColorB", _colors[_colorIndex].b);
        PlayerPrefs.SetFloat("ColorA", _colors[_colorIndex].a);

        //스코프 저장
        PlayerPrefs.SetInt("Scope", _scopeIndex);

        SceneManager.LoadScene(1);
    }
}
