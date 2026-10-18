using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControllSC : MonoBehaviour
{
    #region Avatar Sprites
    [Header("Avatar Sprites")]
    public Sprite[] avatarDefault;
    public Sprite[] avatarFistPump;
    public Sprite[] avatarMoney;
    public Sprite[] avatarDeath;
    public Sprite[] avatarRedEye;
    #endregion
    
    public Sprite[] boxSprites;
    
    public Sprite[] sowrdSprites;
    public Sprite[] deathSprites;
    
    
    public List<float> r;
    public List<float> g;
    public List<float> b;

    public Color GetColor(int colorIndex)
    {
        return new Color(r[colorIndex], g[colorIndex], b[colorIndex], 1);
    }

    public void AssignAvatarSprites(UISpriteChanger scr, int col)
    {
        scr.AvatarDefault = avatarDefault[col];
        scr.AvatarFistPump = avatarFistPump[col];
        scr.AvatarMoney = avatarMoney[col];
        scr.AvatarDeath = avatarDeath[col];
        scr.AvatarRedEye = avatarRedEye[col];
    }
}
