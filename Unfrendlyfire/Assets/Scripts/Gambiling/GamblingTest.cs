using System;
using UnityEngine;
using Random = UnityEngine.Random;

public static class GamblingTest
{
   public static int Gamble(bool black, int money)
    {
        int winnings = money*-1;
        
        var rand = Random.Range(1, 18);
        if (rand == 18)
        {
            Debug.Log("The House won XD tah ha ha. y'all suck");
        }
        if (rand % 2 == 0 && !black)
        {
            Debug.Log("Wow You Win!");
            winnings = (money * 2);
        } else if (rand % 2 == 1 && black)
        {
            Debug.Log("Wow You Win!");
            winnings = (money * 2);
        }
        else
        {
           Debug.Log("You Lose!");
           Debug.Log("BAHhhhAAAAhhhA");
        }
        return winnings;
    } 
   public static int Gamble(int? number, int money)
    {
        int winnings = -money;
        
        var rand = Random.Range(1, 18);
        if (rand == 18)
        {
            Debug.Log("The House won XD tah ha ha. y'all suck");
        }

        if (rand == number)
        {
            winnings = (money * 7)+money;
        }
        
        if (winnings <= 0)
        {
            Debug.Log("You Lose!");
            Debug.Log("BAHhhhAAAAhhhA");
        }
        return winnings;
    }
}
