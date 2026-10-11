using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public float moveSpeedY;
    public float moveSpeedX;
    public float topspeed;
    public float stopspeed;


    public bool Canbounce = false;
    public float bounce;
    public float bounceSpeed;

    public string myKeyup = "";
    public string myKeydown = "";
    public string myKeyleft = "";
    public string myKeyright = "";
    public string myKeyShoot = "";
    
    public float RotationSpe;

    float Rotation;
    Vector2 position;
    Quaternion StarRo;
    Vector2 velocity;
    void Start()
    {


        // Get the Rigidbody2D component attached to the GameObject
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetKey(myKeyup))
        {

            
            velocity = new Vector2(moveSpeedX, moveSpeedY);

            if (moveSpeedY < topspeed)
            {

                moveSpeedY += 0.1f;
            }
        }
        if (Input.GetKey(myKeydown))
        {

            
            velocity = new Vector2(moveSpeedX, moveSpeedY);

            if (moveSpeedY > topspeed * -1)
            {

                moveSpeedY -= 0.1f;
            }
        }
        
        if (!Input.GetKey(myKeydown) && !Input.GetKey(myKeyup))
        {
            if (moveSpeedY > 0)
            {
                moveSpeedY -= stopspeed;
            }
            if (moveSpeedY < 0)
            {
                moveSpeedY += stopspeed;
            }
            if (moveSpeedY < 1 && moveSpeedY > -1)
            {
                moveSpeedY = 0;
            }
        }

        //if (Input.GetKey(myKeyleft))
        // {
        if (Input.GetKey(myKeyright))
        {

            
            velocity = new Vector2(moveSpeedX, moveSpeedY);
            if (moveSpeedX < topspeed)
            {

                moveSpeedX += 0.1f;
            }
        }

        if (Input.GetKey(myKeyleft))
        {

            
            velocity = new Vector2(moveSpeedX, moveSpeedY);
            if (moveSpeedX > topspeed * -1)
            {

                moveSpeedX -= 0.1f;
            }
        }
        // }
        if (!Input.GetKey(myKeyright) && !Input.GetKey(myKeyleft))
        {
            if (moveSpeedX > 1)
            {
                moveSpeedX -= stopspeed;
            }
            if (moveSpeedX < -1)
            {
                moveSpeedX += stopspeed;
            }
            if (moveSpeedX < 1 && moveSpeedX > -1)
            {
                moveSpeedX = 0;
            }
        }


        /* if ()
         {

         }*/


    }
   
    void FixedUpdate()
    {

        position = rigidbody2d.position;
        position.x = position.x + moveSpeedX * Time.deltaTime;
        position.y = position.y + moveSpeedY * Time.deltaTime;
        rigidbody2d.MovePosition(position);




        // Calculate the angle of the velocity
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpe * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (Canbounce)
        {
            if (moveSpeedX <= -bounceSpeed)
            {
                Debug.Log("movingLef");
                moveSpeedX = -bounce * (moveSpeedX / 2);
            }
            if (moveSpeedX >= bounceSpeed)
            {
                moveSpeedX = bounce * (-moveSpeedX / 2);
            }
            if (moveSpeedY <= -bounceSpeed)
            {
                
                moveSpeedY = bounce * (moveSpeedY / 2);
            }
            if (moveSpeedY >= bounceSpeed)
            {
                moveSpeedY = bounce * (-moveSpeedY / 2);
            }
        }
    }
}
