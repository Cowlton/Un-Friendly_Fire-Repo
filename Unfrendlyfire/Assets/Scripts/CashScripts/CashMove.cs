using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CashMove : MonoBehaviour
{
    public GameObject target;
    private CashBagSc CashBagSc;
    public float speed;
    public float minSpeed = 1;
    
    private RectTransform Rect;
    public float scaleAmmount;
    private float scaleTo;
    
    public float Variation;
    private Vector2 Rand;
    public float distroyDistance;
    public float Delay;

    [NonSerialized] public bool ToTracker; 

    void Start()
    {
        Rect = gameObject.GetComponent<RectTransform>();
        Rand = new Vector2(Random.Range(-Variation, Variation), Random.Range(-Variation, Variation));
        scaleTo = Rect.sizeDelta.x;
        Rect.sizeDelta = new Vector2(0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        if (Rect.sizeDelta.x < scaleTo)
        {
            Vector2 size = Rect.sizeDelta;
            size.x += scaleAmmount;
            size.y += scaleAmmount;
            Rect.sizeDelta = size;
        }
        
        if (Delay >= 0)
        {
            Delay -= 1 * Time.deltaTime;
        }

        if (target != null && Delay < 0)
        {
            speed += 1f * Time.deltaTime;
            var diff = Rand * (Vector3.Distance(transform.position,target.transform.position)/10);
            var movePoint = new Vector3(target.transform.position.x + diff.x, target.transform.position.y + diff.y, target.transform.position.z);
            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, movePoint, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position,target.transform.position) < distroyDistance)
            {
                Destroy(gameObject);

                if (ToTracker)
                {
                    var trackerScr =  target.GetComponent<NumEffect>();
                    trackerScr.Add();
                }
                else
                {
                    CashBagSc = target.GetComponent<CashBagSc>();
                    CashBagSc.AddCash();
                }
                
            }
        }
    }
}
