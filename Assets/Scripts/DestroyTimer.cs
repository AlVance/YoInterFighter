using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDeath = .5f;
     
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDeath);
    }
}
