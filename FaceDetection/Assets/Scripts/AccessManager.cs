using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AccessManager : MonoBehaviour
{
    [SerializeField] Dropdown _droptype;
    [SerializeField] Dropdown _dropLR;

    [SerializeField] UI_Manager _uiM;

    private void Start()
    {
        _droptype.value = 0;
        _droptype.onValueChanged.AddListener(delegate { SetAccess(_droptype.value); });

        _dropLR.value = 0;
        _dropLR.onValueChanged.AddListener(delegate { SetEarring(_dropLR.value); });
    }

    void SetEarring(int option)
    {
        _uiM._currentEar = (EAR)option;

        for(int i = 0; i < transform.GetChild((int)TYPE.EARRING).childCount; i++)
        {
            if(i == option)
            {
                transform.GetChild((int)TYPE.EARRING).GetChild(i).gameObject.SetActive(true);
                continue;
            }
            transform.GetChild((int)TYPE.EARRING).GetChild(i).gameObject.SetActive(false);
        }
    }

    void SetAccess(int option)
    {
        _uiM._currentType = (TYPE)option;

        for(int i = 0; i < transform.childCount; i++)
        {
            if(i == option)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                continue;
            }
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if(_droptype.value == (int)TYPE.EARRING) 
        {
            _dropLR.gameObject.SetActive(true);
        }
        else
        {
            _dropLR.gameObject.SetActive(false);
        }
    }
}
