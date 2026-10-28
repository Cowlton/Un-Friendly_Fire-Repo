using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerConfigManager : MonoBehaviour
{
    public List<PlayerConfig> PlayerConfigs;

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
            PlayerConfigs = new List<PlayerConfig>();
        }
    }

    public int SetPlayerColor(int index)
    {
        var color = 0;
        var randNum = Random.Range(0,colors.Count);
        color = colors[randNum]; // I use a list of indexes so that there are not 2 players of the same color
        PlayerConfigs[index].ColorIndex = color;
        colors.Remove(color);
        return color;
    }
    
    public void PlayerJoin(PlayerInput pi)
    {
        Debug.Log($"PlayerJoin {pi}");
        if (PlayerConfigs.All(p => p.PlayerIndex != pi.playerIndex))
        {
            Debug.Log($"New Player ID: {pi.playerIndex}");
            pi.transform.SetParent(transform);
            PlayerConfigs.Add(new PlayerConfig(pi));
        }

        if (promptHandler != null)
        {
            promptHandler.ShowJoinPrompt();
        }
        
    }

    public void ReadyPlayer(int index)
    {
        PlayerConfigs[index].IsReady = true;
        if (PlayerConfigs.All(p => p.IsReady == true))
        {
            StartGame(); // Change to a button press later
        }
    }

    public void Leave(int index,int colorIndex)
    {
        if (PlayerConfigs.Count >= index)
        {
            PlayerConfigs.RemoveAt(index);
        }
        else
        {
            Debug.Log($"Player ID: {PlayerConfigs[index].PlayerIndex} is outside of range");
        }
        promptHandler.HideJoinPrompt();
        colors.Add(colorIndex);
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene("RuleSetup");
    }
    
    public List<PlayerConfig> GetPlayerConfigs()
    {
        return PlayerConfigs;
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