using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager _Inst;

    private void Awake()
    {
        _Inst = this;
    }

    [SerializeField] Text _hpText;

    public void UpdateHP(int hp)
    {
        _hpText.text = "HP : " + hp.ToString("00");
    }
}
