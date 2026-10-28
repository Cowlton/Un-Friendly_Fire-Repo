using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemFrequencyScr : MonoBehaviour
{
    private const int MaxFreq = 100;
    [SerializeField] private Image[] indicators;
    [SerializeField]private Color enabledColor;
    private Color _disabledColor;
    
    private int _fpu; //frequency per unit
    private int _enabledIndex;
    
    [NonSerialized]public int Freq;
    [SerializeField]private int defaultEnabledIndex;

    private void Start()
    {
        _fpu = MaxFreq/indicators.Length;
        _disabledColor = indicators[0].color;
        if (indicators.Length >= defaultEnabledIndex)
        {
            for (int i = 0; i < defaultEnabledIndex; i++)
            {
                indicators[i].color = enabledColor;
            }
            _enabledIndex =  defaultEnabledIndex;
            Freq =  _fpu * _enabledIndex-1;
        }
        
    }
    
    public void OnClick()
    {
        _enabledIndex++;

        if (_enabledIndex > indicators.Length)
        {
            _enabledIndex = 0;
            foreach (var image in indicators)
            {
                image.color = _disabledColor;
            }

            Freq = 0;
        }
        else
        {
            indicators[_enabledIndex-1].color = enabledColor;
            Freq =  _fpu * _enabledIndex-1;
        }
        
        
        
    }
}
