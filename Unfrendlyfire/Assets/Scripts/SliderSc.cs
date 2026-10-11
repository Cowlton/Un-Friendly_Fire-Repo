using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSc : MonoBehaviour
{
    [NonSerialized]public float value;
    public float speed;
    public Slider slider;
    
    private GameObject PointObj;
    private PointSc pointSc;

    public GameObject OverFlowText;
    private NumEffect OFT_sc;

    private bool OFT_active = false;
    
    private RectTransform Rect;
    
    private GameObject _manager;
    private Maneger _managerSc;

    private bool hasWon = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _manager =  GameObject.FindGameObjectWithTag("Manager");
        _managerSc = _manager.GetComponent<Maneger>();
        
        Rect = gameObject.GetComponent<RectTransform>();
        OFT_sc =  OverFlowText.GetComponent<NumEffect>();
        OverFlowText.SetActive(false);
        PointObj = GameObject.FindWithTag("SliderCon");
        pointSc = PointObj.GetComponent<PointSc>();
    }
    // Update is called once per frame
    void Update()
    {
        var val = value / pointSc.maxPoints;
        if (val <= slider.maxValue)
        {
            if (slider.value < val)
            {
                slider.value += val * Time.deltaTime * speed;
            }
        }
        if (slider.value == 1 && !hasWon)
        {
            Won();
            _managerSc.PlayerWon();
            hasWon = true;
        }
    }

    public void Reset()
    {
        value = 0;
        slider.value = 0;
    }

    public void Won()
    {
        Vector2 size = Rect.sizeDelta;
        size.x += 30;
        size.y += 30;
        Rect.sizeDelta = size;
    }

    public void Incramint()
    {
        if (value < pointSc.maxPoints)
        {
            value++;
        }
        else
        {
            if (!OFT_active)
            {
                OverFlowText.SetActive(true);
                OFT_active = true;
            }
            
            OFT_sc.Add();
        }
        
    }
    
}
