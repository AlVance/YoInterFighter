using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public DinoMainManager dinoMngr;

    public ScoreboardObject scoreboard_item;
    public Scoreboard scoreboard_total;

    public Text maxScoreText;

    public GameObject[] scoreTop;

    string jsonSavePath;

    int maxScore;
    string maxName;

    public Text lastScore;
    //bool firstTime = true;

    // Start is called before the first frame update
    public void Start()
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
    public void SetIp(string newID)
    {
        scoreboard_total.id = newID;
        string jsonData = JsonUtility.ToJson(scoreboard_total, true);
        File.WriteAllText(jsonSavePath, jsonData);

    }

    public void SetScore(string name, int score)
    {
        scoreboard_item.name = name;
        scoreboard_item.score = score;

        lastScore.text = scoreboard_item.score.ToString();

        scoreboard_total.scoreboardTotal.Add(scoreboard_item);

        scoreboard_total.scoreboardTotal.Sort(delegate (ScoreboardObject x, ScoreboardObject y)
        {
            if (x.score == null && y.score == null) return 0;
            else if (x.score == null) return -1;
            else if (y.score == null) return 1;
            else return y.score.CompareTo(x.score);
        });

        if(scoreboard_total.scoreboardTotal.Count > 5)
        {
            scoreboard_total.scoreboardTotal.RemoveAt(scoreboard_total.scoreboardTotal.Count - 1);
        }


        string jsonData = JsonUtility.ToJson(scoreboard_total, true);
        File.WriteAllText(jsonSavePath, jsonData);

        ShowScore();
    }

    public int SortByScore()
    {
        return 0;
    }

    public void ShowScore()
    {
        for (int i = 0; i < scoreTop.Length; i++)
        {
            if (i < scoreboard_total.scoreboardTotal.Count)
            {
                if (scoreboard_total.scoreboardTotal[i].score != 0)
                {
                    //scoreTop[i].transform.Find("Name").GetComponent<Text>().text = (i + 1).ToString();
                    //scoreTop[i].transform.Find("Points").GetComponent<Text>().text = scoreboard_total.scoreboardTotal[i].score.ToString();
                }
            }
            else
            {
                //scoreTop[i].transform.Find("Points").GetComponent<Text>().text = "";
                //scoreTop[i].transform.Find("Name").GetComponent<Text>().text = "";
            }
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
    public string id;
    public List<ScoreboardObject> scoreboardTotal;
}