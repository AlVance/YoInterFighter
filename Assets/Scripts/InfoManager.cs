using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    //TO DELETE
    [Range(0, 3)]
    public int portraitPj1Test;
    [Range(0, 3)]
    public int portraitPj2Test;
    [Range(0, 3)]
    public int HealthPj1 = 3;
    [Range(0, 3)]
    public int HealthPj2 = 3;

    public Sprite[] characterPortraits;
    [Header("Pj1Info")]
    public SpriteRenderer pj1Portrait;
    public GameObject[] pj1Lifes;
    [Header("Pj2Info")]
    public SpriteRenderer pj2Portrait;
    public GameObject[] pj2Lifes;

    MainManager mainMngr;

    // Start is called before the first frame update
    void Start()
    {
        mainMngr = FindObjectOfType<MainManager>();
        portraitPj1Test = mainMngr.charP1;
        portraitPj2Test = mainMngr.charP2;
    }

    // Update is called once per frame
    void Update()
    {
        SetPortraits(portraitPj1Test, portraitPj2Test);
        SetVisualHealthPj1(HealthPj1);
        SetVisualHealthPj2(HealthPj2);
    }

    public void SetPortraits(int characterChosenPj1, int characterChosenPj2)
    {
        switch (characterChosenPj1)
        {
            case 0:
                pj1Portrait.sprite = characterPortraits[0];
                break;
            case 1:
                pj1Portrait.sprite = characterPortraits[1];
                break;
            case 2:
                pj1Portrait.sprite = characterPortraits[2];
                break;
            case 3:
                pj1Portrait.sprite = characterPortraits[3];
                break;
        }

        switch (characterChosenPj2)
        {
            case 0:
                pj2Portrait.sprite = characterPortraits[0];
                break;
            case 1:
                pj2Portrait.sprite = characterPortraits[1];
                break;
            case 2:
                pj2Portrait.sprite = characterPortraits[2];
                break;
            case 3:
                pj2Portrait.sprite = characterPortraits[3];
                break;
        }
    }

    public void SetVisualHealthPj1(int healthPj1)
    {
        for (int i = 0; i < pj1Lifes.Length; i++)
        {
            if(i < healthPj1)
            {
                pj1Lifes[i].gameObject.SetActive(true);
            }
            else
            {
                pj1Lifes[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetVisualHealthPj2(int healthPj2)
    {
        for (int i = 0; i < pj2Lifes.Length; i++)
        {
            if (i < healthPj2)
            {
                pj2Lifes[i].gameObject.SetActive(true);
            }
            else
            {
                pj2Lifes[i].gameObject.SetActive(false);
            }
        }
    }

}
