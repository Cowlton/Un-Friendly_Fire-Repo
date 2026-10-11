using UnityEngine;

public class ItemsToHold_sc : MonoBehaviour
{
    
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;
    

    // Update is called once per frame
    public void SetSprite(int Power)
    {
        if  (Power == 0)
        {
            spriteRenderer.sprite = null;
        }
        else
        {
            spriteRenderer.sprite = sprites[Power-1];
        }
        
    }
}
