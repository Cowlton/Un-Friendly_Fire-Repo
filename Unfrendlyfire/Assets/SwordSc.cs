using System;
using PlayerScripts;using UnityEngine;

public class SwordSc : IWeapon
{

    public WeaponScr weaponScr;

    [NonSerialized] public FollowPlayer followScr;
    
    public bool isSpikeBat;
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

    public float trailWaitTime;
    private float _initialTrailWaitTime;
   
    void Start()
    {
        spikes.enabled = false;
        
        _stHitForce = HitForce;
        
        
        _colorChanger = GameObject.FindWithTag("ColorCon");
        _colorCon = _colorChanger.GetComponent<ColorControllSC>();
        
        _spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = _colorCon.sowrdSprites[colorIndex];
        
        _initialTrailWaitTime =  trailWaitTime;
        
    }
    
    public override void Attack()
    {
        if (!atacking)
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
            if (trailWaitTime <= 0)
            {
                var newTrail = Instantiate(SowrdTrail,transform.position,transform.rotation);
                newTrail.GetComponent<SpriteRenderer>().sprite = _spriteRenderer.sprite;
                trailWaitTime = _initialTrailWaitTime;
            }
            trailWaitTime -= 1 * Time.deltaTime;
           HitForce = _stHitForce;
        }
        if (_pauseTime <= 1)
        {
            atacking = false;
            HitForce = 0;
            trailWaitTime = 0;
        }
        if (_pauseTime > 0)
        {
            _pauseTime -= 0.02f;
        }
    }
}
