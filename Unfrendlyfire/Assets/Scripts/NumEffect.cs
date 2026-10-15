using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class NumEffect : MonoBehaviour
{
    public float SizeDiff;
    private float _value = 0;
    public TextMeshProUGUI text;
    public async Task Add()
    {
        _value += 1;
        text.text = _value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(500);
        text.fontSize -= SizeDiff;
    }
    public async Task Add(int delay)
    {
        _value += 1;
        text.text = _value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(delay);
        text.fontSize -= SizeDiff;
    }

    public async Task Remove()
    {
        _value -= 1;
        text.text = _value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(100);
        text.fontSize -= SizeDiff;
    }
    public async Task Remove(int delay)
    {
        _value -= 1;
        text.text = _value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(delay);
        text.fontSize -= SizeDiff;
    }

    public void Reset()
    {
        _value = 0;
        text.text = "";
    }
}
