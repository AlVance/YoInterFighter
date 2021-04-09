using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public GameObject player1, player2;
    private HealthManager hM1, hM2;
    public GameObject mainManager;
    private MainManager sceneManager;
    private InfoManager infoManager;
    public int damageAtFall;
    private Vector2 initialPosPj1, initialPosPj2;
    // Start is called before the first frame update
    void Start()
    {
        infoManager = mainManager.GetComponent<InfoManager>();
        sceneManager = mainManager.GetComponent<MainManager>();
        
        hM1 = player1.GetComponent<HealthManager>();
        infoManager.SetVisualHealthPj1(hM1.health);
        hM2 = player2.GetComponent<HealthManager>();
        infoManager.SetVisualHealthPj2(hM2.health);
       
        initialPosPj1 = player1.transform.position;
        initialPosPj2 = player2.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(hM1.health > 0)
            {
               
                hM1.SetHealth(damageAtFall);
                infoManager.SetVisualHealthPj1(hM1.health);
                player1.transform.localPosition = initialPosPj1;
            }
            else
            {
                sceneManager.GoToWinnerScreen();
            }

        }
        else if(collision.gameObject.tag == "Player2")
        {
            if(hM2.health > 0)
            {
                hM2.SetHealth(damageAtFall);
                infoManager.SetVisualHealthPj2(hM2.health);
                player2.transform.localPosition = initialPosPj2;
            }
            else
            {
                sceneManager.GoToWinnerScreen();
            }
            
        }
    }
}
