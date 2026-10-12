using UnityEngine;

public class ArrowScr : MonoBehaviour
{
    [Header("References")]
    public GameObject baseObject;
    public GameObject arrowObject;
    public Transform player;

    [Header("Settings")]
    public float orbitDistance = 1f;
    public float rotationOffset = 0f;

    void LateUpdate()
    {
        if (baseObject == null || arrowObject == null || player == null)
            return;

        // Find the direction from the base to the player.
        Vector2 direction = player.position - baseObject.transform.position;

        if (direction.sqrMagnitude < 0.0001f)
            return;

        direction.Normalize();

        // Position the arrow around the base, toward the player.
        Vector3 arrowPosition = baseObject.transform.position;

        arrowPosition.x += direction.x * orbitDistance;
        arrowPosition.y += direction.y * orbitDistance;

        // Preserve the arrow's existing Z position.
        arrowPosition.z = arrowObject.transform.position.z;
        arrowObject.transform.position = arrowPosition;

        // Rotate the arrow to face the player.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        arrowObject.transform.rotation = Quaternion.Euler(
            0f,
            0f,
            angle + rotationOffset
        );
    }
}
