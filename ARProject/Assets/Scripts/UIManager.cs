using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    [SerializeField] GameObject[] _uis;

    public enum UI
    {
        START,
        GAME,
        CLEAR,
        OVER,

        MAX
    }

    private void Start()
    {
        Only_Show(UI.START);
    }

    public void Only_Show(UI ui)
    {
        All_Hide();
        _uis[(int)ui].SetActive(true);
    }

    public void All_Hide()
    {
        foreach(GameObject ui in _uis)
        {
            ui.SetActive(false);
        }   
    }
}
