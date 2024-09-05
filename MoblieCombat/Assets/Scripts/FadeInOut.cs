using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] Image _fade;

    public void FadeIn()
    {
        if(_fade.color.a > 0)
            StartCoroutine(FadeInCor());
    }
    IEnumerator FadeInCor()
    {
        while(_fade.color.a > 0)
        {
            Color fade = _fade.color;
            --fade.a; 
            _fade.color = fade;
            yield return null;
        }
    }
    public void FadeOut()
    {
        if (_fade.color.a < 255)
            StartCoroutine(FadeOutCor());
    }
    IEnumerator FadeOutCor()
    {
        while (_fade.color.a < 255)
        {
            Color fade = _fade.color;
            ++fade.a;
            _fade.color = fade;
            yield return null;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
    }
}
