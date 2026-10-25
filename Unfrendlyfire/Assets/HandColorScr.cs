using System;
using System.Collections.Generic;
using UnityEngine;

public class HandColorScr : MonoBehaviour
{
    [NonSerialized] public int Color;
    [SerializeField]private List<Sprite> sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Color];
    }
    
}
