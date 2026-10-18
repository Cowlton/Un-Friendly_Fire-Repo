using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]private Transform topSpawnPoint;
    [SerializeField]private Transform bottomSpawnPoint;
    [SerializeField]private Transform leftSpawnPoint;
    [SerializeField]private Transform rightSpawnPoint;
    
    [SerializeField]private GameObject playerPrefab;
    
    void Start()
    {
        var playerConfigs = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        SpawnPlayers(playerConfigs);
    }

    public void SpawnPlayers(PlayerConfig[] players)
    {
        var leftX = leftSpawnPoint.position.x;
        var rightX = rightSpawnPoint.position.x;
        var topY = topSpawnPoint.position.y;
        var bottomY = bottomSpawnPoint.position.y;
        
        foreach (var playerConfig in players)
        {
            var randX = Random.Range(leftX, rightX);
            var randY =  Random.Range(topY, bottomY);
            var randPos = new Vector2(randX,randY);
            
            var player = Instantiate(playerPrefab, randPos, Quaternion.identity, gameObject.transform);

            player.GetComponent<PlayerMovement>().InitializePlayer(playerConfig);
        }
    }
    
}
