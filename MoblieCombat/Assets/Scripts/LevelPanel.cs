using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] Text[] _lvText;

    private void OnEnable()
    {
        _lvText[0].text = "HP : " + DataCentral._Inst._hpLV + "LV";
        _lvText[1].text = "Speed : " + DataCentral._Inst._speedLV + "LV";
        _lvText[2].text = "Missile : " + DataCentral._Inst._missileLV + "LV";
    }
}
