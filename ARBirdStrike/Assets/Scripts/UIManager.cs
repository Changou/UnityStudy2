using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject[] _uis;

    public enum UI
    {
        START,
        GAME,
        CLEAR,
        OVER,

        MAX
    }

    // Start is called before the first frame update
    void Start()
    {
        Show_Only(UI.START);
    }

    public void Show_Only(UI ui)
    {
        All_Hide();
        _uis[(int)ui].SetActive(true);
    }

    public void All_Hide()
    {
        foreach (GameObject ui in _uis)
        {
            ui.SetActive(false);
        }
    }
}
