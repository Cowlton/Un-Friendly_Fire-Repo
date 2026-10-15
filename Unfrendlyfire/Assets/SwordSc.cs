using UnityEngine;

public class SwordSc : MonoBehaviour
{
    
    public bool isSpikeBat = false;
    public SpriteRenderer spikes;

    public GameObject slapSound;
 
    
    // The object that the follower will track
    public Transform leaderObject;
    
    private GameObject _manager;

    private Maneger ManagerSK;
    CreateItem rockCs;

    // Offset for the end of the leader object, relative to its transform
    public float RotationOffset = -50;
    public float CerrentRotationOffset = -50;

    // Speed at which the follower will move toward the end of the leader
    public float rotationSpeed = 5f;
    PlayerInput PlayerSk;

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

    
    
    
    void Start()
    {
        
    }
    
    public void Attack()
    {
        PauseTime = 2;
        Atacking = true;
        if (RotationOffset == 0)
        {
            RotationOffset = -90;
        }else if (RotationOffset < -80)
        {
            RotationOffset = 0;
        }

        Debug.Log("Attacked");
        
    }
    void HitRock(GameObject collision)
    {
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
            Instantiate(slapSound, collision.transform.position, transform.rotation);

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

    // Update is called once per frame
    
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
        
        
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotation + CerrentRotationOffset); ;

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}
