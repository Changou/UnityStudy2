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

    [SerializeField] GameObject[] _anoterUI;
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
        SetCtrlUI(false);
        Only_Show(UI.START);
    }

    public void Only_Show(UI ui)
    {
        AllHide();
        _uis[(int)ui].SetActive(true);
    }

    void AllHide()
    {
        foreach(GameObject ui in _uis)
        {
            ui.SetActive(false);
        }
    }

    public void OverUI(UI ui)
    {
        SetCtrlUI(false);
        Only_Show(ui);
    }

    public void SetCtrlUI(bool isOn)
    {
        foreach (GameObject aui in _anoterUI)
        {
            aui.SetActive(isOn);
        }
    }

    [SerializeField] SliderHP _hp;
    public void SetHP()
    {
        _hp.SetHP();
    }

}
