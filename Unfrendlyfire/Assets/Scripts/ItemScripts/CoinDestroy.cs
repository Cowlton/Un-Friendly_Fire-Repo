using UnityEngine;

public class CoinDestroy : MonoBehaviour
{
    public float destroyTime = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0)
        {
            transform.localScale = (transform.localScale - new Vector3(2, 2, 0f) * Time.deltaTime);
        }

        if (transform.localScale.x <= 0)
        {
            Destroy(gameObject);
        }
    }
}
