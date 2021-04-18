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
        float yValue = 0;
        if(middlePos.y > limitY.x && middlePos.y < limitY.x)
        {
            yValue = limitY.x;
        }
        else if(middlePos.y >= limitY.x)
        {
            yValue = limitY.y;
        }
        else if(middlePos.y <= limitY.x)
        {
            yValue = middlePos.y;
        }
        Debug.Log(middlePos);
        transform.position = new Vector3(middlePos.x, yValue, transform.position.z);
    }
}
