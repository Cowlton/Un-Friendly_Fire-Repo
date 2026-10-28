using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PointSc : MonoBehaviour
{
    public Maneger manager;

    public int maxPoints = 30;
    public int bonusAmount = 5;
    private float _radius = 5f; 
    
    private bool _grantBonus;
    
    public Sprite[] sliderBaise;
    public Sprite[] sliderFill;
    public Sprite[] sliderBack;
    public Sprite[] moneyBag;

    public GameObject Cash;
    
    public GameObject[] PlayerCash;
    public GameObject[] playerSliders;

    public GameObject[] sliderBaiseObjs;
    public GameObject[] sliderFillObjs;
    public GameObject[] sliderBackObjs;
    public GameObject[] moneyBagObjs;
    
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        bonusAmount = RuleScr.BonusAmount;
        maxPoints = RuleScr.WinAmount;
        _grantBonus = RuleScr.GrantBonus;
        
        foreach (GameObject obj in playerSliders)
        {
            obj.SetActive(false);
        }

        _radius = bonusAmount * 5f;
    }

  
    public void Reset()
    {
        foreach (GameObject obj in playerSliders)
        {
            var slider = obj.GetComponent<SliderSc>();
            slider.Reset();
        }
    }
    public async Task AddCash(int player, float cash, bool bonus)
    {
        for (int i = 0; i < cash; i ++)
        {
            var cash_sc = PlayerCash[player].GetComponent<NumEffect>();
            
            var newCash = Instantiate(Cash, canvas.transform, false);

            RectTransform cashRect = newCash.GetComponent<RectTransform>();
            RectTransform playerCashRect = PlayerCash[player].GetComponent<RectTransform>();

            cashRect.position = playerCashRect.position;

            var CashSc = newCash.GetComponent<CashMove>();
            CashSc.target = moneyBagObjs[player];
            cash_sc.Remove();
            await Task.Delay(200);
        }

        if (bonus && _grantBonus)
        {
            Bonus(player);
        }
    }

    public void Bonus(int player)
    {
        
        UISpriteChanger scr = manager.playerDesplayScr[player];
        scr.Won();
        try
        {
            for (int i = 0; i < bonusAmount; i++)
            {
                
                float angle = i * Mathf.PI * 2f / bonusAmount;   // Even angle spacing
                float x = Mathf.Cos(angle) * _radius;
                float y = Mathf.Sin(angle) * _radius;

                Vector3 pos = new Vector3(x, y, 0f) + PlayerCash[player].transform.position;
                
                var newCash = Instantiate(Cash, canvas.transform, false);

                RectTransform cashRect = newCash.GetComponent<RectTransform>();
                cashRect.position = pos;
                
                var CashSc = newCash.GetComponent<CashMove>();
                CashSc.Delay = 0.5f;
                CashSc.target = moneyBagObjs[player];
            
            }
        }
        catch (Exception exception)
        {
            Debug.Log("Hustin We Have a problem");
            Task.FromException(exception);
        }
    }

    public void ChangeSliders(int playerNum, int color)
    {
        playerSliders[playerNum].SetActive(true);
        sliderBaiseObjs[playerNum].GetComponent<Image>().sprite = sliderBaise[color];
        sliderFillObjs[playerNum].GetComponent<Image>().sprite = sliderFill[color];
        sliderBackObjs[playerNum].GetComponent<Image>().sprite = sliderBack[color];
        moneyBagObjs[playerNum].GetComponent<Image>().sprite = moneyBag[color];
    }
    
}
