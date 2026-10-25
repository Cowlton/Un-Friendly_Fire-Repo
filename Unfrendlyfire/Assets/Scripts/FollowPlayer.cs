using System;
using System.Collections;
using System.Collections.Generic;using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float walkLeftOffset;
    public float walkRightOffset;
    
    private ColorControllSC _colorCon;
    
    // the values for the rotation of the CurrentWeapon
    public float Left;
    public float Right;
    public float Up;
    public float Down;
    
    
    // The object that the follower will track
    public Transform leaderObject;

    private GameObject _manager;

    private Maneger _managerSk;
    private CreateItem _rockCs;

    // Offset for the end of the leader object, relative to its transform
    public Vector3 offset;
    public float rotationOffset = -50;
    public float currentRotationOffset = -50;

    // Speed at which the follower will move toward the end of the leader
    public float followSpeed = 5f;
    public float rotationSpeed = 5f;
    private PlayerMovement _playerSk;
    
    private float _rotation;
    public float rSpeed;
    
    private SpriteRenderer _spriteRenderer;

    [NonSerialized]public bool IsBat = true;

    void Start()
    {
        _playerSk = leaderObject.gameObject.GetComponent<PlayerMovement>();
    }
    
    void Update()
    {
       
        if ( currentRotationOffset < rotationOffset)
        {
            currentRotationOffset += rSpeed * Time.deltaTime;
        }
        else if (currentRotationOffset > rotationOffset)
        {
            currentRotationOffset -= rSpeed * Time.deltaTime;
        }

        if (currentRotationOffset <= -88 && IsBat)
        {
            currentRotationOffset = -90;
        }
        
        if (math.abs(currentRotationOffset) < 0.5)
        {
            currentRotationOffset = 0;
        }
        
        if (_playerSk.movementInput.x >= 0.1f)
        {
            offset.x = walkRightOffset;
            _rotation = Right;
        }
        if (_playerSk.movementInput.x <= -0.1f)
        {
            offset.x = walkLeftOffset;
            _rotation = Left;
        }
        if (_playerSk.movementInput.y >= 0.1f)
        {
            _rotation = Up;
        }
        if (_playerSk.movementInput.y <= -0.1f)
        {
            _rotation = Down;
        }
        
        if (leaderObject != null)
        {
            // Calculate the position at the end of the leader object (using its position + offset)
            Vector3 endPosition = leaderObject.position + leaderObject.transform.rotation * offset;

            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, endPosition, followSpeed * Time.deltaTime);
            
            Quaternion targetRotation = Quaternion.Euler(0, 0, _rotation + currentRotationOffset); ;

            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
