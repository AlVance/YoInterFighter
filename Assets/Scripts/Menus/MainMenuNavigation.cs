using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuNavigation : MonoBehaviour
{
    private int index;
    public MainManager mainManager;

    public RawImage selectScreenButton, quitButton;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Navigation();
        if (index == 0)
        {
            selectScreenButton.GetComponent<RawImage>().color = new Color32(232, 52, 235, 100);
            quitButton.GetComponent<RawImage>().color = new Color32(74, 74, 74, 100);
        }
        else if (index == 1)
        {
            quitButton.GetComponent<RawImage>().color = new Color32(232, 52, 235, 100);
            selectScreenButton.GetComponent<RawImage>().color = new Color32(74, 74, 74, 100);
        }
       
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (index == 0) mainManager.GoToSelectScreen();
            if (index == 1) Application.Quit();
        }
    }

    private void Navigation()
    {
        if(Input.GetKeyDown(KeyCode.S)|| Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(index < 1)
            {
                ++index;
            }
            else if(index >= 1)
            {
                --index;
            }
        }
        else if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index < 1)
            {
                ++index;
            }
            else if (index >= 1)
            {
                --index;
            }
        }
        
       
    }
}
