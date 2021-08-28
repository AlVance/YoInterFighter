using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



public class TwitterShare : MonoBehaviour
{
    ScoreboardManager sM;
    private string puntuation;

    private string twitterAdress = "http://twitter.com/intent/tweet";
    private string tweetLanguage = "es";
    private string tweetText;

    string pathName;
    private void Awake()
    {

#if UNITY_ANDROID
        pathName = "DCIM/Screenshot/";
#else
        pathName = "C:/Users/" + Environment.UserName +  "/OneDrive/Imágenes" + "/YoCreatureto/";
#endif
        if (!System.IO.Directory.Exists(pathName))
        {
            System.IO.Directory.CreateDirectory(pathName);
        }
    }
    private void Start()
    {
        sM = FindObjectOfType<ScoreboardManager>();
        puntuation = sM.lastScore.text;
        

    }
    public void ShareOnTwitter()
    {

        tweetText = "¡Mi puntuación en Yo,Creatureto es de " + puntuation + " puntos!" + "\n" + "Prueba a superarme: https://kiwiteam.itch.io/yocreatureto-dinogame " + "#HoldMyBeer" + "\n" + "[Hay captura de la puntuación en tu galería :D]";
        string fileName = "YoCreatureto_LastPuntuation.png";
        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(pathName, fileName));
        Debug.Log(pathName + fileName);
        
        Application.OpenURL(twitterAdress + "?text=" + WWW.EscapeURL(tweetText) + /*"?media_data=" 
           + Convert.ToBase64String(NewBehaviourScript.CapturaFotografica()) + "?media_category="  + "tweet_image" +*/ "&amp;lang=" + WWW.EscapeURL(tweetLanguage));
        
    }

    
}
