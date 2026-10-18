using System;
using UnityEngine;

public class OOB_Handeler : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int AboutToDie = Animator.StringToHash("aboutToDie");
    public GameObject OOB_prefab;
    private OOB_indacater _oobScr;

    public float oob_time;
    public float panicTime;
    private float _timeRemaining;
    private bool _timerRunning;

    private float _speed;
    
    [NonSerialized]public int ColorInt;
    
    public PlayerMovement playerMovementScr;
    private Animator _anim;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var oob = Instantiate(OOB_prefab, transform.position, transform.rotation);
        _oobScr = oob.GetComponent<OOB_indacater>();
        _oobScr.player = transform;
        _oobScr.ColorInt = ColorInt;
        _anim = _oobScr.anim;
        
        playerMovementScr = GetComponent<PlayerMovement>();

    }

    public void OutOfBounds()
    {
        if (!_timerRunning)
        {
            _oobScr.Hide(false);
            StartTimer(oob_time);
        }
    }

    public void InBounds()
    {
        _timerRunning = false;
        _oobScr.Hide(true);
        _anim.SetBool("AboutToDie",false);
        _speed = 0f;
    }
    private void StartTimer(float time)
    {
        _timeRemaining = time;
        _timerRunning = true;
    }

    void Update()
    {
        if (!_timerRunning)
            return;
        _speed += Time.deltaTime;
        _anim.SetFloat(Speed, _speed);
        _timeRemaining -= Time.deltaTime;

        if (_speed > panicTime)
        {
            _anim.SetBool("AboutToDie",true);
        }
        
        if (_timeRemaining <= 0f)
        {
            _timeRemaining = 0f;
            _speed = 0f;
            _anim.SetBool("AboutToDie",false);
            _timerRunning = false;
            TimerFinished();
        }
    }

    void TimerFinished()
    {
        playerMovementScr.Death();
        _oobScr.Hide(true);
    }
}
