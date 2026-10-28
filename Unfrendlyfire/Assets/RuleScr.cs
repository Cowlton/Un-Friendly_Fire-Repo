using System.Collections.Generic;


public static class RuleScr
{
    //Cash
    public static bool GrantBonus = true;
    public static int BonusAmount;
    public static int WinRoundAmount;
    public static int WinAmount;
    
    //item Frequencies
    public static List<int> Frequencies = new();
    
    
    // WeaponPowerTimer
    public static bool UseWeaponPowerTimers = true;
    public static float WeaponPowerTime = 10;
}
