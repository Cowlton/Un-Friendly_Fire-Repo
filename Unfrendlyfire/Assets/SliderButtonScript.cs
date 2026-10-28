using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SliderButtonScript : MonoBehaviour
{ 
    [SerializeField]private GameObject slider;
    private bool _sliderSelected;

    private float _pauseTime;

    private void Update()
    {
        if (_pauseTime > 0)
        {
            _pauseTime -= 1 * Time.deltaTime;
        }
        if (_sliderSelected && _pauseTime <= 0)
        {
            if (Input.GetButtonDown("Submit"))
            {
                Confirm();
            }
        }
    }

    private void Confirm()
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        _sliderSelected = false;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnClick()
    {
        EventSystem.current.SetSelectedGameObject(slider);
        _sliderSelected =  true;
        _pauseTime = 1;
    }
}
