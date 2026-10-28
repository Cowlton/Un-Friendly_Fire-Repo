using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueTextScr : MonoBehaviour
{
    [SerializeField]private Slider slider;
    
    [SerializeField]private NumEffect effect;
    [SerializeField]private TextMeshProUGUI text;
    private float _value;
    private bool _hasEffect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Update is called once per frame

    void Start()
    {
        _hasEffect = effect != null;
        _value = slider.value;
        effect.Value = _value;
        text.text = _value.ToString();
    }
    
    void Update()
    {
        if (_hasEffect && effect.Value < _value)
        {
            _ = effect.Add();
        } else if (_hasEffect && effect.Value > _value)
        {
            _ = effect.Remove();
        }
    }

    public void SetValue()
    {
        _value = slider.value;
        if(!_hasEffect)
        {
            text.text = _value.ToString();
        }
    }
}
