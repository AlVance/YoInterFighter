using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PUController : MonoBehaviour
{
    private BoxCollider2D bC;
    public RectTransform beerSliderTr;
    private ObstacleMovement oM;
    private Animator anim;

    private bool hasCollided = false;

    DinoMainManager dinoMainMngr;

    Vector3 targetPos;
    bool collided;

    Slider beerSlider;
    float currentBeers;
    GameObject lastCan;
    bool shortDist;

    // Start is called before the first frame update
    void Start()
    {
        dinoMainMngr = FindObjectOfType<DinoMainManager>();
        bC = this.GetComponent<BoxCollider2D>();
        oM = this.GetComponent<ObstacleMovement>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (collided)
        {
            Debug.Log("Distancia " + currentBeers + ": " + Vector3.Distance(this.transform.position, targetPos) + " | " + ((Vector3.Distance(this.transform.position, targetPos) < 1.5f) && (Vector3.Distance(this.transform.position, targetPos) > 0.01f)));
            if (Vector3.Distance(this.transform.position, targetPos) > 1.5f)
            {
                //Vector3 centerPos = Camera.main.ScreenToWorldPoint(new Vector3(beerSliderTr.position.x, beerSliderTr.position.y, 0));
                this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 3.5f * Time.deltaTime);
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(0, 0, 0), 5f * Time.deltaTime);
            }
            else if ((Vector3.Distance(this.transform.position, targetPos) < 1.5f) && (Vector3.Distance(this.transform.position, targetPos) > 0.01f))
            {
                if((lastCan != null)&&(!shortDist))
                {
                    beerSlider.value = lastCan.GetComponent<PUController>().currentBeers;
                    Destroy(lastCan);
                }
                shortDist = true;
                beerSlider.value = Mathf.Lerp(beerSlider.value, currentBeers, 2f * Time.deltaTime);
                this.transform.position = Vector3.Lerp(this.transform.position, targetPos, 3.5f * Time.deltaTime);
                this.transform.localScale = Vector3.Lerp(this.transform.localScale, new Vector3(0, 0, 0), 5f * Time.deltaTime);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void StartCollision(Slider _beerSlider, float _currentBeers, GameObject _lastCan)
    {
        if(_lastCan != null)
        {
            lastCan = _lastCan;
        }
        targetPos = dinoMainMngr.targetCans[dinoMainMngr.fillCans];
        if (dinoMainMngr.fillCans < dinoMainMngr.targetCans.Length)
        {
            dinoMainMngr.fillCans++;
        }
        else
        {
            dinoMainMngr.fillCans = 0;
        }
        beerSlider = _beerSlider;
        currentBeers = _currentBeers;
        collided = true;
        CollectPU();
    }

    public void CollectPU()
    {
        bC.enabled = false;
        oM.enabled = false;
        hasCollided = true;
        anim.SetBool("Collected", true);
        
       
    }
}
