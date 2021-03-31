using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GifSprite : MonoBehaviour
{
    public Sprite[] gallery;
    public float timeBetween = 0.02f;
    SpriteRenderer spriteTarget;
    IEnumerator coroutine;

    private void Start()
    {
        spriteTarget = GetComponent<SpriteRenderer>();
        CicleSprite();
    }

    IEnumerator LoopImage()
    {
        for (int i = 0; i < gallery.Length; i++)
        {
            spriteTarget.sprite = gallery[i];
            yield return new WaitForSeconds(timeBetween);
        }
        CicleSprite();
    }

    public void CicleSprite()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }

        coroutine = LoopImage();
        StartCoroutine(coroutine);
    }
}
