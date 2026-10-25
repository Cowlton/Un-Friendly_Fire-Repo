using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponPowerTimeScr : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI timerText;
    [SerializeField]private Image itemImage;
    [SerializeField]private List<Sprite> itemSprites;
    [SerializeField]private Animator animator;
    
    [SerializeField]private Maneger manager;
    
    public float timeRemaining = 0;
    
    void Update() 
    {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            timerText.text = string.Format("{0:0}.{1:00}",
                Mathf.Floor(timeRemaining),
                (int)(timeRemaining * 100) % 100);
        }else if (timeRemaining <= 0)
        {
            timerText.text = string.Format("{0:0}.{1:00}",
                Mathf.Floor(0),
                (int)(timeRemaining * 100) % 100);
            TimerEnd();
        }
    }

    public void SetTimer(int spriteIndex)
    {
        Show();
        timeRemaining = RuleScr.WeaponPowerTime;
        itemImage.sprite = itemSprites[spriteIndex];
    }

    private void TimerEnd()
    {
        List<PlayerMovement> playerScr = manager.PlayerSc;
        foreach (PlayerMovement scr in playerScr)
        {
            scr.ResetBat();
        }
        animator.SetBool("Out", true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
        animator.SetBool("Out", false);
    }
    
    
    
}
