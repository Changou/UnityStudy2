using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Manager2 : MonoBehaviour
{
    [SerializeField] Text _text;
    
    public void SetText(string text)
    {
        _text.text = text;
    }
}
