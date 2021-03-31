using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorDetection : MonoBehaviour
{
    private GraphicRaycaster gR;
    private PointerEventData pointerED = new PointerEventData(null);

    // Start is called before the first frame update
    void Start()
    {
        gR = GetComponentInParent<GraphicRaycaster>();
    }

    // Update is called once per frame
    void Update()
    {
        pointerED.position = this.transform.position;//Camera.main.WorldToScreenPoint(transform.position);
       List<RaycastResult> results = new List<RaycastResult>();
       gR.Raycast(pointerED, results);

       if(results.Count > 0)
       {
 
           Debug.Log(results[0].gameObject.name);
       }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
