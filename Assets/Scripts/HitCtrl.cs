using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCtrl : MonoBehaviour
{
    public GameObject playerObj;
    public bool player2;
    public int damage;

    private void Start()
    {
        if (playerObj.CompareTag("Player2"))
        {
            player2 = true;
        }
        else if (playerObj.CompareTag("Player"))
        {
            player2 = false;
        }
        else
        {
            Debug.Log("O no has enlazado el player o faltan Tags en el player");
        }
    }
}
