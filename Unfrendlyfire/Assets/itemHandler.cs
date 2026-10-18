using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    private GameObject _manager;
    private Maneger _managerSc;

    private float _playerNum;
    private ItemsToHold_sc _powerHoldSc;
    
    public List<Power> powers = new();
    public bool hasItem;
    public int powerNum;

    public GameObject disc;
    private Disc_Script _discSk;
    public GameObject landMine;
    
    [NonSerialized] public GameObject RockPower;
    [NonSerialized] public GameObject DiscPower;
    [NonSerialized] public GameObject LandMinePower;
    [NonSerialized] public GameObject EyePower;
    [NonSerialized] public GameObject SpikePower;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _manager = GameObject.FindWithTag("Manager");
        _managerSc =  _manager.GetComponent<Maneger>();
        _playerMovement =  gameObject.GetComponent<PlayerMovement>();
        _playerNum = _playerMovement.PlayerNUM;
        _powerHoldSc = _playerMovement.PowerHoldSc;

        if (_managerSc != null)
        {
            
            RockPower = _managerSc.PlayerRocks[(int)_playerNum];
            DiscPower = _managerSc.PlayerDiscs[(int)_playerNum];
            LandMinePower = _managerSc.PlayerLandMines[(int)_playerNum];
            EyePower = _managerSc.PlayerEyes[(int)_playerNum];
            SpikePower = _managerSc.PlayerBats[(int)_playerNum];
        
        
            RockPower.SetActive(false);
            DiscPower.SetActive(false);
            LandMinePower.SetActive(false);
            EyePower.SetActive(false);
            SpikePower.SetActive(false);
        }
        
    }

    public void UsePower()
    {
        if (_managerSc.roundEnded || !_playerMovement.canMove) return;
        if (_playerMovement.alive)
        {
            if (hasItem)
            {
                hasItem = false;
                
                _powerHoldSc.SetSprite(0);
                switch (powerNum)
                {
                    case 1: UseRock(); break;
                    case 2: UseDisk(); break;
                    case 3: UseLandMine(); break;
                    case 4: UseEye(); break;
                    case 5: UseSpikes(); break;
                }
            }
        }
        else
        {
            _playerMovement.UnDie();
        }
    }
    
    void UseRock()
    {
        RockPower.SetActive(false);
        
        _managerSc.SpawnBigRock();
        Debug.Log("Used Rock");
    }
    
    void UseDisk()
    {
        DiscPower.SetActive(false);
        
        var newObject = Instantiate(disc, transform.position, Quaternion.identity);
        _discSk = newObject.GetComponent<Disc_Script>();
        _discSk.moveDirection = _playerMovement.LastVelocity;
        Debug.Log("Used Disk");
    }
    
    void UseLandMine()
    {
        LandMinePower.SetActive(false);
        
        Instantiate(landMine, transform.position, Quaternion.identity);
        Debug.Log("Used Land Mine");
    }
    
    void UseEye()
    {
        EyePower.SetActive(false);
        
        List<Transform> rocks = _managerSc.ROCKS;
        for (int i = 0; i < rocks.Count; i++)
        {
            var sk = rocks[i].gameObject.GetComponent<CreateItem>();
            sk.showItem = true;
        }
        Debug.Log("Use Eye");
    }
    
    void UseSpikes()
    {
        List<PlayerMovement> Players = _managerSc.PlayerSc;
        foreach (var Player in Players)
        {
            Player.MakeSpikeBat();
        }
        
        SpikePower.SetActive(false);
    }

    public void ClearInventory()
    {
        RockPower.SetActive(false);
        DiscPower.SetActive(false);
        LandMinePower.SetActive(false);
        EyePower.SetActive(false);
        SpikePower.SetActive(false);
        
        _powerHoldSc.SetSprite(0);//null
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var other = collision.gameObject;
        if (_playerMovement != null)
        {
            if (!hasItem && _playerMovement.alive)
            {
                foreach (var power in powers)
                {
                    if(power == null || power.item == null)
                        continue;
                    if (other.CompareTag(power.item.tag))
                    {
                        powerNum = power.powerNum;
                        hasItem = true;
                        _powerHoldSc.SetSprite(power.powerNum);
                        ShowUI(power.powerNum);
                        Destroy(other);
                        return;
                    }
                }
            }
        }
    }
    
    void ShowUI(int power)
    {
        switch (power)
        {
            case 1:
                RockPower.SetActive(true);
                break;
            case 2:
                DiscPower.SetActive(true);
                break;
            case 3:
                LandMinePower.SetActive(true);
                break;
            case 4:
                EyePower.SetActive(true);
                break;
            case 5:
                SpikePower.SetActive(true);
                break;
            
        }
    }
    
    
}
