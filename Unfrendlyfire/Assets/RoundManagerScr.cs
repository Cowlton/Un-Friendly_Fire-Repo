using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RoundManagerScr : MonoBehaviour
{
    [SerializeField] private Maneger maneger;
    [SerializeField] private PointSc pointManagerScr;
    [SerializeField]private SpotlightScr spotlightSc;
    
    [Range(1,20)]
    public int maxPoints;
    
    private bool _canCheckContinue = false;
    private bool _roundEnded = false;
    [NonSerialized]public bool ShowContinue = false;
    public float continueTime = 20;
        
    public GameObject continueUI;
    
    public GameObject roundsUI;
    public GameObject backgroundUI;

    [NonSerialized]public bool GameOver = false;
    
    public float fadeInDuration = 1f;
    public float fadeOutDuration = 1f;
    
    private GameObject _uIcash;


    void Start()
    {
        roundsUI.SetActive(false);
    }
    
    void Update()
    {
        _uIcash = GameObject.FindGameObjectWithTag("UICash");
        if (_roundEnded && _uIcash != null || !_canCheckContinue)
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
    
    
    public void RoundEnd(int playerInt)
    {
        _roundEnded =  true;
        var players = maneger.Player;
        
        for (int i = 0; i < maneger.Player.Count; i++)
        {
            maneger.PlayerSc[i].slowMo = true;
        }
        
        Debug.Log("Round End");
        Debug.Log($"Player {playerInt+1} Won");
        
        UISpriteChanger scr = maneger.playerDesplayScr[playerInt];
        scr.Won();
        spotlightSc.SetWinnerSpotlight(players[playerInt].transform);
        FadeBacroundUiIn();
    }
    
    public void FadeBacroundUiIn()
    {
        var image = backgroundUI.GetComponent<Image>();
        StartCoroutine(Fade(0f, 1f, image,fadeInDuration));
    }

    public void FadeBacroundUiOut()
    {
        var image = backgroundUI.GetComponent<Image>();
        StartCoroutine(Fade(1f, 0f,  image,fadeOutDuration));
        roundsUI.SetActive(false);
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, Image image, float fadeDuration)
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
        if(startAlpha != 0)
        {
            _roundEnded = false;
            _canCheckContinue = false;
            maneger.roundEnded = false;
            maneger.SpawnPlayers();
            
        }
    }
    
    public void Continue()
    {
        if (GameOver)
        { 
            pointManagerScr.Reset();
        }
        FadeBacroundUiOut();
        var players = maneger.Player;
        for (int i = 0; i < players.Count; i++)
        {
            maneger.PlayerSc[i].slowMo = false;
        }
    }
    
    public void ClearScores(int playerWiner)
    {
        var sc = pointManagerScr;
        var playerScores = maneger.PlayerScores;
        for (int i = 0; i < playerScores.Count; i++)
        {
            if (i == playerWiner)
            {
                sc.AddCash(i, playerScores[i],true);
                _canCheckContinue = true;
            }
            else
            {
                sc.AddCash(i, playerScores[i],false);
            }
            playerScores[i] = 0;
           
        }
    }
}
