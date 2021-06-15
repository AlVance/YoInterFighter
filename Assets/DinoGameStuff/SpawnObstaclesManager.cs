using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaclesManager : MonoBehaviour
{
    public DinoMainManager dMM;
    public Transform[] spawnPoints;
    public GameObject obstaclePrefab;
    public GameObject obstacleLargePrefab;
    public Vector2 timeToSpawnInterval;
    public Vector2 minTimeToSpawnInterval;
    private float timeToSpawn;
    private float currentTime;
    public float spawnTimeReducer = 0.0002f;
    // Start is called before the first frame update
    void Start()
    {
       SpawnObstacle();
       timeToSpawn = Random.Range(timeToSpawnInterval.x, timeToSpawnInterval.y);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        if(currentTime >= timeToSpawn)
        {
            SpawnObstacle();
            currentTime = 0;
            SetTimeToSpawn();
        }
        if(timeToSpawnInterval.x > minTimeToSpawnInterval.x) timeToSpawnInterval.x -= spawnTimeReducer;
        if(timeToSpawnInterval.y > minTimeToSpawnInterval.y) timeToSpawnInterval.y -= spawnTimeReducer;
    }

    private void SpawnObstacle()
    {
        int rnd = Random.Range(1, 100);
        if(rnd <= 60)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[0].position, Quaternion.Euler(0,0,0));
            int rndx = Random.Range(1, 11);
            if (rndx > 6 && dMM.velocityObstacles > 4.5f) newObstacle.transform.localScale += new Vector3(Random.Range(0.3f, 0.65f), 0, 0);
            int rndy = Random.Range(1, 11);
            if (rndy > 6 && dMM.velocityObstacles > 4f) 
            {
                newObstacle.transform.localScale += new Vector3(0, Random.Range(0.2f, 0.45f), 0);
                newObstacle.transform.localPosition += new Vector3(0, 0.15f, 0);
            } 
        }
        else if(rnd > 60 && rnd <= 90)
        {
            GameObject newObstacle = Instantiate(obstacleLargePrefab, spawnPoints[1].position, Quaternion.Euler(0, 0, 0));
            
            int rndx = Random.Range(1, 11);
            //if (rndx > 6) newObstacle.transform.localScale += new Vector3(Random.Range(0.3f, 0.65f), 0, 0);
        }
        else
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[2].position, Quaternion.Euler(0, 0, 0));
            int rndx = Random.Range(1, 11);
            if (rndx > 6) newObstacle.transform.localScale += new Vector3(Random.Range(0.3f, 0.65f), 0, 0);
        }
    }
    private void SetTimeToSpawn()
    {
        timeToSpawn = Random.Range(timeToSpawnInterval.x, timeToSpawnInterval.y);
        
        
    }

    
}
