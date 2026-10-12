using System.Collections.Generic;
using UnityEngine;

public class CamraSc : MonoBehaviour
{
    public List<Transform> players;
    public float minSize = 5f;           // Minimum camera size
    public Vector2 padding;           // Extra space around players
    public float smoothSpeed = 5f;       // Camera smoothing

    private Camera cam;
    
    
    private Transform _topBoundary;
    private Transform _bottomBoundary;
    private Transform _leftBoundary;
    private Transform _rightBoundary;
    

    void Start()
    {
        cam = GetComponent<Camera>();
        
        var bounds = GameObject.FindGameObjectWithTag("BoundsManager");
        var sc = bounds.GetComponent<BoundsScr>();
        
        _topBoundary = sc.boundsTop;
        _bottomBoundary = sc.boundsBottom;
        _leftBoundary = sc.boundsLeft;
        _rightBoundary = sc.boundsRight;
        
    }
    // Update is called once per frame
    
    
    
    void LateUpdate()
    {
        if (players.Count == 0) return;

        Vector3 center = GetCenterPoint();

        // Find the maximum zoom allowed by the level boundaries.
        float maxSize = GetMaxCameraSize();

        // Calculate the zoom needed to fit all players.
        float requiredSize = GetRequiredSize(center);

        // Never request a zoom larger than the boundaries allow.
        float targetSize = Mathf.Min(requiredSize, maxSize);

        // Smooth the zoom toward the limited target.
        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize,
            targetSize,
            Time.deltaTime * smoothSpeed
        );

        // Follow the players.
        Vector3 targetPosition = new Vector3(
            center.x,
            center.y,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            Time.deltaTime * smoothSpeed
        );

        // Keep the camera inside the boundaries.
        ClampCameraPosition();
    }



    void ClampCameraPosition()
    {
        // Half the camera's visible height and width
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        // Calculate the allowed camera center
        float minX = _leftBoundary.position.x + halfWidth;
        float maxX = _rightBoundary.position.x - halfWidth;

        float minY = _bottomBoundary.position.y + halfHeight;
        float maxY = _topBoundary.position.y - halfHeight;

        Vector3 pos = transform.position;

        // Clamp only when the camera fits inside the level.
        if (minX <= maxX)
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
        else
            pos.x = (_leftBoundary.position.x + _rightBoundary.position.x) * 0.5f;

        if (minY <= maxY)
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
        else
            pos.y = (_bottomBoundary.position.y + _topBoundary.position.y) * 0.5f;

        transform.position = pos;
    }

    
    float GetMaxCameraSize()
    {

        float levelHeight =
            _topBoundary.position.y - _bottomBoundary.position.y;

        float levelWidth =
            _rightBoundary.position.x - _leftBoundary.position.x;

        // Orthographic size is half the visible vertical height.
        float maxSizeByHeight = levelHeight * 0.5f;
        float maxSizeByWidth = (levelWidth * 0.5f) / cam.aspect;

        return Mathf.Max(
            minSize,
            Mathf.Min(maxSizeByHeight, maxSizeByWidth)
        );
    }

    
    /*
    void LateUpdate()
    {
        
        if (players.Count == 0) return;

        // 1. Find center point of all players
        Vector3 center = GetCenterPoint();

        // 2. Move camera toward center
        Vector3 newPos = new Vector3(center.x, center.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * smoothSpeed);

        // 3. Adjust zoom to fit all players
        var newSize = GetRequiredSize(center);
        
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newSize, Time.deltaTime * smoothSpeed);
    }*/
    
    Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
            return players[0].position;

        Bounds bounds = new Bounds(players[0].position, Vector3.zero);
        foreach (Transform p in players)
            bounds.Encapsulate(p.position);

        return bounds.center;
    }

    float GetRequiredSize(Vector3 center)
    {
        float sizeY = 0f;
        float sizeX = 0f;

        foreach (Transform p in players)
        {
            Vector3 localPos = transform.InverseTransformPoint(p.position);
            Vector3 centerLocal = transform.InverseTransformPoint(center);

            float distanceY = Mathf.Abs(localPos.y - centerLocal.y);
            sizeY = Mathf.Max(sizeY, distanceY);
            float distanceX = Mathf.Abs(localPos.x - centerLocal.x);
            sizeX = Mathf.Max(sizeX, distanceX);
        }
        var X = Mathf.Max(sizeX + padding.x, minSize);
        var Y = Mathf.Max(sizeY + padding.y, minSize);
        var result = 0f;
        if (X >= Y)
        {
            result = X;
        }
        else
        {
            result = Y;
        }
        
        return result;
    }
}
