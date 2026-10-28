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

    private int _playerNum;
    private ItemsToHold_sc _powerHoldSc;
    
    public List<Power> powers = new();
    public bool hasItem;
    public int powerNum;

    public GameObject disc;
    private Disc_Script _discSk;
    public GameObject landMine;
    
    private ItemDisplayScr _itemDisplayScr;
    
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
            _itemDisplayScr = _managerSc.powerUIScr[_playerNum];
            _itemDisplayScr.HideSprite();
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
                _itemDisplayScr.HideSprite();
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
        _managerSc.SpawnBigRock();
        Debug.Log("Used Rock");
    }
    
    void UseDisk()
    {
       var newObject = Instantiate(disc, transform.position, Quaternion.identity);
        _discSk = newObject.GetComponent<Disc_Script>();
        _discSk.moveDirection = _playerMovement.LastVelocity;
        Debug.Log("Used Disk");
    }
    
    void UseLandMine()
    {
        Instantiate(landMine, transform.position, Quaternion.identity);
        Debug.Log("Used Land Mine");
    }
    
    void UseEye()
    {
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
        List<PlayerMovement> players = _managerSc.PlayerSc;
        foreach (var player in players)
        {
            player.MakeSpikeBat();
        }
        
        SetTimer(0);
    }

    void UseGun()
    {
        List<PlayerMovement> players = _managerSc.PlayerSc;
        foreach (var player in players)
        {
            player.MakeGun();
        }
        
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
        _itemDisplayScr.HideSprite();
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
                        ShowUI(power.powerNum-1);
                        Destroy(other);
                        return;
                    }
                }
            }
        }
    }
    
    void ShowUI(int power)
    {
        _itemDisplayScr.ShowSprite(power);
    }
    
    
}
