using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerConfigManager : MonoBehaviour
{
    public List<PlayerConfig> _playerConfigs;

    [SerializeField]
    private int maxPlayers = 4;

    [SerializeField] private JoinPromptHandlerScr promptHandler;

    public static PlayerConfigManager Instance {get;set; }

    private List<int> colors = new(){0,1,2,3,4,5,6,7};

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Whoops - you are trying to create another instance ");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            _playerConfigs = new List<PlayerConfig>();
        }
    }

    public int SetPlayerColor(int index)
    {
        var color = 0;
        var randNum = Random.Range(0,colors.Count);
        color = colors[randNum]; // I use a list of indexes so that there are not 2 players of the same color
        _playerConfigs[index].ColorIndex = color;
        colors.Remove(color);
        return color;
    }

    public void PlayerJoin(PlayerInput pi)
    {
       
        Debug.Log($"PlayerJoin {pi}");
        if (_playerConfigs.All(p => p.PlayerIndex != pi.playerIndex))
        {
            Debug.Log($"New Player ID: {pi.playerIndex}");
            pi.transform.SetParent(transform);
            _playerConfigs.Add(new PlayerConfig(pi));
        }
        promptHandler.ShowJoinPrompt();
    }

    public void ReadyPlayer(int index)
    {
        _playerConfigs[index].IsReady = true;
        if (_playerConfigs.All(p => p.IsReady == true))
        {
            StartGame(); // Change to a button press later
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("0");
    }
    
    public List<PlayerConfig> GetPlayerConfigs()
    {
        return _playerConfigs;
    }
    
}



public class PlayerConfig
{
    public PlayerConfig(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    
    public PlayerInput Input { get; set; }
    public int PlayerIndex {get;set;}
    
    public bool IsReady {get;set;}

    public int ColorIndex {get;set;}
}