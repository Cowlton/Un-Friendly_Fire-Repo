using System;
using UnityEngine;

public class CashPickUpSc : MonoBehaviour
{
    
    public GameObject CollectSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
 
    private void OnDestroy()
    {
        Instantiate(CollectSound, transform.position, transform.rotation);
    }
}
