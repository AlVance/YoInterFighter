using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltiLaytonBlock : MonoBehaviour
{
    public int idBlock;
    public TextMesh textId;

    // Start is called before the first frame update
    void Start()
    {
        textId.text = idBlock.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
