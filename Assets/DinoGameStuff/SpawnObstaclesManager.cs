using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObstaclesManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject obstaclePrefab;
    public Vector2 timeToSpawnInterval;
    private float timeToSpawn;
    private float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        SetTimeToSpawn();
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
    }

    private void SpawnObstacle()
    {
        int rnd = Random.Range(1, 11);
        if(rnd <= 5)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[0].position, Quaternion.Euler(0,0,0));
            int rnd0 = Random.Range(1, 11);
            if (rnd0 > 7) newObstacle.transform.localScale = new Vector3(2, 1, 1);
        }
        else if(rnd > 5 && rnd <= 8)
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[1].position, Quaternion.Euler(0, 0, 0));
            int rnd0 = Random.Range(1, 11);
            if (rnd0 > 7) newObstacle.transform.localScale = new Vector3(2, 1, 1);
        }
        else
        {
            GameObject newObstacle = Instantiate(obstaclePrefab, spawnPoints[2].position, Quaternion.Euler(0, 0, 0));
            int rnd0 = Random.Range(1, 11);
            if (rnd0 > 7) newObstacle.transform.localScale = new Vector3(2, 1, 1);
        }
    }
    private void SetTimeToSpawn()
    {
        timeToSpawn = Random.Range(timeToSpawnInterval.x, timeToSpawnInterval.y);
    }

    
}
