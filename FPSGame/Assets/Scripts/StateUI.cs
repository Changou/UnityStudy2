using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateUI : MonoBehaviour
{
    [SerializeField] GameObject[] _stateUI;

    public void SetState(STATE state)
    {
        for (int i = 0; i < _stateUI.Length; i++)
        {
            if ((int)state == i)
                _stateUI[i].SetActive(true);
            else
                _stateUI[i].SetActive(false);
        }
    }
}
