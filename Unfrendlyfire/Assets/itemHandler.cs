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
    
     private GameObject _rockPower;
     private GameObject _discPower;
     private GameObject _landMinePower;
     private GameObject _eyePower;
     private GameObject _spikePower;
     private GameObject _gunPower;
     
     private WeaponPowerTimeScr _weaponTimerScr;
    
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
            _rockPower = _managerSc.PlayerRocks[(int)_playerNum];
            _discPower = _managerSc.PlayerDiscs[(int)_playerNum];
            _landMinePower = _managerSc.PlayerLandMines[(int)_playerNum];
            _eyePower = _managerSc.PlayerEyes[(int)_playerNum];
            _spikePower = _managerSc.PlayerBats[(int)_playerNum];
            _gunPower = _managerSc.PlayerGuns[(int)_playerNum];
            
            _rockPower.SetActive(false);
            _discPower.SetActive(false);
            _landMinePower.SetActive(false);
            _eyePower.SetActive(false);
            _spikePower.SetActive(false);
            _gunPower.SetActive(false);
        }
        
        _weaponTimerScr = GameObject.Find("Gun/SpikeTime").GetComponent<WeaponPowerTimeScr>();

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
                    case 6: UseGun(); break;
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
        _rockPower.SetActive(false);
        
        _managerSc.SpawnBigRock();
        Debug.Log("Used Rock");
    }
    
    void UseDisk()
    {
        _discPower.SetActive(false);
        
        var newObject = Instantiate(disc, transform.position, Quaternion.identity);
        _discSk = newObject.GetComponent<Disc_Script>();
        _discSk.moveDirection = _playerMovement.LastVelocity;
        Debug.Log("Used Disk");
    }
    
    void UseLandMine()
    {
        _landMinePower.SetActive(false);
        
        Instantiate(landMine, transform.position, Quaternion.identity);
        Debug.Log("Used Land Mine");
    }
    
    void UseEye()
    {
        _eyePower.SetActive(false);
        
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
        
        _spikePower.SetActive(false);
        SetTimer(0);
    }

    void UseGun()
    {
        List<PlayerMovement> players = _managerSc.PlayerSc;
        foreach (var player in players)
        {
            player.MakeGun();
        }
        _gunPower.SetActive(false);
        SetTimer(1);
    }

    void SetTimer(int spriteIndex)
    {
        if (RuleScr.UseWeaponPowerTimers)
        {
           _weaponTimerScr.SetTimer(spriteIndex);
        }
    }

    public void ClearInventory()
    {
        _rockPower.SetActive(false);
        _discPower.SetActive(false);
        _landMinePower.SetActive(false);
        _eyePower.SetActive(false);
        _spikePower.SetActive(false);
        _gunPower.SetActive(false);
        
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
                _rockPower.SetActive(true);
                break;
            case 2:
                _discPower.SetActive(true);
                break;
            case 3:
                _landMinePower.SetActive(true);
                break;
            case 4:
                _eyePower.SetActive(true);
                break;
            case 5:
                _spikePower.SetActive(true);
                break;
            case 6:
                _gunPower.SetActive(true);
                break;
            
        }
    }
    
    
}
