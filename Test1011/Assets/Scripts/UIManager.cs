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

    public enum UI
    {
        START,
        GAME,
        OVER
    }
    [Header("UI"), SerializeField] GameObject[] _uis;

    [Header("볼 카운트 제어"), SerializeField] BallCount _ballCount;

    public void GoalIn()
    {
        _ballCount.BallCountUp();
    }

    public void SetClearUI(bool isClear)
    {
        _uis[(int)UI.OVER].transform.GetChild(isClear ? 0 : 1).gameObject.SetActive(true);
    }

    public void SetActiveUI(UI ui, bool isOn)
    {
        _uis[(int)ui].SetActive(isOn);
    }
}
