using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] Image _fade;
    [SerializeField] DOTweenAnimation _anim;

    public void FadeIn()
    {
        _anim.DOPlayById("FadeIn");
    }

    public void FadeOut()
    {
        _anim.DOPlayById("FadeOut");
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
