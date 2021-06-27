using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardManager : MonoBehaviour
{
    public DinoMainManager dinoMngr;

    public RectTransform fullContainer;

    public GameObject contentScore;
    public int sizeItem = 75;

    // Start is called before the first frame update
    void Start()
    {
        fullContainer.sizeDelta = new Vector2(fullContainer.sizeDelta.x, fullContainer.transform.childCount * sizeItem);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetScore();
        }
    }

    public void SetScore()
    {
        Instantiate(contentScore, fullContainer.transform);
        fullContainer.sizeDelta = new Vector2(fullContainer.sizeDelta.x, fullContainer.transform.childCount * sizeItem);
    }
}
