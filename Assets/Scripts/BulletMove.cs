using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speedBullet;
    public float timeLife = 2;
    public float x;
    public Rigidbody2D rbBullet;
    float currentTime;

    private void Start()
    {
        Destroy(this.gameObject, timeLife);
    }

    // Update is called once per frame
    void Update()
    {
        if (x > 0)
        {
            transform.position += transform.right * speedBullet;
        }
        else
        {
            transform.position -= transform.right * speedBullet;
        }
        currentTime += Time.deltaTime;
        if(currentTime > (timeLife / 3) * 2)
        {
            float timeLerp = currentTime - (timeLife / 3) * 2;
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, timeLerp);
        }
    }
}
