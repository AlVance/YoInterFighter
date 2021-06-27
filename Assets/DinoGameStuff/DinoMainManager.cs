using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DinoMainManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject tutoScreen;
    public GameObject gameOverScreen;
    public GameObject scoreboardScreen;
    public Text puntuationText;
    public float puntuation;
    private int shownPuntuation;

    public float maxVelocityObstacles;
    public float velocityObstacles;
    public float acceleration = 0.0005f;

    private bool gameStarted;

    public InputField nameField;
    public Text scoreField;

    string nameFinish;
    int scoreFinish;

    public ScoreboardManager scoreboardMngr;

    private void Awake()
    {
        Time.timeScale = 0f;
        gameStarted = false;
        tutoScreen.SetActive(true);
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        scoreboardScreen.SetActive(false);
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
            tutoScreen.SetActive(true);
            Invoke("TutoPanel", 5f);
        }
    }

    public void GameOver()
    {
        scoreboardScreen.SetActive(true);
    }

    public void SaveName()
    {
        if(nameField.text != string.Empty)
        {
            nameFinish = nameField.text;
            scoreFinish = Mathf.RoundToInt(puntuation);
            scoreboardMngr.SetScore(nameFinish, scoreFinish);
        }
    }

    public void TutoPanel()
    {
        tutoScreen.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
