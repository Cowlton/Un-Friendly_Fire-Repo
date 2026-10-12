using UnityEngine;
[System.Serializable]
public class SpawnItem
{
    public GameObject item;
    [Range(0f, 100f)]
    public int likelihood;
}
