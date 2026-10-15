using System;
using System.Collections.Generic;
using UnityEngine;

public class OOB_indacater : MonoBehaviour
{
    
    [Header("Target")]
    public Transform player;
    
    //Boundaries
    private Transform _topBound;
    private Transform _bottomBound;
    private Transform _leftBound;
    private Transform _rightBound;

    public SpriteRenderer baseRenderer;
    public SpriteRenderer arrowRenderer;

    public ArrowScr arrowScr;
   
    public float offset = 0f;

    public Animator anim;
    
    public List<Sprite> baseSprites;
    public List<Sprite> arrowSprites;

    [NonSerialized]public int ColorInt;

    [Header("Follow Settings")]
    public float smoothSpeed = 10f;

    void Start()
    {
        var bounds = GameObject.FindGameObjectWithTag("BoundsManager");
        var sc = bounds.GetComponent<BoundsScr>();
        
        _topBound = sc.boundsTop;
        _bottomBound = sc.boundsBottom;
        _leftBound = sc.boundsLeft;
        _rightBound = sc.boundsRight;
        arrowScr.player = player;
        
        Hide(true);
        baseRenderer.sprite = baseSprites[ColorInt];
        arrowRenderer.sprite = arrowSprites[ColorInt];
        
    }

    public void Hide(bool hide)
    {
        if (hide)
        {
            baseRenderer.enabled = false;
            arrowRenderer.enabled = false;
        }
        else
        {
            baseRenderer.enabled = true;
            arrowRenderer.enabled = true;
        }
    }

    void LateUpdate()
    {
        if (player == null)
        {
            return;
        }

        // Calculate the allowed movement boundaries.
        float minX = _leftBound.position.x + offset;
        float maxX = _rightBound.position.x - offset;

        float minY = _bottomBound.position.y + offset;
        float maxY = _topBound.position.y - offset;

        // Handle boundaries that overlap because of offsets.
        if (minX > maxX)
            minX = maxX = (minX + maxX) / 2f;

        if (minY > maxY)
            minY = maxY = (minY + maxY) / 2f;

        // Follow the player's position, clamped to the boundaries.
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(player.position.x, minX, maxX),
            Mathf.Clamp(player.position.y, minY, maxY),
            transform.position.z
        );

        // Smoothly move toward the target.
        Vector3 newPosition = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        // Clamp again so smoothing never takes it outside the bounds.
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

        transform.position = newPosition;
    }
}
