using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Disc_Script : MonoBehaviour
{
    public float speed = 4f; // Overall speed
    public Vector2 moveDirection = new Vector2(1, 1); // Initial movement direction

    public float Hits = 5;
    
    //scripts
    CreateItem rockCs;

    public float timeToWait = 1f;

    public GameObject sound;

    private void Update()
    {
        if (timeToWait >= 0)
        {
            timeToWait -= 1 * Time.deltaTime;
        }

        if (timeToWait <= 0.01f)
        {
            if (Hits <= 1)
            {
                Destroy(gameObject);
            }
        }
        
        transform.Rotate(0f, 0f, transform.rotation.z + speed);

        // Move object

       // if (canMove)
       // {
            transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
       // }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Rock"))
        {
            rockCs = collision.gameObject.GetComponent<CreateItem>();
            rockCs.Create();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (timeToWait <= 0.01f)
            {
                Hits -= 1;
                Debug.Log("HitWall");
                Vector2 normal = collision.contacts[0].normal;
                moveDirection = Vector2.Reflect(moveDirection, normal);
            }
        }else if (!collision.gameObject.CompareTag("Player"))
        {
            
            Hits -= 1;
            Debug.Log("HitWall");
            if (collision.contactCount > 0)
            {
                // Get the first contact's normal
                Vector2 normal = collision.GetContact(0).normal;

                // Reflect the movement direction based on the collision normal
                moveDirection = Vector2.Reflect(moveDirection, normal);
                Instantiate(sound, transform.position, Quaternion.identity);
            }
        }




    }
}
