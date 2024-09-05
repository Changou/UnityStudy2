using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject _fireBtn;
    [SerializeField] JetPlane _jet;

    // Update is called once per frame
    void Update()
    {
        if(_jet._haveMissile > 0) _fireBtn.SetActive(true);
        else _fireBtn.SetActive(false);
    }
}
