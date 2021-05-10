using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class WinnerMenuNavigation : MonoBehaviour
{
    private int index;
    private MainManager mainManager;
    public Text winnerText;

    public RawImage returnToSelectScreenButton;
    private int index1;
    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        mainManager = FindObjectOfType<MainManager>();
        if (mainManager.pj1Win)
        {
            winnerText.text = "PLAYER 1 WINS!";
        }
        else if (mainManager.pj2Win)
        {
            winnerText.text = "PLAYER 2 WINS!";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if(index1 == 0)
            {
                mainManager.GoToSelectScreen();
            }
        }

        if(index1 == 0)
        {
            returnToSelectScreenButton.GetComponent<RawImage>().color = new Color32(232, 52, 235, 100);
        }
    }

    private void Navigation()
    {

    }
}
