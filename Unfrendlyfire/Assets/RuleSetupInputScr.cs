using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class RuleSetupInputScr : MonoBehaviour
{
    private PlayerInput _input;
    public ScrollRectAutoScroll ScrollRect;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerConfigManager configManager = GameObject.Find("PlayerConfigManager").GetComponent<PlayerConfigManager>();
        if ( configManager != null)
        {
            _input = configManager.PlayerConfigs[0].Input;
            _input.uiInputModule = gameObject.GetComponent<InputSystemUIInputModule>();
        }
    }

    void Update()
    {
        if (_input.uiInputModule.move)
        {
            ScrollRect.ScrollToSelected(false);
        }
    }
}
