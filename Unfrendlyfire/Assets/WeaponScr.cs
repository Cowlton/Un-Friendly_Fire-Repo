using System;
using DefaultNamespace;
using PlayerScripts;
using UnityEngine;

public class WeaponScr : MonoBehaviour
{

    private SwordSc _batBehaver;
    private GunScr _gunBehaver;
   
    private PlayerMovement _playerSk;
    [NonSerialized]public int PlayerNumber;
    [NonSerialized]public GameObject PlayerObj;

    private FollowPlayer _followScr;
    private IWeapon _weapon;

    [SerializeField] private GameObject batSprites;
    [SerializeField] private GameObject gunSprites;
    [SerializeField] private HandColorScr handColorScr;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
        
        // Make sure Attack Only effects other players
        Collider2D weaponCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = PlayerObj.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(weaponCollider, playerCollider, true);
    }

    public void Initialize()
    {
        _playerSk = PlayerObj.gameObject.GetComponent<PlayerMovement>();
        
        _followScr = GetComponent<FollowPlayer>();
        _batBehaver = GetComponent<SwordSc>();
        _gunBehaver = GetComponent<GunScr>();
        
        _batBehaver.leaderTransform = PlayerObj.transform;
        _batBehaver.followScr = _followScr;
        _batBehaver.playerNumber = PlayerNumber;
        
        _gunBehaver.PlayerSk = _playerSk;
        _gunBehaver.WeaponScr = GetComponent<WeaponScr>();
        _gunBehaver.FollowScr = _followScr;
        
        _weapon = _batBehaver;
    }

    public void SetColor(int colorIndex)
    {
        _batBehaver.colorIndex = colorIndex;
        handColorScr.Color = colorIndex;
    }

    // Update is called once per frame
    
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        
        Collider2D weaponCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = PlayerObj.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(weaponCollider, playerCollider, true);
    }
    
    public void ResetBat()
    {
        gunSprites.SetActive(false);
        batSprites.SetActive(true);
        
        _batBehaver.spikes.enabled = false;
        _batBehaver.isSpikeBat = false;
        
        _gunBehaver.IsGun =  false;
        
        UpdateRotationOfSets();
        
        transform.localScale = new Vector3(1, 1, 1);

        _followScr.IsBat = true;
        _followScr.rSpeed = _batBehaver.rotateSpeed;
        
        _weapon = _batBehaver;
    }

    public void BecomeSpikeBat()
    {
        ResetBat();
        
        _batBehaver.spikes.enabled = true;
        _batBehaver.isSpikeBat = true;
    }
    public void Attack()
    {
        _weapon.Attack();
        //_followScr.RotationOffset = _weapon.RotationOffset;
    }

    public void BecomeGun()
    {
        batSprites.SetActive(false);
        gunSprites.SetActive(true);
        
        _gunBehaver.IsGun =  true;
        
        _weapon = _gunBehaver;
        UpdateRotationOfSets();
        
        _followScr.IsBat = false;
        _followScr.rSpeed = _gunBehaver.returnSpeed;
    }

    void UpdateRotationOfSets()
    {
        _followScr.rotationOffset = _weapon.RotationOffset;
    }
    
    public void MoveBack(GameObject collision, float backForce)
    {
        Vector3 heading = collision.transform.position - PlayerObj.transform.position;

        Vector3 forceDirection = heading * -1;

        float dist = Vector3.Distance(PlayerObj.transform.position, collision.transform.position);
        float amountOfForce = dist + backForce;
        
        if (amountOfForce > 0)
        {
            _playerSk.moveSpeedX = (forceDirection.x * amountOfForce / 2); 
            _playerSk.moveSpeedY = (forceDirection.y * amountOfForce / 2);
        }
    }
}
