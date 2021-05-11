using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoMainManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text puntuationText;
    public float puntuation;
    private int shownPuntuation;

    public float maxVelocityObstacles;
    public float velocityObstacles;
    private float acceleration = 0.001f;
 
    
    // Update is called once per frame
    void Update()
    {
        puntuation += Time.deltaTime;
        shownPuntuation = Mathf.RoundToInt(puntuation * 10);
        puntuationText.text = shownPuntuation.ToString();
        
        if(velocityObstacles < maxVelocityObstacles)
        {
            velocityObstacles += acceleration;
        }
        
        
    }


}
