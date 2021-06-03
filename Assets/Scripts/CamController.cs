using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;

    public Vector2 limitY;

    Vector3 middlePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        middlePos = Vector3.Lerp(player1.transform.position, player2.transform.position, 0.5f);
        //Debug.Log(middlePos);
        transform.position = new Vector3(middlePos.x, middlePos.y, transform.position.z);
    }
}
