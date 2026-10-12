using System;
using System.Collections;
using UnityEngine;

public class SpotlightScr : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    [SerializeField]private Maneger _manegerSc;
    [SerializeField]private RoundManagerScr _roundManagerScr;
    
    [SerializeField] private Material spotlightMaterial;
    [SerializeField] private Camera gameCamera;

    private static readonly int CenterID = Shader.PropertyToID("_Center");
    private static readonly int RadiusID = Shader.PropertyToID("_Radius");
    
    [SerializeField] private float growDuration = 0.3f;
    [SerializeField] private float holdDuration = 1.5f;
    [SerializeField] private float shrinkDuration = 0.5f;

    private Coroutine spotlightCoroutine;
    private static readonly int SpotlightEnabledID =
        Shader.PropertyToID("_SpotlightEnabled");
    
    private float _radius;
    private Transform _winner;

    private bool hasWinner = false;
    

    public void PlayWinnerSpotlight()
    {
        if (spotlightCoroutine != null)
            StopCoroutine(spotlightCoroutine);

        spotlightCoroutine = StartCoroutine(AnimateSpotlight());
    }
    
    public void SetWinnerSpotlight(Transform winner, float radius = 0.15f)
    {
        _radius = radius;
        _winner = winner;
        hasWinner =  true;
        PlayWinnerSpotlight();
    }

    private void Update()
    {
        if (hasWinner)
        {
            Vector3 screenPos = gameCamera.WorldToScreenPoint(_winner.position);

            Vector2 uv = new Vector2(
                screenPos.x / Screen.width,
                screenPos.y / Screen.height
            );

            spotlightMaterial.SetVector(CenterID, uv);
            spotlightMaterial.SetFloat(RadiusID, _radius);
        }
    }

    private IEnumerator AnimateSpotlight()
    {
        spotlightMaterial.SetFloat(SpotlightEnabledID, 1f);
        float radius = 0f;
        float maxRadius = 0.15f;

        // Grow the circle
        float elapsed = 0f;

        while (elapsed < growDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / growDuration);
            radius = Mathf.Lerp(0f, maxRadius, t);

            spotlightMaterial.SetFloat(RadiusID, radius);

            yield return null;
        }

        // Keep the circle open
        spotlightMaterial.SetFloat(RadiusID, maxRadius);
        yield return new WaitForSeconds(holdDuration/2);
        _roundManagerScr.roundsUI.SetActive(true);
        yield return new WaitForSeconds(holdDuration/2);

        // Shrink the circle
        elapsed = 0f;

        while (elapsed < shrinkDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / shrinkDuration);
            radius = Mathf.Lerp(maxRadius, 0f, t);

            spotlightMaterial.SetFloat(RadiusID, radius);

            yield return null;
        }

        spotlightMaterial.SetFloat(RadiusID, 0f);
        spotlightMaterial.SetFloat(SpotlightEnabledID, 0f);
        _manegerSc.HidePlayers();
        _manegerSc.Clear();
        _manegerSc.ClearScores();
        _manegerSc.ClearPlayerInventory();
        hasWinner = false;
        spotlightCoroutine = null;
    }
    

}
