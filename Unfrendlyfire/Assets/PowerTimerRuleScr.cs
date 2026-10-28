using UnityEngine;
using UnityEngine.UI;

public class PowerTimerRuleScr : MonoBehaviour
{
    
    [SerializeField]private CheckBoxToggleForButton timerBox;
    [SerializeField]private Slider time;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SaveTimerRules()
    {
        RuleScr.UseWeaponPowerTimers = timerBox.Value;
        RuleScr.WeaponPowerTime =  time.value;
    }
}
