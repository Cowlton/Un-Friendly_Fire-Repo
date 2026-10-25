
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CreateItem : MonoBehaviour
{
    public GameObject objectToSpawn;

    private GameObject _manager;
    private Maneger _managerSc;
    
    private GameObject _itemManager;
    private ItemManagerSc _itemManagerSc;
    
    public bool showItem = false;
    public GameObject itemDisplay;
    public SpriteRenderer _itemDisplayRenderer;

    public Sprite sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        
        if (GameObject.FindWithTag("Manager") != null)
        {
            _manager = GameObject.FindWithTag("Manager");
            _managerSc = _manager.GetComponent<Maneger>();
        }
       
        _itemManager = GameObject.FindWithTag("ItemManager");
        _itemManagerSc = _itemManager.GetComponent<ItemManagerSc>();
        objectToSpawn = _itemManagerSc.GetObject();
        
        sprite = objectToSpawn.GetComponent<SpriteRenderer>().sprite;
        
    }
    
    void Update()
    {
        //temporary
        itemDisplay.SetActive(showItem);
        
        _itemDisplayRenderer.sprite = sprite;

    }

    // Update is called once per frame


    public void Create()
    {
       // Spawn the object at the spawnPoint's position or at the origin (0,0,0) with no rotation
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);
        _managerSc.DestroyRock(gameObject);
       // Debug.Log("Removed transform.");
    }
}
