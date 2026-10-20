using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
   public GameObject playerSetupPrefab;
   public PlayerInput input;
   
   private void Awake()
   {
      var manager = GameObject.Find("PlayerConfigManager");
      transform.SetParent(manager.transform);
      var rootMenu = GameObject.Find("MainLayout");
      if (rootMenu != null)
      {
         Debug.Log($"Spawning Player with index{input.playerIndex}");
         var menu = Instantiate(playerSetupPrefab,rootMenu.transform);
         
         input.uiInputModule = menu.GetComponentInChildren<InputSystemUIInputModule>();

         var menuScr = menu.GetComponent<PlayerSetUpMenu>();
         menuScr.Config = gameObject;
         menuScr.SetPlayerIndex(input.playerIndex);
      }
   }
   
   
}
