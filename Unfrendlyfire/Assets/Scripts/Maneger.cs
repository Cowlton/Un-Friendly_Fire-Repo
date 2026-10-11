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

    private bool GameOver = false;
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
    public GameObject RoundsUI;
    public GameObject BacroundUI;
    
    public List<float> PlayerScores = new List<float>();
    
    #endregion player UI
    

    private int playerWiner;
    
    public int RanColor;
    public List<int> RanNums;

    #region RoundRules
    
    public bool roundEnded = false;
    [NonSerialized]public bool ShowContinue = false;
    private bool CanCheckContinue = false;
    public float continueTime = 20;
        
    public GameObject continueUI;
    
    #endregion RoundRules
   
    private float _waitTime;
    
    public float maxPoints = 10;
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

    public GameObject pointManager;
    public GameObject UIcash;

    public List<String> tagsToDistroy = new List<string>(){"RockMaker","DiscMaker","LandMineMaker","Landmine","Keycard"};
    
    // Start is called before the first frame update
    void Start()
    {
        RoundsUI.SetActive(false);

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
    void Update()
    {
        UIcash = GameObject.FindGameObjectWithTag("UICash");
        if (roundEnded && UIcash != null || !CanCheckContinue)
        {
            ShowContinue = false;
            continueUI.SetActive(false);
            continueTime = 20;
        }
        else
        {
            ShowContinue = true;
        }

        if (ShowContinue)
        {
            if (continueTime > 0)
            {
                continueTime -= 0.5f;
            }
            else
            {
                continueUI.SetActive(true);
            }
        }
        
    }

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
            if (PlayerScores[i] >= maxPoints)
            {
                RoundEnd(i);
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

	public void RoundEnd(int playerInt)
    {
        roundEnded = true;
        for (int i = 0; i < Player.Count; i++)
        {
            PlayerSc[i].slowMo = true;
        }
        
        Debug.Log("Round End");
        Debug.Log($"Player {playerInt+1} Won");
        
        UISpriteChanger scr = playerDesplayScr[playerInt];
        playerWiner =  playerInt;
        scr.Won();
        FadeBacroundUiIn();

    }
    

    public void ClearScores()
    {
        
        var sc = pointManager.GetComponent<PointSc>();
        for (int i = 0; i < PlayerScores.Count; i++)
        {
            if (i == playerWiner)
            {
                sc.AddCash(i, PlayerScores[i],true);
                CanCheckContinue = true;
            }
            else
            {
                sc.AddCash(i, PlayerScores[i],false);
            }
            PlayerScores[i] = 0;
           
        }
        HidePlayers();
    }
 
    public void Continue()
    {
        if (GameOver == true)
        { 
            var sc = pointManager.GetComponent<PointSc>();
            sc.Reset();
        }
        FadeBacroundUiOut();
        for (int i = 0; i < Player.Count; i++)
        {
            PlayerSc[i].slowMo = false;
        }
    }
    
    public void PlayerWon()
    {
        GameOver = true;
    }

    public void FadeBacroundUiIn()
    {
        RoundsUI.SetActive(true);
        var Image = BacroundUI.GetComponent<Image>();
        StartCoroutine(Fade(0f, 1f, Image));
    }

    public void FadeBacroundUiOut()
    {
        var Image = BacroundUI.GetComponent<Image>();
        StartCoroutine(Fade(1f, 0f,  Image));
        RoundsUI.SetActive(false);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, Image image)
    {
        float elapsedTime = Time.deltaTime;
        float start = Time.deltaTime;
        while (elapsedTime < fadeDuration + start)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }
        image.color = new Color(image.color.b, image.color.b, image.color.b, endAlpha);
        yield return null;
        if (startAlpha == 0f)
        {
            Clear();
            ClearScores();
        }
        else
        {
            roundEnded = false;
            CanCheckContinue = false;
            SpawnPlayers();
            
        }
    }

    void HidePlayers()
    {
        for (int i = 0; i < Player.Count; i++)
        {
            PlayerSc[i].Store();
        }
    }

    async Task SpawnPlayers()
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

        ClearPlayerInvintory();
    }

    void ClearPlayerInvintory()
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
