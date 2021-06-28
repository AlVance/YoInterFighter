using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public DinoMainManager dinoMngr;

    public GameObject newScore;
    public GameObject scoreboardFull;

    public RectTransform fullContainer;

    public GameObject contentScore;
    public int sizeItem = 75;

    public ScoreboardObject scoreboard_item;
    public Scoreboard scoreboard_total;

    public Text maxScoreText;

    string jsonSavePath;

    int maxScore;
    string maxName;

    // Start is called before the first frame update
    void Start()
    {
        jsonSavePath = Application.dataPath + "/saveload.json";
        fullContainer.sizeDelta = new Vector2(fullContainer.sizeDelta.x, fullContainer.transform.childCount * sizeItem);
        ReadJson();
    }

    public void ReadJson()
    {
        string json = File.ReadAllText(jsonSavePath);
        JsonUtility.FromJsonOverwrite(json, scoreboard_total);
        CheckMaxScore();
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
        newScore.SetActive(false);
        scoreboardFull.SetActive(true);

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
        for (int i = 0; i < scoreboard_total.scoreboardTotal.Count; i++)
        {
            GameObject newItem = Instantiate(contentScore, fullContainer.transform);
            newItem.transform.Find("NameText").GetComponent<Text>().text = scoreboard_total.scoreboardTotal[i].name;
            newItem.transform.Find("ScoreText").GetComponent<Text>().text = scoreboard_total.scoreboardTotal[i].score.ToString();
            fullContainer.sizeDelta = new Vector2(fullContainer.sizeDelta.x, fullContainer.transform.childCount * sizeItem);
        }
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
    public List<ScoreboardObject> scoreboardTotal = new List<ScoreboardObject>();
}