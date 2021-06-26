using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoMainManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject gameOverScreen;
    public Text puntuationText;
    public float puntuation;
    private int shownPuntuation;

    public float maxVelocityObstacles;
    public float velocityObstacles;
    public float acceleration = 0.0005f;

    private bool gameStarted;
    private void Awake()
    {
        Time.timeScale = 0f;
        gameStarted = false;
        startScreen.SetActive(true);
    }

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

    public void StartGame()
    {
        if (!gameStarted)
        {
            Time.timeScale = 1f;
            gameStarted = true;
            startScreen.SetActive(false);
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
