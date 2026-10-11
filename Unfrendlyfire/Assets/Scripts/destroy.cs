using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public GameObject colorController;
    private ColorControllSC _colorCon;
    
    public SpriteRenderer spriteRenderer;
    public float time = 1;
    public int index;

    // Start is called before the first frame update
    void Start()
    {
       colorController = GameObject.FindWithTag("ColorCon");
       _colorCon = colorController.GetComponent<ColorControllSC>();

       var color = _colorCon.GetColor(index);

        Destroy(gameObject, time);

        if (GetComponent<ParticleSystem>() == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Apply the new start color
        spriteRenderer.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
