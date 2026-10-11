using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISpriteChanger : MonoBehaviour
{
    public Sprite[] avatarDefalt;
    public Sprite[] avatarFistPump;
    public Sprite[] avatarMoney;
    public Sprite[] avatarDeath;
    public Sprite[] avatarRedEye;
    
    public TextMeshProUGUI OFT;

    private bool isDefalt = false;
    
    public Image avater;
    
    public GameObject colorChanger;
    private ColorControllSC _colorCon;
    
	private	GameObject SliderCon;
	private PointSc SliderCon_sc;
	
    public GameObject KeycardBack;

    public Sprite[] BoxSprites;

    private GameObject manager;
    private Maneger ManagerSK;

    public int PlayerNUM;
    
    private bool hasChanged = false;

    [NonSerialized]public int color = 0;

    private float timer;

    // Start is called before the first frame update
    void Start()
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
        
        if (avatarFistPump.Length >= color)
        {
            avater.sprite = avatarFistPump[color];
        }
    }


    public void Died()
    {
        if (avatarDeath.Length >= color)
        {
            avater.sprite = avatarDeath[color];
        }
    }
    public void Won()
    {
        timer = Time.time + 0.7f;
        isDefalt = false;
        if (avatarMoney.Length >= color)
        {
            avater.sprite = avatarMoney[color];
        }
    }

    public void DefaltAvatar()
    {
        if (avatarDefalt.Length >= color)
        {
            avater.sprite = avatarDefalt[color];
        }
    }
    

    // Update is called once per frame
    void Update()
    {
        if (timer <= Time.time && !isDefalt)
        {
            isDefalt = true;
            DefaltAvatar();
        }
        if (hasChanged == false && PlayerNUM == ManagerSK.Player.Count)
        {
            changeSprites();
            hasChanged = true;
        }
    }
    
    void changeSprites()
    {
        var changeColor = _colorCon.GetColor(color);
        OFT.color = changeColor;
        DefaltAvatar();
        
         Image image = KeycardBack.GetComponent<Image>();
         image.sprite = BoxSprites[color];
		 SliderCon_sc.ChangeSliders(PlayerNUM-1,color);
                     
        
    }
}
