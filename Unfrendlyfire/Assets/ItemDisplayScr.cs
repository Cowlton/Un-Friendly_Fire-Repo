using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDisplayScr : MonoBehaviour
{
    
    [SerializeField]private List<Sprite> itemSprites;
    [SerializeField]private Image itemImage;

    public void ShowSprite(int powerIndex)
    {
        itemImage.enabled = true;
        itemImage.sprite = itemSprites[powerIndex];
    }

    public void HideSprite()
    {
        itemImage.enabled = false;
    }
}
