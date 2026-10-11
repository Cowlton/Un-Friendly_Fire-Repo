using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphaShift : MonoBehaviour
{
    public int index = 0;
    public SpriteRenderer spriteRenderer;
    public float duration = 1f;
    public bool fadeOut = true;
    public bool destroyAfterFade = true;
    public Sprite[] sprites;
    

    void Start()
    {
        // Auto-assign if not done in Inspector
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprites[index];
        StartCoroutine(Fade(fadeOut));
    }

    IEnumerator Fade(bool toTransparent)
    {
        Color color = spriteRenderer.color;
        float startAlpha = color.a;
        float endAlpha = toTransparent ? 0f : 1f;
        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            spriteRenderer.color = color;

            time += Time.deltaTime;
            yield return null;
        }

        // Ensure final alpha is set
        color.a = endAlpha;
        spriteRenderer.color = color;

        // Destroy after fade if enabled
        if (destroyAfterFade && toTransparent)
        {
            Destroy(gameObject);
        }
    }

}
