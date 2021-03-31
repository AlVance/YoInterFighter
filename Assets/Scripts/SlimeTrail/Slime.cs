using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float speed;
    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x > 0)
        {
            transform.localScale -= Vector3.one * (Time.deltaTime / speed);
        }else if(transform.localScale.x < 0)
        {
            transform.localScale += Vector3.one * (Time.deltaTime / speed);
        }
        else if(transform.localScale.x == 0)
        {
            Destroy(gameObject);
        }
    }
}
