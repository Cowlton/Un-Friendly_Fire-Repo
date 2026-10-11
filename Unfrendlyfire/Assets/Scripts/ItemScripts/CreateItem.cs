
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
    private SpriteRenderer ItemDisplayRenderer;

    public Sprite _sprite;
    
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
        
        _sprite = objectToSpawn.GetComponent<SpriteRenderer>().sprite;
        
        ItemDisplayRenderer = itemDisplay.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //temporary
        itemDisplay.SetActive(showItem);
        
        ItemDisplayRenderer.sprite = _sprite;

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
