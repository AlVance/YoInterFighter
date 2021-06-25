using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoMainManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text puntuationText;
    public float puntuation;
    private int shownPuntuation;

    public float maxVelocityObstacles;
    public float velocityObstacles;
    public float acceleration = 0.0005f;
 
    
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

    public void RestardGame()
    {
        SceneManager.LoadScene("DinoGame");
        Time.timeScale = 1;
    }

}
