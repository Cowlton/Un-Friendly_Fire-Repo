using System;
using UnityEngine;
using UnityEngine.UIElements;

public class CheckBoxToggleForButton : MonoBehaviour
{
    [NonSerialized]public bool Value;
    [SerializeField]private GameObject checkBox;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Value = checkBox.activeSelf;// active self checks if the checkbox is active or not
    }

    public void Toggle()
    {
        Value = !Value;// Reverses value
        checkBox.SetActive(Value);// Sets the checkbox as active or not
        
        Debug.Log(Value);
    }
}
