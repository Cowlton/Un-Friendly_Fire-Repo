using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControllSC : MonoBehaviour
{
    
    public Sprite[] sowrdSprites;
    public Sprite[] deathSprites;
    
    
    public List<float> r;
    public List<float> g;
    public List<float> b;

    public Color GetColor(int colorIndex)
    {
        return new Color(r[colorIndex], g[colorIndex], b[colorIndex], 1);
    }
}
