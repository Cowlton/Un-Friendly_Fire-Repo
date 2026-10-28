using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateRulesScr : MonoBehaviour
{
    [SerializeField]private ItemFreqRuleScr itemRuleScr;
    [SerializeField]private PowerTimerRuleScr powerTimerRuleScrRuleScr;
    [SerializeField]private CashRuleScr cashRuleScrRuleScrRuleScr;
    public void UpdateRules()
    {
        itemRuleScr.SaveFrequencies();
        powerTimerRuleScrRuleScr.SaveTimerRules();
        cashRuleScrRuleScrRuleScr.SaveCashRules();

        Advance();
    }

    private void Advance()
    {
        SceneManager.LoadScene("0");
    }
}
