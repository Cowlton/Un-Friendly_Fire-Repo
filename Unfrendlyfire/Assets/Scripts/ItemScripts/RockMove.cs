using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockMove : MonoBehaviour
{
    public Rigidbody2D rigidbody2d;
    CreateItem rockCs;
    Vector2 position;
    public Vector2 velocity;

  
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject,3);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate()
    {
        position = rigidbody2d.position;
        position += velocity * Time.fixedDeltaTime;
        rigidbody2d.MovePosition(position);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Rock"))
        {
            rockCs = collision.gameObject.GetComponent<CreateItem>();
            rockCs.Create();
        }
            
        
    }
}
