using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSc : MonoBehaviour
{

    public Sprite Pressed;
    SpriteRenderer _spriteRenderer;

    [NonSerialized] public bool isArmed = false;
    public float dieTime;

    public GameObject Sound;
    public GameObject partical;
    private Animator animator;

    public float delay = 10f;
    bool isPressed = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (isArmed)
        {
            if (collider.gameObject.CompareTag("BigRock") || (collider.gameObject.CompareTag("Disc")) ||
                collider.gameObject.CompareTag("Explosion"))
            {
                Activate();
            }
            
            if (collider.gameObject.CompareTag("Player"))
            {
                var sc = collider.gameObject.GetComponent<PlayerInput>();
                if (sc.alive)
                {
                    Activate();
                }
            }
           
        }
        
    }
    

    public void Activate()
    {
        if (isArmed)
        {
            isArmed = false;
            animator.SetBool("Pressed", true); 
            isPressed = true;
            Instantiate(Sound, transform.position, Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isPressed && delay > -0.1)
        {
            delay -= 1f * Time.deltaTime;
        }
        if (delay <= 0)
        {
            if (isPressed)
            {
                isPressed = false;
                CreatePartical();
            }
            
        }
    }

   

    public void Armed()
    {
        isArmed = true;
    }

    public void CreatePartical()
    {
        var x = transform.position.x;
        var y = transform.position.y;
        Vector3[] points = new Vector3[]
        {
            new Vector3(x, y, 0f),
            new Vector3(x + 1f, y, 0f),
            new Vector3(x + 0.7f, y + 0.7f, 0f),
            new Vector3(x, y + 1f, 0f),
            new Vector3(x - 0.7f, y + 0.7f, 0f),
            new Vector3(x - 1f, y, 0f),
            new Vector3(x - 0.7f, y - 0.7f, 0f),
            new Vector3(x, y - 1f, 0f),
            new Vector3(x + 0.7f, y - 0.7f, 0f)
        };

        int times = 9;
        for (int i = 0; i < times; i++)
        {
            Instantiate(partical, points[i], transform.rotation);
        }
        
        Destroy(gameObject, dieTime);
        
    }
}
