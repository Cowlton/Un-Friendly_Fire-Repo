using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemManagerSc : MonoBehaviour
{
    public List<SpawnItem> items = new List<SpawnItem>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject GetObject()
    {
        float totalWeight = 0f;

        foreach (SpawnItem spawnItem in items)
        {
            totalWeight += spawnItem.likelihood;
        }

        if (totalWeight <= 0f)
        {
            Console.Write("No Items Are assigned a likelihood");
        }
        else
        {
            float roll = Random.Range(0f, totalWeight);

            foreach (SpawnItem spawnItem in items)
            {
                roll -= spawnItem.likelihood;

                if (roll < 0f)
                {
                    return(spawnItem.item);
                }
            }
        }
        return(null);
    }
}
