using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public float playerNumber;
    
    public float walkLeftOffset;
    public float walkRightOffset;

    public bool isSpikeBat = false;
    public SpriteRenderer spikes;

    public GameObject SlapSound;
    
    public GameObject colorChanger;
    private ColorControllSC _colorCon;
    
    public int colorIndex;
    
    // the values for the rotation of the sword
    public float Left;
    public float Right;
    public float Up;
    public float Down;
    // The object that the follower will track
    public Transform leaderObject;

    public Animator animator;
    private GameObject manager;

    private Maneger ManagerSK;
    CreateItem rockCs;

    // Offset for the end of the leader object, relative to its transform
    public Vector3 offset;
    public float RotationOffset = -50;
    public float CerrentRotationOffset = -50;

    // Speed at which the follower will move toward the end of the leader
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;
    PlayerInput PlayerSk;
    public GameObject Trial;

    public GameObject rockId;
    public GameObject SowrdTrail;
   
    public float HitForce;
    public float HitForceR;
    float StHitForce;
    public bool Atacking = false;
    
    public bool hittingRock = false;

    float rotation;
    public float RSPEED;
    float PauseTime = 1;
    
    private SpriteRenderer _spriteRenderer;
    public GameObject sprite;
    void Start()
    {
        
        // temp
        spikes.enabled = false;
        // **

        gameObject.SetActive(true);
        StHitForce = HitForce;
        PlayerSk = leaderObject.gameObject.GetComponent<PlayerInput>();

        if (GameObject.FindWithTag("Manager") != null)
        {
            manager = GameObject.FindWithTag("Manager");
            ManagerSK = manager.GetComponent<Maneger>();
        }
        
        
        

       // GameObject trailObject = Instantiate(Trial, transform.position, Quaternion.identity);

       if (GameObject.FindWithTag("ColorCon") != null)
       {
           colorChanger = GameObject.FindWithTag("ColorCon");
        
           _colorCon =  colorChanger.GetComponent<ColorControllSC>();
       }
       _spriteRenderer =  sprite.GetComponent<SpriteRenderer>();
       _spriteRenderer.sprite = _colorCon.sowrdSprites[colorIndex];
       
       /* var trail = trailObject.GetComponent<TrailRenderer>();
        
        var color = _colorCon.GetColor(colorIndex);
        trail.startColor = color;
        
        Follow Trialsk = trailObject.GetComponent<Follow>();

        Trialsk.leaderObject = transform;*/
        

        Collider2D weaponCollider = GetComponent<Collider2D>();
        Collider2D playerCollider = leaderObject.GetComponent<Collider2D>();

        Physics2D.IgnoreCollision(weaponCollider, playerCollider, true);
        
        
    }

    public void BecomeSpikeBat()
    {
        spikes.enabled = true;
        isSpikeBat = true;
    }

    public void ResetSpikes()
    {
        spikes.enabled = false;
        isSpikeBat = false;
    }
    

    public void Deactivate()
    {
        gameObject.SetActive(false);

    }

    public void Activate()
    {
        gameObject.SetActive(true);

    }
    public void Attack()
    {
        
        /*
            Current Scr Attack
         */
        PauseTime = 2;
        Atacking = true;
        /*
        animator.SetBool("attack", true);
        animator.SetBool("attack", false);*/
        if (RotationOffset == 0)
        {
            RotationOffset = -90;
        }else if (RotationOffset <= -90)
        {
            RotationOffset = 0;
        }

       //Debug.Log("Attacked");
        
    }

    void HitRock(GameObject collision)
    {
        /*
            Delete Collision And Move it to SwordSc
         */
            if (hittingRock == true)
            {
                Vector3 heading = collision.transform.position - leaderObject.transform.position;

                Vector3 forceDirection = heading * -1;

                float dist = Vector3.Distance(leaderObject.transform.position, collision.transform.position);
                float amountOfForce = dist + HitForceR;
                if (amountOfForce > 0)
                {
                    PlayerSk.moveSpeedX = (forceDirection.x * amountOfForce / 2);
                    PlayerSk.moveSpeedY = (forceDirection.y * amountOfForce / 2);
                }
                rockCs = collision.GetComponent<CreateItem>();
                rockCs.Create();
                Instantiate(SlapSound, collision.transform.position, transform.rotation);

            }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        { 
            hittingRock = true;
            rockId = collision.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            
                hittingRock = false;
                rockId = collision.gameObject;
        }
    }


    void Update()
    {
        if (Atacking)
        {
            var trail = Instantiate(SowrdTrail, transform.position, transform.rotation);
            var trailSprite = trail.GetComponent<SpriteRenderer>();
            trailSprite.sprite = _spriteRenderer.sprite;
            if (rockId != null)
            {
                HitRock(rockId);
            }
            HitForce = StHitForce;
        }
        if (PauseTime <= 1)
        {
            Atacking = false;
            HitForce = 0;
        }
        if (PauseTime >= 0)
        {
            PauseTime -= 0.02f;
        }
        
        if (RotationOffset == 0 && CerrentRotationOffset <= 0)
        {
            CerrentRotationOffset += RSPEED * Time.deltaTime;
        }
        else if (RotationOffset < -80 && CerrentRotationOffset >= -90)
        {
            CerrentRotationOffset -= RSPEED * Time.deltaTime;
        }
        
        /*

         float angle = Mathf.Atan2(PlayerSk.velocity.y, PlayerSk.velocity.x) * Mathf.Rad2Deg;
         float rotation = angle + RotationOffset;
        */
         if (PlayerSk.movementInput.x >= 0.1f)
         {
             offset.x = walkRightOffset;
             rotation = Right;

         }
         if (PlayerSk.movementInput.x <= -0.1f)
         {
             offset.x = walkLeftOffset;
             rotation = Left;
         }
         if (PlayerSk.movementInput.y >= 0.1f)
         {
             rotation = Up;
         }
         if (PlayerSk.movementInput.y <= -0.1f)
         {
             rotation = Down;
         }


        if (leaderObject != null)
        {
            // Calculate the position at the end of the leader object (using its position + offset)
            Vector3 endPosition = leaderObject.position + leaderObject.transform.rotation * offset;

            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, endPosition, followSpeed * Time.deltaTime);


            // Smoothly rotate the follower to match the leader's rotation 
            /*
             *Delete and add to swordSc
             * 
             */
           
            Quaternion targetRotation = Quaternion.Euler(0, 0, rotation + CerrentRotationOffset); ;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
