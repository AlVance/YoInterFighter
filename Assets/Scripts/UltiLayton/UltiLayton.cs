using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiLayton : MonoBehaviour
{
    public KeyCode ultiKey;
    MainManager mainMngr;

    public GameObject block_Pref;
    public GameObject alert_Pref;

    // Start is called before the first frame update
    void Start()
    {
        mainMngr = FindObjectOfType<MainManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(ultiKey))
        {
            Ultimate();
        }
    }

    public void Ultimate()
    {
        if (gameObject.CompareTag("Player"))
        {
            UltiLaytonVictim ultiLayVic =  mainMngr.player2.AddComponent<UltiLaytonVictim>();
            ultiLayVic.block_Pref = block_Pref;
            ultiLayVic.alertDontAttack = alert_Pref;
        }
    }
}
