using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteMaskSc : MonoBehaviour
{
    public SpriteMask targetMask;

    void Start()
    {
        var spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure the sprite is only visible inside the mask
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;

        // Optional: Match sorting layer and order to ensure correct rendering
        if (targetMask != null)
        {
            spriteRenderer.sortingLayerID = targetMask.sortingLayerID;
            spriteRenderer.sortingOrder = targetMask.sortingOrder + 1;
        }
    }

}
