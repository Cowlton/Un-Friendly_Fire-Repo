using UnityEngine;

public class ItemFreqRuleScr : MonoBehaviour
{
    [SerializeField]private ItemFrequencyScr[] frequencies;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SaveFrequencies()
    {
        for (int i = 0; i < frequencies.Length; i++)
        {
            RuleScr.Frequencies.Add(frequencies[i].Freq);
        }
    }
}
