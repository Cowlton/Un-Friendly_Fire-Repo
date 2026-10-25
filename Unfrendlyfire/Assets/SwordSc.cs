using System;
using PlayerScripts;using UnityEngine;

public class SwordSc : IWeapon
{

    public WeaponScr weaponScr;

    [NonSerialized] public FollowPlayer followScr;
    
    public bool isSpikeBat = false;
    public SpriteRenderer spikes;
    public GameObject slapSound;
    
    private GameObject _colorChanger;
    private ColorControllSC _colorCon;
    
    public int colorIndex;
    
    // the values for the rotation of the sword
    public float Left;
    public float Right;
    public float Up;
    public float Down;
    // The object that the follower will track
    public Transform leaderTransform;
    
    private GameObject _manager;

    private Maneger _managerSk;
    private CreateItem _rockCs;

    // Offset for the end of the leader object, relative to its transform
    public float RotationOffset = 0;
    
    public GameObject rockId;
    public GameObject SowrdTrail;
   
    public float HitForce;
    public float HitForceR;
    
    private float _stHitForce;
    
    public bool atacking = false;
    
    public bool hittingRock = false;

    private float _pauseTime = 1;
    
    private SpriteRenderer _spriteRenderer;
    public GameObject sprite;

    public float rotateSpeed;
    
    public int playerNumber;

   
    void Start()
    {
        spikes.enabled = false;
        
        _stHitForce = HitForce;
        
        
        _colorChanger = GameObject.FindWithTag("ColorCon");
        _colorCon = _colorChanger.GetComponent<ColorControllSC>();
        
        _spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _colorCon.sowrdSprites[colorIndex];
        
        
        // Make sure Attack Only effects other players
        Collider2D weaponCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = leaderTransform.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(weaponCollider, playerCollider, true);
    }
    
    public override void Attack()
    {
        _pauseTime = 2;
        atacking = true;
        if (RotationOffset == 0)
        {
            RotationOffset = -90;
        }else if (RotationOffset < -80)
        {
            RotationOffset = 0;
        }
        followScr.rotationOffset = RotationOffset;
        Debug.Log("Attacked");
        
    }
    void HitRock(GameObject collision)
    {
        if (hittingRock)
        { 
            weaponScr.MoveBack(collision, HitForceR);
           _rockCs = collision.GetComponent<CreateItem>();
           _rockCs.Create();
           Instantiate(slapSound, collision.transform.position, transform.rotation);
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            if (atacking)
            {
                hittingRock = true;
                rockId = other.gameObject;
                HitRock(rockId);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Rock"))
        {
            hittingRock = false;
            rockId = other.gameObject;
        }
    }
    
    void Update()
    {
        if (atacking)
        {
           HitForce = _stHitForce;
        }
        if (_pauseTime <= 1)
        {
            atacking = false;
            HitForce = 0;
        }
        if (_pauseTime > 0)
        {
            _pauseTime -= 0.02f;
        }
    }
}
