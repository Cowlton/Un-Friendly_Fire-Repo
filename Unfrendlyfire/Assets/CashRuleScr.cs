using UnityEngine;
using UnityEngine.UI;

public class CashRuleScr : MonoBehaviour
{
    [SerializeField]private CheckBoxToggleForButton grantBonus;
    
    [SerializeField]private Slider bonusAmountSlider;
    [SerializeField]private Slider roundAmountSlider;
    [SerializeField]private Slider winAmountSlider;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SaveCashRules()
    {
        RuleScr.GrantBonus = grantBonus.Value;
        RuleScr.BonusAmount = (int)bonusAmountSlider.value;
        RuleScr.WinRoundAmount = (int)roundAmountSlider.value;
        RuleScr.WinAmount = (int)winAmountSlider.value;
    }
}
