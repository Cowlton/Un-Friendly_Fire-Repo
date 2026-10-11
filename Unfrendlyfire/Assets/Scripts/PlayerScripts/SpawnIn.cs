using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnIn : MonoBehaviour
{
    public float followSpeed = 10;

    public float offset;

    public GameObject leaderObject;
    public GameObject DethTrail;
    public int index = 0;
    public Sprite[] sprites;

    public float waitTime = 3;
    public float timeToWaite;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        spriteRenderer.sprite = sprites[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (leaderObject != null)
        {

            if (timeToWaite <= Time.time)
            {
                GameObject newTrail = Instantiate(DethTrail, transform.position, Quaternion.identity);
                AlphaShift trailScript = newTrail.GetComponent<AlphaShift>();
                trailScript.index = index;
                timeToWaite = Time.time + waitTime;
            }

            // Calculate the position at the end of the leader object (using its position + offset)
            Vector3 endPosition = leaderObject.transform.position;

            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, endPosition, followSpeed * Time.deltaTime);

            float endY = leaderObject.transform.position.y + 0.2f;

            if (transform.position.y <= endY)
            {
                Destroy(gameObject);
            }

        }
    }

}
