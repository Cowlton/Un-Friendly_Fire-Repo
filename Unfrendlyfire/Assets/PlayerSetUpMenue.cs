using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSetUpMenue : MonoBehaviour
{

    private int _playerIndex;

    [SerializeField] private TextMeshProUGUI titleText;

    [SerializeField] private GameObject readyPanel;
    
    [SerializeField]private GameObject menuPanel;
    [SerializeField]private Image playerImage;

    private float _ignoreInputTime = 1.5f;
    private bool _inputEnabled;

    [SerializeField]private GameObject readyButton;
    
    [SerializeField]private List<Sprite> backSprites;
    [SerializeField]private List<Sprite> playerSprites;

    public void SetPlayerIndex(int pi)
    {
        _playerIndex = pi;
        titleText.SetText("Player " + (_playerIndex +1));
        _ignoreInputTime = Time.time + _ignoreInputTime;
        SetPlayerColor();
    }

    private void SetPlayerColor()
    {
        var color = PlayerConfigManager.Instance.SetPlayerColor(_playerIndex);
        
        playerImage.sprite = playerSprites[color];
        var backImage = GetComponent<Image>();
        backImage.sprite = backSprites[color];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        PlayerConfigManager.Instance.ReadyPlayer(_playerIndex);
        readyButton.SetActive(false);
        readyPanel.SetActive(true);
        
    }

    public void Leave()
    {
        Debug.Log("Leaving");
    }
    
}
