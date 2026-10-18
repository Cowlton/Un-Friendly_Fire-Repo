using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteChanger : MonoBehaviour
{
    [NonSerialized]public Sprite AvatarDefault;
    [NonSerialized]public Sprite AvatarFistPump;
    [NonSerialized]public Sprite AvatarMoney;
    [NonSerialized]public Sprite AvatarDeath;
    [NonSerialized]public Sprite AvatarRedEye;
    
    public TextMeshProUGUI OFT; // overflow text

    private bool isDefalt = false;
    
    public Image avater;
    
    public GameObject colorChanger;
    private ColorControllSC _colorCon;
    
	private	GameObject SliderCon;
	private PointSc SliderCon_sc;
	
    public GameObject KeycardBack;
    
    private GameObject manager;
    private Maneger ManagerSK;

    [NonSerialized]public int PlayerNum;
    
    private bool hasChanged = false;

    [NonSerialized]public int Color = 0;

    private float timer;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.FindWithTag("Manager");
        ManagerSK = manager.GetComponent<Maneger>();

		SliderCon = GameObject.FindWithTag("SliderCon");
		SliderCon_sc = SliderCon.GetComponent<PointSc>();
        
        colorChanger = GameObject.FindWithTag("ColorCon");
        _colorCon = colorChanger.GetComponent<ColorControllSC>();

    }
    
    public void GotCash()
    {
        timer = Time.time + 0.4f;
        isDefalt = false;
        avater.sprite = AvatarFistPump;
        
    }


    public void Died()
    {
        avater.sprite = AvatarDeath;
    }
    public void Won()
    {
        timer = Time.time + 0.7f;
        isDefalt = false;
        
        avater.sprite = AvatarMoney;
        
    }

    public void DefaultAvatar()
    {
        avater.sprite = AvatarDefault;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (timer <= Time.time && !isDefalt)
        {
            isDefalt = true;
            DefaultAvatar();
        }
    }
    
    public void ChangeSprites(int color)
    {
        Color = color;
        var scr = gameObject.GetComponent<UISpriteChanger>();
        _colorCon.AssignAvatarSprites(scr, Color);
        
        var changeColor = _colorCon.GetColor(Color);
        OFT.color = changeColor;
        
        DefaultAvatar();
        
        Image image = KeycardBack.GetComponent<Image>();
        image.sprite = _colorCon.boxSprites[Color];
        SliderCon_sc.ChangeSliders(PlayerNum,Color);
    }
}
