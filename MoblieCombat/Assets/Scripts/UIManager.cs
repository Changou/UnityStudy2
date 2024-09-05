using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        LOBBY,
        GAME,
        GAMEOVER,
        GAMECLEAR,

        MAX
    }

    [Header("ÀÎ°ÔÀÓ")]
    [SerializeField] Text _coinT;
    [SerializeField] Text _hp;
    [SerializeField] Text _speed;
    
    private void Start()
    {
        GameManager._Inst.StartGameFunc += () => Show_Only(UI.GAME); 
    }

    public void UpdateCoin(int coin)
    {
        _coinT.text = "COIN : " + coin;
    }

    public void UpdateHp(int hp)
    {
        _hp.text = "HP : " + hp;
    }
    public void UpdateSpeed(int speed)
    {
        _speed.text = "SPEED : " + speed + "m/s";
    }

    public void Show_Only(UI ui)
    {
        AllHide();
        _uis[(int)ui].SetActive(true);
    }

    void AllHide()
    {
        foreach (GameObject ui in _uis)
        {
            ui.SetActive(false);
        }
    }

    [SerializeField] FadeInOut _fade;

    public void FadeIn()
    {
        _fade.FadeIn();
    }

    public void Restart()
    {
        _fade.FadeOut();
    }
}
