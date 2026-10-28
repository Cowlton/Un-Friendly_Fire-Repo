using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
public class NumEffect : MonoBehaviour
{
    public float SizeDiff;
    [NonSerialized]public float Value;
    public TextMeshProUGUI text;
    public async Task Add()
    {
        Value += 1;
        text.text = Value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(500);
        text.fontSize -= SizeDiff;
    }
    public async Task Add(int delay)
    {
        Value += 1;
        text.text = Value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(delay);
        text.fontSize -= SizeDiff;
    }

    public async Task Remove()
    {
        Value -= 1;
        text.text = Value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(100);
        text.fontSize -= SizeDiff;
    }
    public async Task Remove(int delay)
    {
        Value -= 1;
        text.text = Value.ToString();
        text.fontSize += SizeDiff;
        await Task.Delay(delay);
        text.fontSize -= SizeDiff;
    }

    public void Reset()
    {
        Value = 0;
        text.text = "";
    }
}
