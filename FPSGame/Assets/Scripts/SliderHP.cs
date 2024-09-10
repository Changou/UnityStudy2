using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderHP : MonoBehaviour
{
    [SerializeField] Player _player;

    Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _slider.maxValue = _player._HP;
        _slider.minValue = 0;
        _slider.value = _player._HP;
    }

    public void SetHP()
    {
        _slider.value = _player._HP;
    }
}
