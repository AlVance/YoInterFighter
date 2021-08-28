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
    public GameObject inGamePanel;

    public Text puntuationText;
    public float puntuation;
    private int shownPuntuation;

    public float maxVelocityObstacles;
    public float velocityObstacles;
    public float acceleration = 0.0005f;

    public bool gameStarted;

    public InputField nameField;
    public Text scoreField;

    public Slider beerSlider;

    string nameFinish;
    int scoreFinish;

    public ScoreboardManager scoreboardMngr;

    public AudioSource buttonAudio;
    public GameObject music1Audio;
    public GameObject music2Audio;
    private AudioSource musicPlaying;

    public int beersToUlti;

    public Vector3[] targetCans;
    public GameObject startTargetsCan;
    public int fillCans;

    private void Awake()
    {
        Time.timeScale = 0f;
        gameStarted = false;
        tutoScreen.SetActive(true);
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        scoreboardScreen.SetActive(false);
        inGamePanel.SetActive(false);

        int rnd = Random.Range(1, 3);
        if(rnd == 1)
        {
            music1Audio.SetActive(true);
            musicPlaying = music1Audio.GetComponent<AudioSource>();
        }
        else
        {
            music2Audio.SetActive(true);
            musicPlaying = music2Audio.GetComponent<AudioSource>();
        }
        targetCans = new Vector3[beersToUlti];
        float offset = 10 / beersToUlti;

        for (int i = 0; i < beersToUlti; i++)
        {
            if(i == 0)
            {
                targetCans[i] = startTargetsCan.transform.position;
            }
            else
            {
                targetCans[i] = new Vector3(targetCans[i - 1].x + offset, targetCans[i - 1].y,targetCans[i-1].z);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {      
        if(gameStarted)
        {
            puntuation += Time.deltaTime;
            shownPuntuation = Mathf.RoundToInt(puntuation * 10);
            puntuationText.text = shownPuntuation.ToString();

            if (velocityObstacles < maxVelocityObstacles)
            {
                velocityObstacles += acceleration;
            }
        }
    }

    public void RestardGame()
    {
        buttonAudio.Play();
        SceneManager.LoadScene("DinoGame");
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        if (!gameStarted)
        {
            buttonAudio.Play();
            Time.timeScale = 1f;
            gameStarted = true;
            startScreen.SetActive(false);
            inGamePanel.SetActive(true);
            Invoke("TutoPanel", 5f);
        }
    }

    public void GameOver()
    {
        musicPlaying.Stop();
        scoreField.text = shownPuntuation.ToString();
        scoreboardMngr.SetScore(nameFinish, shownPuntuation);
        scoreboardScreen.SetActive(true);
        tutoScreen.SetActive(false);
        inGamePanel.SetActive(false);
    }

    public void SaveName()
    {
        if(nameField.text != string.Empty)
        {
            buttonAudio.Play();
            nameFinish = nameField.text;
            scoreFinish = shownPuntuation;
            scoreboardMngr.SetScore(nameFinish, scoreFinish);
        }
    }

    public void TutoPanel()
    {
        tutoScreen.SetActive(false);
    }

    public void ExitGame()
    {
        buttonAudio.Play();
        Application.Quit();
    }
}
