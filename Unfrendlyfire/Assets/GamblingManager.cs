using System.Net.Mime;
using TMPro;
using UnityEngine;

public class GamblingManager : MonoBehaviour
{
    
    private bool _betBlack = false;

    [SerializeField] private int currentCash;
    [SerializeField] private TextMeshProUGUI cashDisplay;

    private int _bet;
    public int textDelay;

    public int num;

    public NumEffect betScr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cashDisplay.text = currentCash.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void IncrementBet()
    {
        if (_bet < currentCash)
        {
            _bet += 1;
            betScr.Add(textDelay);
        }
    }

    public void DecrementBet()
    {
        if (_bet > 0)
        {
            _bet -= 1;
            betScr.Remove(textDelay);
        }
    }

    public void BetNumber(int number)
    {
        num =  number;
    }
    
    public void Red()
    {
        num = 0;
        _betBlack = false;
    }

    public void Black()
    {
        num = 0;
        _betBlack = true;
    }

    public void PlaceBet()
    {
        Debug.Log("Bet Placed");
        var winnings = 0;
        if (num > 0)
        {
            winnings = GamblingTest.Gamble(num, _bet);
        }
        else
        {
            winnings = GamblingTest.Gamble(_betBlack,_bet);
        }
        
        Debug.Log(winnings);

        currentCash += winnings;
        cashDisplay.text = currentCash.ToString();

        if (_bet > currentCash)
        {
            betScr.text.text = currentCash.ToString();
            _bet = currentCash;
        }
    }
}
