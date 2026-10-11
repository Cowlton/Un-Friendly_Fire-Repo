using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundSc : MonoBehaviour
{

    public Color color;

    int score = 0;
    
    public GameObject[] points;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RoundPoint()
    {
        if (score < points.Length) {
            Image myImage = points[score].GetComponent<Image>();
            myImage.color = color;
            score++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
