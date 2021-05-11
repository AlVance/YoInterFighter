using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    private float velocity;
    public DinoMainManager dMM;
    // Start is called before the first frame update
    void Start()
    {
        dMM = FindObjectOfType<DinoMainManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        velocity = dMM.velocityObstacles;
        this.transform.position += Vector3.left * velocity * Time.deltaTime;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Obstacle")
        {
            Destroy(this.gameObject);
        }
    }
}
