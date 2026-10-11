using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SowrdTrailSC : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public float deathTime = 2;
    public float fadeSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        Color newColor = _spriteRenderer.color;
        newColor.a = 0.5f ;
        _spriteRenderer.color = newColor;
        Destroy(gameObject, deathTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate new alpha, decreasing over time
        float newAlpha = _spriteRenderer.color.a - (Time.deltaTime * fadeSpeed);

        // Clamp alpha between 0 and 1
        newAlpha = Mathf.Clamp01(newAlpha);

        // Apply new color with updated alpha
        Color newColor = _spriteRenderer.color;
        newColor.a = newAlpha;
        _spriteRenderer.color = newColor;
    }
}
