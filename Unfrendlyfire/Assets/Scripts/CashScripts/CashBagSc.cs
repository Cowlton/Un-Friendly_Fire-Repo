using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Slider = UnityEngine.UI.Slider;

public class CashBagSc : MonoBehaviour
{
    //[SerializeField]private GameObject sliderObj;
    public SliderSc slider;
    private RectTransform Rect;

   

    public float SizeDiff;

    private Vector2 Size;
    // Start is called before the first frame update
    void Start()
    {
        //text =  sliderObj.GetComponent<Slider>();
        Rect = gameObject.GetComponent<RectTransform>();
        Size =  Rect.sizeDelta;
    }

    public async Task AddCash()
    {

        slider.Incramint();
        
        Vector2 size = Rect.sizeDelta;
        size.x += SizeDiff;
        size.y += SizeDiff;
        Rect.sizeDelta = size;
        await Task.Delay(100);
        Rect.sizeDelta = Size;

        
    }
}
