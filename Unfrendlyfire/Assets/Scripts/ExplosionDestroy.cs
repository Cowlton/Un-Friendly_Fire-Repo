using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    private string[] ItemCoinTags = {"RockMaker","DiscMaker" ,"LandMineMaker"};

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            var sc = collider.gameObject.GetComponent<PlayerMovement>();
            sc.Death();
        }
        if (collider.gameObject.CompareTag("Rock"))
        {
            // Debug.Log("Rock collided");
            var sc = collider.gameObject.GetComponent<CreateItem>();
            sc.Create();
        }
        if (ItemCoinTags.Contains(collider.tag))
        {
            Destroy( collider.gameObject);
        }
    }

}
