using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    [Header("ÃÑ ¼ÂÆÃ")]
    [SerializeField] GameObject[] _gun;
    [SerializeField] GameObject[] _scope;
    [SerializeField] Transform _gunPos;

    // Start is called before the first frame update
    void Start()
    {
        SettingGun();
    }

    void SettingGun()
    {
        int gunIndex = PlayerPrefs.GetInt("Gun");

        Color32 color = new Color32((byte)PlayerPrefs.GetFloat("ColorR")
            , (byte)PlayerPrefs.GetFloat("ColorG"), (byte)PlayerPrefs.GetFloat("ColorB"), (byte)PlayerPrefs.GetFloat("ColorA"));

        int scopeIndex = PlayerPrefs.GetInt("Scope");

        GameObject gun = Instantiate(_gun[gunIndex], _gunPos);

        gun.GetComponent<Gun>().ChanedColor(color);

        if(scopeIndex != 0)
        {
            GameObject scope = Instantiate(_scope[scopeIndex - 1], gun.transform.Find("ScopePosition"));
        }
    }
    
}
