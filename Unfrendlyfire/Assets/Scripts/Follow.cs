using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    // The object that the follower will track
    public Transform leaderObject;

    // Offset for the end of the leader object, relative to its transform
    public Vector3 offset;

    // Speed at which the follower will move toward the end of the leader
    public float followSpeed = 5f;

   

    // Speed at which the follower will rotate to match the leader's rotation
    public float rotationSpeed = 5f;

    void Update()
    {


        if (leaderObject != null)
        {
            // Calculate the position at the end of the leader object (using its position + offset)
            Vector3 endPosition = leaderObject.position + leaderObject.rotation * offset;

            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, endPosition, followSpeed * Time.deltaTime);

            // Smoothly rotate the follower to match the leader's rotation
            Quaternion targetRotation = leaderObject.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
   
}
