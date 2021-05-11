using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DinoMainManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Text puntuationText;
    private float puntuation;
    private int shownPuntuation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        puntuation += Time.deltaTime;
        shownPuntuation = Mathf.RoundToInt(puntuation * 10);
        puntuationText.text = shownPuntuation.ToString();
    }
}
