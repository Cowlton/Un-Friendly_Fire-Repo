using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CashTrackerSc : MonoBehaviour
{
    
    public List<NumEffect> scoreScr;
    public GameObject cashPrefab;
    public Canvas canvas;

    public void AddCash(int playerNum,Transform position)
    {
        Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(
            canvas.worldCamera,
            position.position
        );
        RectTransform canvasRect = canvas.GetComponent<RectTransform>();
        
        
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay
                ? null
                : canvas.worldCamera,
            out Vector2 uiPosition
        );
        
        var newCash = Instantiate(cashPrefab, canvas.transform, false);

        RectTransform cashRect = newCash.GetComponent<RectTransform>();
        var playerRect = position.transform.position;
        
        cashRect.anchoredPosition = uiPosition;
        //cashRect.position = playerRect;
        
        var scr = newCash.GetComponent<CashMove>();
        scr.Delay = 0.1f;
        scr.ToTracker = true;
        scr.target = scoreScr[playerNum].gameObject;
    }

    public void Reset()
    {
        for(int i=0; i<scoreScr.Count;i++)
        {
            scoreScr[i].Reset();
        }
    }
    
}
