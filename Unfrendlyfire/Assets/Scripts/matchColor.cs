using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class matchColor : MonoBehaviour
{
    public TextMeshProUGUI text;
    public TextMeshProUGUI ColorToMatch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (text.color != ColorToMatch.color)
        {
            text.color = ColorToMatch.color;
        }
        
    }
}
