using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EMOTION
{
    ANGRY,
    SAD,
    HAPPY,

    MAX
}

public class UI_Manager2 : MonoBehaviour
{
    [SerializeField] Text _text;
    
    public void SetText(string text)
    {
        _text.text = text;
    }

    [SerializeField] EmotionParticle _emotionP;

    public void SetEmotionParticleOn(int num)
    {
        _emotionP.EmotionParticleOn((EMOTION)num);
    }
}
