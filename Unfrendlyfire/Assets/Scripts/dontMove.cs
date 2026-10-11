using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontMove : MonoBehaviour
{



    public Transform leaderObject;
    public float followSpeed = 150;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leaderObject != null)
        {
            // Calculate the position at the end of the leader object (using its position + offset)
            Vector3 endPosition = leaderObject.position + leaderObject.transform.rotation * offset;

            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, endPosition, followSpeed * Time.deltaTime);



        }
    }
}
