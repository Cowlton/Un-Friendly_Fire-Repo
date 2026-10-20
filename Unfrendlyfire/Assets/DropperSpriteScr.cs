using System.Collections.Generic;
using UnityEngine;

public class DropperSpriteScr : MonoBehaviour
{
    [SerializeField]private SpriteRenderer spriteRenderer;

    [SerializeField] private List<Sprite> sprites;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        ChoseSprite();
    }

    private void ChoseSprite()
    {
        var randNum = Random.Range(0, sprites.Count);
        spriteRenderer.sprite = sprites[randNum];
    }
}
