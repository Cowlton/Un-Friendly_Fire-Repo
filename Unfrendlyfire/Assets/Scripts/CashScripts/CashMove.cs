using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class CashMove : MonoBehaviour
{
    public GameObject CashBag;
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

    void Start()
    {
        Rect = gameObject.GetComponent<RectTransform>();
        CashBagSc =  CashBag.GetComponent<CashBagSc>();
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
        
        if (CashBag != null && Delay < 0)
        {
            speed += 1f * Time.deltaTime;
            var diff = Rand * (Vector3.Distance(transform.position,CashBag.transform.position)/10);
            var movePoint = new Vector3(CashBag.transform.position.x + diff.x, CashBag.transform.position.y + diff.y, CashBag.transform.position.z);
            // Move the follower object towards the calculated end position
            transform.position = Vector3.Lerp(transform.position, movePoint, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position,CashBag.transform.position) < distroyDistance)
            {
                Destroy(gameObject);
                CashBagSc.AddCash();
            }
        }
    }
}
