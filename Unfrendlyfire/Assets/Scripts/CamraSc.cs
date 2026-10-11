using System.Collections.Generic;
using UnityEngine;

public class CamraSc : MonoBehaviour
{
    public List<Transform> players;
    public float minSize = 5f;           // Minimum camera size
    public Vector2 padding;           // Extra space around players
    public float smoothSpeed = 5f;       // Camera smoothing

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }
    // Update is called once per frame
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
    }
    
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
