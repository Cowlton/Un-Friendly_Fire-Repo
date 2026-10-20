using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetUpMenu : MonoBehaviour
{

    [NonSerialized] public GameObject Config;
    private int _playerIndex;
    private int _color;

    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private GameObject readyPanel;
    
    [SerializeField]private GameObject menuPanel;
    [SerializeField]private Image playerImage;

    private float _ignoreInputTime = 1.5f;
    private bool _inputEnabled;

    [SerializeField]private GameObject readyButton;
   
    [SerializeField]private List<Sprite> backSprites;
    [SerializeField]private List<Sprite> playerSprites;
    
    private PlayerConfigManager _manager;

    private void Awake()
    {
        _manager = PlayerConfigManager.Instance;
    }

    public void SetPlayerIndex(int pi)
    {
        _playerIndex = pi;
        titleText.SetText("Player " + (_playerIndex +1));
        _ignoreInputTime = Time.time + _ignoreInputTime;
        SetPlayerColor();
        
    }

    private void SetPlayerColor()
    {
        _color = _manager.SetPlayerColor(_playerIndex);
        
        playerImage.sprite = playerSprites[_color];
        var backImage = GetComponent<Image>();
        backImage.sprite = backSprites[_color];
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > _ignoreInputTime)
        {
            _inputEnabled = true;
        }
        
    }

    public void Ready()
    {
        if(!_inputEnabled) return;
        _manager.ReadyPlayer(_playerIndex);
        readyButton.SetActive(false);
        readyPanel.SetActive(true);
        
    }

    public void Leave()
    {
        if(!_inputEnabled) return;
        Debug.Log("Leaving");
        _manager.Leave(_playerIndex,_color);
        Destroy(Config.gameObject);
        Destroy(gameObject,0.01f);
    }
    
}
