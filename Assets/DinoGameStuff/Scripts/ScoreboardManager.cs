using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public DinoMainManager dinoMngr;

    public ScoreboardObject scoreboard_item;
    public Scoreboard scoreboard_total;

    public Text maxScoreText;

    string jsonSavePath;

    int maxScore;
    string maxName;

    int currentScore;
    //bool firstTime = true;

    // Start is called before the first frame update
    public void Awake()
    {
        jsonSavePath = Application.persistentDataPath + "/saveload.json";
        Debug.Log(jsonSavePath);
        ReadJson();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SetScore("Prueba",Mathf.RoundToInt(dinoMngr.puntuation * 10));
        }
    }

    public void ReadJson()
    {
        if (File.Exists(jsonSavePath))
        {
            string json = File.ReadAllText(jsonSavePath);
            JsonUtility.FromJsonOverwrite(json, scoreboard_total);
            CheckMaxScore();
        }
        else
        {
            File.Create(jsonSavePath);
        }
    }

    public void CheckMaxScore()
    {
        for (int i = 0; i < scoreboard_total.scoreboardTotal.Count; i++)
        {
            if(scoreboard_total.scoreboardTotal[i].score > maxScore)
            {
                maxName = scoreboard_total.scoreboardTotal[i].name;
                maxScore = scoreboard_total.scoreboardTotal[i].score;
            }
        }
        maxScoreText.text = maxName + " > " + maxScore;
    }

    public void SetScore(string name, int score)
    {
        currentScore = score;
        scoreboard_item.name = name;
        scoreboard_item.score = score;
        scoreboard_total.scoreboardTotal.Add(scoreboard_item);

        Debug.Log(scoreboard_total);

        string jsonData = JsonUtility.ToJson(scoreboard_total, true);
        File.WriteAllText(jsonSavePath, jsonData);

        ShowScore();
    }

    public void ShowScore()
    {
        CheckMaxScore();
    }
}

[System.Serializable]
public class ScoreboardObject
{
    public string name;
    public int score;

    public ScoreboardObject(string _name, int _score)
    {
        name = _name;
        score = _score;
    }
}

[System.Serializable]
public class Scoreboard
{
    public List<ScoreboardObject> scoreboardTotal = new List<ScoreboardObject>(5);
}