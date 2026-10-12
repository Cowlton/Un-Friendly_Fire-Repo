using System;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Color = UnityEngine.Color;


public class Maneger : MonoBehaviour
{

    public List<TextMeshProUGUI> PlayerPoints = new List<TextMeshProUGUI>();
    
    public CamraSc CamraSc;
    
    #region player UI
    
    #region UI powers
    
    public List<GameObject> PlayerRocks = new List<GameObject>();
    public List<GameObject> PlayerDiscs = new List<GameObject>();
    public List<GameObject> PlayerLandMines = new List<GameObject>();
    public List<GameObject> PlayerEyes = new List<GameObject>();
    public List<GameObject> PlayerBats = new List<GameObject>();
    
    #endregion UI powers
    
    public GameObject KeyCard;
 
    public List<float> PlayerScores = new List<float>();
    
    #endregion player UI
    

    private int _playerWiner;
    
    public int RanColor;
    public List<int> RanNums;
    
    public bool roundEnded = false;
    
    private float _waitTime;
    
    public float AmountOfRocks = 5;
    
    public float fadeDuration = 1f;

    public List<Sprite> PlayerSprites;
    public List<Sprite> DeadPlayerSprites;
    public List<Sprite> SowrdSprites;
    public List<Sprite> HandSprites;
    public List<GameObject> PlayerUIs;
    
    public GameObject BigRock;
    public GameObject SmallRock;

    public Transform[] TopLeftSpawnPoints;
    public Transform[] BottomLeftSpawnPoints;
    public Transform[] TopRightSpawnPoints;
    public Transform[] BottomRightSpawnPoints;

    public List<Transform> ROCKS = new List<Transform>();
    
    public Transform Xmax;
    public Transform Xmin;

    public Transform Ymax;
    public Transform Ymin;

    public Sprite PlayerSprite;
    public Sprite DeadPlayerSprite;
    public Sprite HandSprite;
    public Sprite SowrdSprite;

    public UISpriteChanger[] playerDesplayScr;

    public List<GameObject> Player;
    public List<PlayerInput> PlayerSc;
    
    public List<String> tagsToDistroy = new List<string>(){"RockMaker","DiscMaker","LandMineMaker","Landmine","Keycard"};
    
    public RoundManagerScr roundManager;
    
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < PlayerUIs.Count; i++)
        {
            GameObject curUI = PlayerUIs[i];
            curUI.SetActive(false);
        }

        for (int i = 0; i < PlayerPoints.Count; i++)
        {
            PlayerPoints[i].text = PlayerScores[i].ToString();
        }
        
        MakeNewRocks();
        
    }

    // Update is called once per frame

    public void PlayerDied(int player)
    {
        if (playerDesplayScr.Length >= player){
            
            UISpriteChanger scr = playerDesplayScr[player];
            scr.Died();
        }
    }

    public void PlayerRevived(int player)
    {
        if (playerDesplayScr.Length >= player){
            
            UISpriteChanger scr = playerDesplayScr[player];
            scr.DefaltAvatar();
        }
    }
    
    
    public void CleanRocks()
    {
        ROCKS.RemoveAll(t => t == null);
        if (ROCKS.Count <= 3)
        {
            MakeNewRocks();
        }
    }

    public void DestroyRock(GameObject rock)
    {
        ROCKS.Remove(rock.transform);
        CleanRocks();
        Destroy(rock);
    }

    public void Collect(int player)
    {
        if(!roundEnded)
        {
        if (playerDesplayScr.Length > player){
            
            UISpriteChanger scr = playerDesplayScr[player];
            scr.GotCash();
        }
        PlayerScores[player] += 1;
        var pointSc = PlayerPoints[player].GetComponent<NumEffect>();
        pointSc.Add();
        winCheck();
        }
    }

    private void winCheck()
    {
        for (int i = 0; i < PlayerScores.Count; i++)
        {
            if (PlayerScores[i] >= roundManager.maxPoints)
            {
                roundManager.RoundEnd(i);
                roundEnded = true;
            }
        }
    }

    void MakeNewRocks()
    {
        for (int i = 0; i < AmountOfRocks; i++) // Loop for rows
        {
            float PositionX = Random.Range(Xmin.transform.position.x, Xmax.transform.position.x);
            float PositionY = Random.Range(Ymin.transform.position.y, Ymax.transform.position.y);
            Vector3 position = new Vector2(PositionX, PositionY);
            GameObject NewRock = Instantiate(SmallRock, position, Quaternion.identity);
            ROCKS.Add(NewRock.transform);
        }

    }

    public int PICK()
    {

        RanColor = Random.Range(0, RanNums.Count);
        
        int col = RanNums[RanColor];
        
        PlayerSprite = PlayerSprites[col];
        DeadPlayerSprite = DeadPlayerSprites[col];
        SowrdSprite = SowrdSprites[col];
        HandSprite = HandSprites[col];
        
        GameObject curUI = PlayerUIs[Player.Count];
        curUI.SetActive(true);
        var UISc = curUI.GetComponent<UISpriteChanger>();
            
        UISc.color = col;
        
        RanNums.RemoveAt(RanColor);
        
        return col;

    }
    
    public void ClearScores()
    {
        roundManager.ClearScores(_playerWiner);
    }
 
    public void Continue()
    {
        roundManager.Continue();
    }
    
    public void PlayerWon()
    {
        roundManager.GameOver = true;
    }
   
    public void HidePlayers()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            PlayerSc[i].Store();
        }
    }

    public async Task SpawnPlayers()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            PlayerSc[i].UnDie();
            PlayerSc[i].Spawn(Xmin,Xmax,Ymin,Ymax);
        }
        await Task.Delay(300);
        CleanRocks();
    }

    public void Clear()
    {
        CleanRocks();
        foreach (Transform rock in ROCKS)
        {
            Destroy(rock.gameObject);
        }
        ROCKS.Clear();
        
        HashSet<GameObject> toDestroy = new HashSet<GameObject>();
        foreach (string tag in tagsToDistroy)
        {
            foreach (GameObject obj in GameObject.FindGameObjectsWithTag(tag))
            {
                toDestroy.Add(obj);
            }
        }
        foreach (GameObject obj in toDestroy)
        {
            Destroy(obj);
        }
    }

    public void ClearPlayerInventory()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            PlayerSc[i].ClearInventory();
        }
    }


    public void SpawnBigRock()
    {
        int RandomeNum = Random.Range(1, 4);

        if (RandomeNum == 1)
        {
            int randomIndex = Random.Range(0, TopLeftSpawnPoints.Length - 1);

            GameObject NEWBigRock = Instantiate(BigRock, TopLeftSpawnPoints[randomIndex].transform.position, Quaternion.identity);
            RockMove BigRock_sk = NEWBigRock.GetComponent<RockMove>();

            int RandomeEnd = Random.Range(0, BottomRightSpawnPoints.Length - 1);
            BigRock_sk.velocity = new Vector2(BottomRightSpawnPoints[RandomeEnd].transform.position.x, BottomRightSpawnPoints[RandomeEnd].transform.position.y);
        }
        if (RandomeNum == 2)
        {
            int randomIndex = Random.Range(0, BottomLeftSpawnPoints.Length - 1);

            GameObject NEWBigRock = Instantiate(BigRock, BottomLeftSpawnPoints[randomIndex].transform.position, Quaternion.identity);
            RockMove BigRock_sk = NEWBigRock.GetComponent<RockMove>();

            int RandomeEnd = Random.Range(0, TopRightSpawnPoints.Length - 1);
            BigRock_sk.velocity = new Vector2(TopRightSpawnPoints[RandomeEnd].transform.position.x, TopRightSpawnPoints[RandomeEnd].transform.position.y);
        }
        if (RandomeNum == 3)
        {
            int randomIndex = Random.Range(0, TopRightSpawnPoints.Length - 1);

            GameObject NEWBigRock = Instantiate(BigRock, TopRightSpawnPoints[randomIndex].transform.position, Quaternion.identity);
            RockMove BigRock_sk = NEWBigRock.GetComponent<RockMove>();

            int RandomeEnd = Random.Range(0, BottomLeftSpawnPoints.Length - 1);
            BigRock_sk.velocity = new Vector2(BottomLeftSpawnPoints[RandomeEnd].transform.position.x, BottomLeftSpawnPoints[RandomeEnd].transform.position.y);
        } 
        if (RandomeNum == 4)
        {
            int randomIndex = Random.Range(0, BottomRightSpawnPoints.Length - 1);

            GameObject NEWBigRock = Instantiate(BigRock, BottomRightSpawnPoints[randomIndex].transform.position, Quaternion.identity);
            RockMove BigRock_sk = NEWBigRock.GetComponent<RockMove>();

            int RandomeEnd = Random.Range(0, TopLeftSpawnPoints.Length - 1);
            BigRock_sk.velocity = new Vector2(TopLeftSpawnPoints[RandomeEnd].transform.position.x, TopLeftSpawnPoints[RandomeEnd].transform.position.y);

        }
        

    }
}
