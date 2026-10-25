
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayerMovement : MonoBehaviour
{

    private PlayerConfig _playerConfig;
    private PlayerControls _controls;
    
    private Animator _animator;

    public GameObject CameraObj;
    private CamraSc CameraSc;

    //Vector2
    public Vector2 movementInput;    // Input from the player
    public Vector2 velocity;
    [NonSerialized]public Vector2 LastVelocity;
    Vector2 position;
    

    //Scripts
    private Maneger ManagerSK;
    private RoundManagerScr _roundManagerScr;
    private ItemHandler _itemHandler;
    
    private FollowPlayer _followScr;
    
    private WeaponScr _weaponScr;
    
    
    private Hand Hand_sc;
    [NonSerialized] public ItemsToHold_sc PowerHoldSc;
    private Disc_Script Disc_sk;
    private destroy Part_sk;
    private SpawnIn DethPart;

    //Game Objects

        // public
    public GameObject dethPartical;
    public GameObject Sword;
    public GameObject Hand;
    public GameObject Disc;
    public GameObject LandMine;
    public GameObject dethSpot;
    public GameObject spawnPoint;
    public GameObject dethPart;
    public GameObject player;
    
    public GameObject DethSound;
    

    //Private
    private GameObject manager;

    public GameObject newHand;

    // floats

        //Public

          //speed
    public float MoveSpeed = 3f;// Base movement speed
    
    public float SprintSpeed = 5f;
    
    public float slowMoveSpeed = 1f;
    private float defaltMoveSpeed;
    
    public float topspeed = 5f;// Max speed limit
    
    public float TopSprint = 7f;
    
    public float slowTopSpeed = 3f;
    private float defaltTopSpeed;
    public float stopspeed = 0.1f;// Deceleration with no input
    public float SwichingSpeed = 0.4f;// Deceleration with input
    public float moveSpeedX = 0f;// Current speed on X axis
    public float moveSpeedY = 0f;// Current speed on Y axis

    float lastVelocityX;
    float lastVelocityY;


    public float amountOfForce;
    private float baseForce;
    
    public int PlayerNUM = 0;
    public float RotationSpe;

    public float ResponTime = 3;
    public float timeToWaite;
    //Private 
    float dist;
    float HitForce;
    private double cooldown;

    //int
    int coler_index;

    //Rigid-body
    public Rigidbody2D rigidbody2d;   // Reference to Rigidbody2D

    // bool
    public bool canDash = true;
    public bool canMove = true;
    public bool slowMo = false;
    public bool alive = true;
    public bool isSprinting = false;

    public GameObject _playerTrail;
    private SpriteRenderer _spriteRenderer;
    //Sprite
    [NonSerialized]public Sprite GhostSp;
    [NonSerialized]public Sprite BaiseColor;
    [NonSerialized]public Sprite SwordSpr;
    [NonSerialized]public Sprite HandSprite;

    //Transform
    // private
    Transform Player_obj;

    private SpriteRenderer spriteRenderer;
    // public

    private Transform _boundsTop;
    private Transform _boundsBottom;
    private Transform _boundsRight;
    private Transform _boundsLeft;
    
    private bool _outOfBounds;
    private OOB_Handeler _oobScr;
  
    public void UnDie()
    {
        alive = true;
        
        spriteRenderer.sprite = BaiseColor;
        _weaponScr.Activate();
        Hand_sc.gameObject.SetActive(true);
        _animator.SetBool("IsDead",false);
    }

    public void Dash()
    {
        if (alive && canMove)
        {
            isSprinting = !isSprinting;
        }
        else
        {
            isSprinting = false;
        }
    }
    

    public void UnDash()
    {
        isSprinting = !isSprinting;
    }
    
  
    public void MakeSpikeBat()
    {
        _weaponScr.BecomeSpikeBat();
    }

    public void MakeGun()
    {
        _weaponScr.BecomeGun();
    }

    void Awake()
    {
        _controls = new PlayerControls();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        dethSpot = GameObject.FindWithTag("DethSpot");
        
        CameraObj = GameObject.FindWithTag("MainCamera");
        CameraSc = CameraObj.GetComponent<CamraSc>();
        CameraSc.players.Add(transform);

        _animator = GetComponent<Animator>();
        
        baseForce = amountOfForce;
        
        defaltTopSpeed = topspeed;
        defaltMoveSpeed = MoveSpeed;
        
        var newSowrd = Instantiate(Sword, transform.position, Quaternion.identity);
        
        Collider2D playerCollider = GetComponent<Collider2D>();
        Collider2D weaponCollider = newSowrd.GetComponent<Collider2D>();

        if (playerCollider != null && weaponCollider != null)
        {
            Physics2D.IgnoreCollision(playerCollider, weaponCollider, true);
        }
        else
        {
            Debug.LogError("Player or weapon collider was not found!");
        }
        
        newHand = Instantiate(Hand, transform.position, Quaternion.identity);

         _spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody2d = GetComponent<Rigidbody2D>();

        
        _followScr = newSowrd.GetComponent<FollowPlayer>();
       
        _weaponScr = newSowrd.GetComponent<WeaponScr>();
        _weaponScr.PlayerNumber = PlayerNUM;
        
        Hand_sc = newHand.GetComponent<Hand>();
        
        _weaponScr.PlayerObj = gameObject;
        _weaponScr.Initialize();
        
        _followScr.leaderObject = transform;
        
        
        Hand_sc.leaderObject = transform;
        if (manager == null)
        {
            manager = GameObject.FindWithTag("Manager");
            ManagerSK = manager.GetComponent<Maneger>(); 
        }
        
        if (GameObject.FindGameObjectWithTag("Manager") != null)
        {
            ManagerSK.PlayerSc.Add(GetComponent<PlayerMovement>());
            
            _itemHandler = gameObject.GetComponent<ItemHandler>();
            
            PowerHoldSc = newHand.GetComponent<ItemsToHold_sc>();
            
            ManagerSK.Player.Add(gameObject);
        }
        
        var bounds = GameObject.FindGameObjectWithTag("BoundsManager");
        var boundsScr = bounds.GetComponent<BoundsScr>();
        
        _boundsTop = boundsScr.boundsTop;
        _boundsBottom = boundsScr.boundsBottom;
        _boundsRight =  boundsScr.boundsRight;
        _boundsLeft = boundsScr.boundsLeft;
        // Initialize Rigidbody2D
        _roundManagerScr = ManagerSK.roundManager;
        
    }


    public void InitializePlayer(PlayerConfig pc)
    {
        if (manager == null)
        {
            manager = GameObject.FindWithTag("Manager");
            ManagerSK = manager.GetComponent<Maneger>();
        }
        
        _playerConfig = pc;
        _playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        PlayerNUM = _playerConfig.PlayerIndex;
        SetColors();
    }

    private void SetColors()
    {
        coler_index = _playerConfig.ColorIndex;
        
        var scr = gameObject.GetComponent<PlayerMovement>();
        ManagerSK.SetColors(coler_index, scr);
        
        _spriteRenderer.sprite = BaiseColor;
        
        var swordSpr = _followScr.GetComponentInChildren<SpriteRenderer>();
        swordSpr.sprite = SwordSpr;
        
        var handSpr = newHand.GetComponent<SpriteRenderer>();
        handSpr.sprite = HandSprite;
        
        _oobScr = GetComponent<OOB_Handeler>();
        _oobScr.ColorInt = coler_index;
        
        _weaponScr.SetColor(coler_index);
    }

    private void Input_onActionTriggered(InputAction.CallbackContext context)
    {
        if (context.action.name == _controls.Player.Move.name)
        {
            OnMove(context);
        }

        if (context.action.name == _controls.Player.Dash.name)
        {
            Dash();
        }

        if (context.action.name == _controls.Player.UNDash.name)
        {
            UnDash();
        }

        if (context.action.name == _controls.Player.UseItem.name)
        {
            _itemHandler.UsePower();
        }

        if (context.action.name == _controls.Player.Attack.name)
        {
            Attack();
        }

        if (context.action.name == _controls.Player.Continue.name)
        {
            Continue();
        }
    }

    // Called by the Input System when the player gives movement input
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // Capture input from the Input System (Vector2)
        movementInput = ctx.ReadValue<Vector2>();
    }
    public void Attack()
    {
        if (alive && cooldown <= 0)
        {
            _weaponScr.Attack();
            cooldown = 0.1;
        }
    }
    void FixedUpdate()
    {

        if (canMove)
        {
            velocity = new Vector2(moveSpeedX, moveSpeedY);
            
            position = rigidbody2d.position;
            position += velocity * Time.fixedDeltaTime;
            rigidbody2d.MovePosition(position);
        }
        
    }

    void checkState()
    {
        if (movementInput.x >= 0.1f)
        {
            lastVelocityX = 1;
            moveRight();
            StopVelocityY();
        }
        else if (movementInput.x <= -0.1f)
        {
            lastVelocityX = -1;
            moveLeft();
            StopVelocityY();
        }
        else
        {
            if (moveSpeedX > 1)
            {
                moveSpeedX -= stopspeed * Time.deltaTime;
            }
            else
            {
                moveSpeedX += stopspeed * Time.deltaTime;
            }
                
            if (Math.Abs(moveSpeedX) < 1)
            {
                moveSpeedX = 0;
            }
        }
            
        if (movementInput.y >= 0.1f)
        {
            StopVelocityX();
            moveDown();
        }
        else if (movementInput.y <= -0.1f)
        {
            StopVelocityX();
            moveUp();
        }
        else
        {
            
            if (moveSpeedY > 1)
            {
                moveSpeedY -= stopspeed * Time.deltaTime;
            }
            if (moveSpeedY < -1)
            {
                moveSpeedY += stopspeed * Time.deltaTime;
            }
            if (Math.Abs(moveSpeedY) < 1)
            {
                moveSpeedY = 0;
            }
        }
        
    }

    void StopVelocityY()
    {
        if (movementInput.y is <= 0.1f and >= -0.1f)
        {
            lastVelocityY = 0;
        }
    }

    void StopVelocityX()
    {
        if (movementInput.x is <= 0.1f and >= -0.1f)
        {
            lastVelocityX = 0;
        }
    }

    void moveRight()
    {
        // Debug.Log("Moving X");
        lastVelocityX = 1;
        

        if (moveSpeedX < topspeed)
        {
            if (moveSpeedX <= -0.1)
            {
                moveSpeedX += SwichingSpeed * Time.deltaTime;
            }
            else
            {
                moveSpeedX += 20f * Time.deltaTime;
            }
        }else if (moveSpeedX > topspeed)
        {
            moveSpeedX = topspeed;
        }
    }

    void moveLeft()
    {
        lastVelocityX = -1;

        if (moveSpeedX > -topspeed)
        {
            if (moveSpeedX >= 0.1)
            {
                moveSpeedX -= SwichingSpeed * Time.deltaTime;
            }
            else
            {
                moveSpeedX -= 20f * Time.deltaTime;
            }
        }else if (moveSpeedX < -topspeed)
        {
            moveSpeedX = -topspeed;
        }
    }

    void moveUp()
    {
        lastVelocityY = -1;

        if (moveSpeedY > -topspeed)
        {
            if (moveSpeedY >= 0.1f)
            {
                moveSpeedY -= SwichingSpeed * Time.deltaTime;
            }
            else
            {
                moveSpeedY -= 20f * Time.deltaTime;
            }
        }else if (moveSpeedY < -topspeed)
        {
            moveSpeedY = -topspeed;
        }
    }

    void moveDown()
    {
        lastVelocityY = 1;

        if (moveSpeedY < topspeed)
        {
            if (moveSpeedY <= -0.1f)
            {
                moveSpeedY += SwichingSpeed * Time.deltaTime;
            }
            else
            {
                moveSpeedY += 20f * Time.deltaTime;
            }
        }else if (moveSpeedY > topspeed)
        {
            moveSpeedY = topspeed;
        }
    }

    void Update()
    {
        var x = transform.position.x;
        var rX = _boundsRight.transform.position.x;
        var lX = _boundsLeft.transform.position.x;
        var y = transform.position.y;
        var tY = _boundsTop.transform.position.y;
        var bY = _boundsBottom.transform.position.y;
        if (x > rX || x < lX || y > tY || y < bY)
        {
            if (canMove && _outOfBounds == false)
            {
                _oobScr.OutOfBounds();
                _outOfBounds = true;
            }
        }
        else
        {
            if (_outOfBounds)
            {
                _oobScr.InBounds();
                _outOfBounds = false;
            }
            
        }
        
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        if (cooldown >= 0)
        {
            cooldown -= 1*Time.deltaTime;
        }

        if (isSprinting)
        {
            topspeed = TopSprint;
            MoveSpeed = SprintSpeed;
        }else if (slowMo)
        {
            topspeed = slowTopSpeed;
            MoveSpeed = slowMoveSpeed;
        }
        else
        {
            topspeed = defaltTopSpeed;
            MoveSpeed = defaltMoveSpeed;
        }
        
        if (!canMove)
        {
            if (ManagerSK.roundEnded == false)
            {
                if (timeToWaite <= Time.time)
                {
                    var newPos = transform.position;
                    newPos.x = spawnPoint.transform.position.x;
                    newPos.y = spawnPoint.transform.position.y;
                    transform.position = newPos;

                    float ranNumx = Random.Range(-10, 10);
                    moveSpeedX = ranNumx;

                    float ranNumy = Random.Range(-10, 10);
                    moveSpeedY = ranNumy;

                    var spawnPart = Instantiate(dethPart, dethSpot.transform.position, Quaternion.identity);

                    var deathPartComponent = spawnPart.GetComponent<SpawnIn>();


                    deathPartComponent.leaderObject = this.gameObject;
                    deathPartComponent.index = coler_index;

                    if (!CameraSc.players.Contains(transform))
                    {
                        CameraSc.players.Add(transform);
                    }

                    int player = PlayerNUM;
                    ManagerSK.PlayerRevived(player);
                    canMove = true;
                }
            }

        }

        if (canMove)
        {
            LastVelocity = new Vector2(lastVelocityX, lastVelocityY);
            
            if (isSprinting)
            {
                var trail = Instantiate(_playerTrail,  transform.position, Quaternion.identity);
                var trailSprite = trail.GetComponent<SpriteRenderer>();
                trailSprite.sprite = _spriteRenderer.sprite;
            }
            checkState();
        }

    }

    public void ClearInventory()
    {
        _itemHandler.ClearInventory();
    }
    
    public void ResetBat()
    {
        _weaponScr.ResetBat();
    }

    void Respawn()
    {

        if (!RuleScr.UseWeaponPowerTimers)
        {
            _weaponScr.ResetBat();
        }
        
        ClearInventory();
        alive = false;
        Store();
        CameraSc.players.Remove(transform);
        timeToWaite = Time.time + ResponTime;
    }

    public void Spawn(Transform Xmin, Transform Xmax, Transform Ymin, Transform Ymax )
    {
        timeToWaite = 100;
        var newPos = dethSpot.transform.position;
        newPos.x = Random.Range(Xmin.position.x, Xmax.position.x);
        newPos.y = Random.Range(Ymin.position.y, Ymax.position.y);
        transform.position = new Vector3(newPos.x,newPos.y,transform.position.z);
        if (!CameraSc.players.Contains(transform))
        {
            CameraSc.players.Add(transform);
        }
        canMove = true;
    }

    public void Store()
    {
        CameraSc.players.Remove(transform);
        
        _itemHandler.hasItem = false;
        
        //Debug.Log("Stored player");
        var newPos = transform.position;
        newPos.x = dethSpot.transform.position.x;
        newPos.y = dethSpot.transform.position.y;
       
        transform.position = newPos;
        
        canMove = false;
        
        moveSpeedY = 0;
        moveSpeedX = 0;
    }
    
    public void Death()
    {
        if (alive)
        {
            Instantiate(DethSound, transform.position, Quaternion.identity);
            spriteRenderer.sprite = GhostSp;
            _animator.SetBool("IsDead",true);
            _weaponScr.Deactivate();
            Hand_sc.gameObject.SetActive(false);
        
            _itemHandler.hasItem = false;

            if (!RuleScr.UseWeaponPowerTimers)
            {
                _weaponScr.ResetBat();
            }
        
            int num = PlayerNUM;
            ManagerSK.PlayerDied(num);
            alive = false;
            canMove = false;
        

            var times = 4;
            for (int i = 0; i < times; i++)
            {
                var ranx = Random.Range(-0.5f, 0.5f);
                var rany = Random.Range(-0.5f, 0.5f);
                var pos = new Vector3(transform.position.x + ranx, transform.position.y + rany, transform.position.z);
            
                var NewPartical = Instantiate(dethPartical, pos, Quaternion.identity);
                Part_sk = NewPartical.GetComponent<destroy>();
                Part_sk.index = coler_index;
            }
        
            Respawn();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.CompareTag("Disc"))
            {
                Disc_sk = collision.gameObject.GetComponent<Disc_Script>();
                if (Disc_sk.timeToWait <= 0.45f)
                {
                    Death();
                }
            }
            if (collision.gameObject.CompareTag("BigRock"))
            {
                Death();
            }
            
            if (collision.gameObject.CompareTag("Keycard") && alive)
            {
                int playerInt = PlayerNUM;
                ManagerSK.Collect(playerInt);
                Destroy(collision.gameObject);
            }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("weapon"))
        {
            var sk = other.gameObject.GetComponent<SwordSc>();
            if (sk.atacking)
            {
                if (sk.isSpikeBat && sk.playerNumber != PlayerNUM)
                {
                    Death();
                }
                else if (alive)
                {
                    amountOfForce = 0;
               
                    HitForce = sk.HitForce;
                
                    Player_obj = sk.leaderTransform;

                    var heading = transform.position - Player_obj.transform.position;
                
                    // dist = Vector3.Distance(transform.position, Player_obj.gameObject.transform.position);
                    amountOfForce = HitForce;

                    MoveBack(heading);
                }
            }
        }
    }

    public void Continue()
    {
        if (_roundManagerScr.ShowContinue)
        {
            _weaponScr.ResetBat();
            ManagerSK.Continue();
        }
        
    }

    void MoveBack(Vector3 forceDirection)
    {
        //  Debug.Log("ATTAKED");
        if (amountOfForce > 0)
        {
            moveSpeedX = (forceDirection.x * amountOfForce / 2);
            moveSpeedY = (forceDirection.y * amountOfForce / 2);
        }
    }

    // Adjusts speed based on input and current speed

}
