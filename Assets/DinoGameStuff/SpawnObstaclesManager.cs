﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaclesManager : MonoBehaviour
{
    public DinoMainManager dMM;
    public Transform[] spawnPoints;
    public GameObject obstaclePrefab;
    public Vector2 timeToSpawnInterval;
    public Vector2 minTimeToSpawnInterval;
    private float timeToSpawn;
    private float currentTime;
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
        if(timeToSpawnInterval.x > minTimeToSpawnInterval.x) timeToSpawnInterval.x -= 0.0002f;
        if(timeToSpawnInterval.y > minTimeToSpawnInterval.y) timeToSpawnInterval.y -= 0.0002f;
    }

    private void SpawnObstacle()
    {
        int rnd = Random.Range(1, 11);
        if(rnd <= 6)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[0].position, Quaternion.Euler(0,0,0));
            int rndx = Random.Range(1, 11);
            if (rndx > 6) newObstacle.transform.localScale += new Vector3(Random.Range(0.4f, 0.85f), 0, 0);
            int rndy = Random.Range(1, 11);
            if (rndy > 6) 
            {
                newObstacle.transform.localScale += new Vector3(0, Random.Range(0.3f, 0.55f), 0);
                newObstacle.transform.localPosition += new Vector3(0, 0.25f, 0);
            } 
        }
        else if(rnd > 6 && rnd <= 9)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[1].position, Quaternion.Euler(0, 0, 0));
            int rndx = Random.Range(1, 11);
            if (rndx > 6) newObstacle.transform.localScale += new Vector3(Random.Range(0.4f, 0.85f), 0, 0);
        }
        else
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[2].position, Quaternion.Euler(0, 0, 0));
            int rndx = Random.Range(1, 11);
            if (rndx > 6) newObstacle.transform.localScale += new Vector3(Random.Range(0.4f, 0.85f), 0, 0);
        }
    }
    private void SetTimeToSpawn()
    {
        timeToSpawn = Random.Range(timeToSpawnInterval.x, timeToSpawnInterval.y);
        
        
    }

    
}
