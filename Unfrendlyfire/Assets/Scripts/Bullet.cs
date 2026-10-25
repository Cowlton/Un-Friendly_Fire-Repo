using UnityEngine;

namespace DefaultNamespace
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20f;
        public Rigidbody2D rb;
       // public GameObject impactEffect;
        public Collider2D coll;
        public Collider2D batColl;

        void Start()
        {
            coll = GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(coll, batColl, true);
            rb.linearVelocity = transform.right * speed;
        }

        void OnTriggerEnter2D(Collider2D hitInfo)
        {
            var enemy = hitInfo.gameObject.GetComponent<PlayerMovement>();
            if (enemy != null)
            {
                enemy.Death();
            }

            var rock = hitInfo.gameObject.GetComponent<CreateItem>();
            if (rock != null)
            {
                rock.Create();
            }
            //Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}