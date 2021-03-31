using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeTrail : MonoBehaviour
{
    public GameObject slimeTrailModule;
    public GameObject groundChecker;
    Platformer platformer;
    public float timeBetweenTrail;
    public float speedLifeTrail;
    float currentTime;
    
    // Start is called before the first frame update
    void Start()
    {
        platformer = GetComponent<Platformer>();
        if(platformer == null) { Debug.LogError("Este script no esta asignado en un Player"); }
    }

    // Update is called once per frame
    void Update()
    {
        if (platformer.isGrounded)
        {
            if (platformer.x != 0)
            {
                if (currentTime < timeBetweenTrail)
                {
                    currentTime += Time.deltaTime;
                }
                else
                {
                    GameObject slime = Instantiate(slimeTrailModule, groundChecker.transform);
                    slime.transform.parent = null;
                    slime.transform.localScale = Vector3.one;
                    slime.GetComponent<Slime>().speed = speedLifeTrail;
                    currentTime = 0;
                }
            }
            else
            {
                currentTime = 0;
            }
        }
        else
        {
            currentTime = 0;
        }
    }
}
