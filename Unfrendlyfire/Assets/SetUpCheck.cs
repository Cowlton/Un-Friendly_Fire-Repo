using UnityEngine;
using UnityEngine.SceneManagement;

public class SetUpCheck : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        var configManager = GameObject.Find("PlayerConfigManager");
        if (configManager  == null)
        {
            SceneManager.LoadScene("StartSceen");
        }
    }
}
