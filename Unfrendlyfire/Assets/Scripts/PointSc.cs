using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class PointSc : MonoBehaviour
{

    public float maxPoints = 30;
    public int bonusAmount = 5;
    public float radius = 5f; 
    
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
        foreach (GameObject obj in playerSliders)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        foreach (GameObject obj in playerSliders)
        {
            var slider = obj.GetComponent<SliderSc>();
            slider.Reset();
        }
    }
    public async Task AddCash(int player, float cash, bool Bonus)
    {
        for (int i = 0; i < cash; i ++)
        {
            var cash_sc = PlayerCash[player].GetComponent<NumEffect>();
            var newCash = Instantiate(Cash, PlayerCash[player].transform.position,Quaternion.identity);
            newCash.transform.SetParent(canvas.transform, true);
            
            var CashSc = newCash.GetComponent<CashMove>();
            CashSc.CashBag = moneyBagObjs[player];
            cash_sc.Remove();
            await Task.Delay(200);
        }

        if (Bonus)
        {
            bonus(player);
        }
    }

    public async Task bonus(int player)
    {
        for (int i = 0; i < bonusAmount; i++)
        {
            
            float angle = i * Mathf.PI * 2f / bonusAmount;   // Even angle spacing
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            Vector3 pos = new Vector3(x, y, 0f) + PlayerCash[player].transform.position;
            
            var newCash = Instantiate(Cash,pos ,Quaternion.identity);
            newCash.transform.SetParent(canvas.transform, true);
            var CashSc = newCash.GetComponent<CashMove>();
            CashSc.Delay = 0.5f;
            CashSc.CashBag = moneyBagObjs[player];
            
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
