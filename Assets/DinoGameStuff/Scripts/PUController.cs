using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PUController : MonoBehaviour
{
    private BoxCollider2D bC;
    public RectTransform beerSliderTr;
    private ObstacleMovement oM;

    private bool hasCollided = false;
    // Start is called before the first frame update
    void Start()
    {
        bC = this.GetComponent<BoxCollider2D>();
        oM = this.GetComponent<ObstacleMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hasCollided)
        {
            if (Vector3.Distance(this.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(beerSliderTr.position.x, beerSliderTr.position.y, 0))) > 0.01f)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, Camera.main.ScreenToWorldPoint(new Vector3(beerSliderTr.position.x, beerSliderTr.position.y, 0)), 2 * Time.deltaTime);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void CollectPU()
    {
        bC.enabled = false;
        oM.enabled = false;
        hasCollided = true;
        
       
    }
}
