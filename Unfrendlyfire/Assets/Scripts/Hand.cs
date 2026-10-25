using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    
    public Transform leaderObject;
    public Vector3 offset;
    public float followSpeed = 5f;
    
    public float walkLeftOffset;
    public float walkRightOffset;
    
    PlayerMovement PlayerSk;
    // Start is called before the first frame update
    void Start()
    {
        PlayerSk = leaderObject.gameObject.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (leaderObject != null)
        {
            
            if (PlayerSk.movementInput.x >= 0.1f)
            {
                offset.x = walkRightOffset;
                transform.localScale = new Vector3(1, 1, 1);

            }
            if (PlayerSk.movementInput.x <= -0.1f)
            {
                offset.x = walkLeftOffset;
                transform.localScale = new Vector3(-1, 1, 1);
                
            }
           
            // Calculate the position at the end of the leader object (using its position + offset)
            Vector3 endPosition = leaderObject.position + leaderObject.transform.rotation * offset;

            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, endPosition, followSpeed * Time.deltaTime);
           
        }
    }
}
