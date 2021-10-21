using System.Collections;
using System.Collections.Generic;
using PlayFab;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Code;

public class DinoMainManager : MonoBehaviour
{
    public GameObject startScreen;
    public GameObject tutoScreen;
    public GameObject gameOverScreen;
    public GameObject scoreboardScreen;
    public GameObject inGamePanel;
    public GameObject inputNamePanel;

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

    Main main;
    bool named;

    private void Awake()
    {
        Time.timeScale = 0f;
        gameStarted = false;
        tutoScreen.SetActive(true);
        startScreen.SetActive(true);
        gameOverScreen.SetActive(false);
        scoreboardScreen.SetActive(false);
        inGamePanel.SetActive(false);
        inputNamePanel.SetActive(false);
        main = GetComponent<Main>();

        int rnd = Random.Range(1, 3);
        if (rnd == 1)
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
                velocityObstacles += (acceleration * (Time.deltaTime * Time.timeScale)) * 100;
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
        tutoScreen.SetActive(false);
        inGamePanel.SetActive(false);

        if (main.CheckName())
        {
            scoreboardScreen.SetActive(true);
            inputNamePanel.SetActive(false);
            main.OnAddPlayerScoreButtonPressed(shownPuntuation);
        }
        else
        {
            inputNamePanel.SetActive(true);
            scoreboardScreen.SetActive(false);
        }
    }

    public void SubmitNameText(InputField namefield)
    {
        if (namefield.text.Length >= 3)
        {
            main.SubmitName(namefield);
        }
    }

    public void ShowScore(string[] arrayStr)
    {
        if (arrayStr.Length > 2)
        {
            scoreboardMngr.scoreTop[0].transform.Find("Name").GetComponent<Text>().text = arrayStr[1];
            scoreboardMngr.scoreTop[0].transform.Find("Points").GetComponent<Text>().text = arrayStr[2];
        }
        if (arrayStr.Length > 5)
        {
            scoreboardMngr.scoreTop[1].transform.Find("Name").GetComponent<Text>().text = arrayStr[4];
            scoreboardMngr.scoreTop[1].transform.Find("Points").GetComponent<Text>().text = arrayStr[5];
        }
        if (arrayStr.Length > 8)
        {
            scoreboardMngr.scoreTop[2].transform.Find("Name").GetComponent<Text>().text = arrayStr[7];
            scoreboardMngr.scoreTop[2].transform.Find("Points").GetComponent<Text>().text = arrayStr[8];
        }
        if (arrayStr.Length > 11)
        {
            scoreboardMngr.scoreTop[3].transform.Find("Name").GetComponent<Text>().text = arrayStr[10];
            scoreboardMngr.scoreTop[3].transform.Find("Points").GetComponent<Text>().text = arrayStr[11];
        }
        if (arrayStr.Length > 14)
        {
            scoreboardMngr.scoreTop[4].transform.Find("Name").GetComponent<Text>().text = arrayStr[13];
            scoreboardMngr.scoreTop[4].transform.Find("Points").GetComponent<Text>().text = arrayStr[14];
        }

    }

    public void SaveName()
    {
        if(nameField.text != string.Empty)
        {
            named = true;
            main.OnAddPlayerScoreButtonPressed(shownPuntuation);
            main.SubmitName(nameField);
            buttonAudio.Play();
            scoreboardScreen.SetActive(true);
            inputNamePanel.SetActive(false);
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

    public void SetCanPlayMusic()
    {
        if (musicPlaying.isPlaying) musicPlaying.Stop();
        else musicPlaying.Play();
    }
}
