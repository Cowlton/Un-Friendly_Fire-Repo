using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GamblingTest : MonoBehaviour
{
    [Range(1,17)]
    public int Number;
    
    [Range(1,10)]
    public int Money;

    public bool Black;

    public int Winnings;

    void Update()
    {
        Winnings += Gamble(Number, null, Money);
        Winnings += Gamble(null, Black, Money);
        Debug.Log(Gamble(Number,null,Money));
        Debug.Log(Gamble(null,Black,Money));
    }
    
    public int Gamble(int? number, bool? black, int money)
    {
        
        Winnings -= money;
        int winnings = 0;
        
        var rand = Random.Range(1, 18);
        if (rand == 18)
        {
            Debug.Log("The House won XD tah ha ha. y'all suck");
        }

        if (rand == number)
        {
            if (black != null && black == false)
            {
                Debug.Log("Wow You Win!");
            }
            winnings = (money * 7)+money;
        }

        if (rand % 2 == 0)
        {
            if (black != null && black == false)
            {
                Debug.Log("Wow You Win!");
            }
            winnings = (money * 2);
        }

        if (rand % 2 == 1)
        {
            if (black != null && black == true)
            {
                Debug.Log("Wow You Win!");
            }
            winnings = (money * 2);
        }

        
        return winnings;
    }
}
