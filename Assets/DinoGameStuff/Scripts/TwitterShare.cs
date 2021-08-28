using System.Collections;
using System.Collections.Generic;
using System;
using Twity.Helpers;
using UnityEngine;
using UnityEngine.Networking;
using Twity;
using Twity.DataModels.Core;
using System.IO;

public class TwitterShare : MonoBehaviour
{
    ScoreboardManager sM;
    private string puntuation;

    private string twitterAdress = "http://twitter.com/intent/tweet";
    private string tweetLanguage = "es";
    private string tweetText;
    
    private void Start()
    {
        sM = FindObjectOfType<ScoreboardManager>();
        puntuation = sM.lastScore.text;
        tweetText = "Mi puntuación en Yo,Creatureto es " + puntuation + "/n" + "#HoldMyBeer" + "[Adjunta la captura de la puntucaión que se encuentra en tu galería]";

    }
    public void ShareOnTwitter()
    {
        string fileName = "LastPunctuation.png";
        ScreenCapture.CaptureScreenshot(fileName);
       
        Application.OpenURL(twitterAdress + "?text=" + WWW.EscapeURL(tweetText) + /*"?media_data=" 
            + Convert.ToBase64String(NewBehaviourScript.CapturaFotografica()) + "?media_category="  + "tweet_image" +*/ "&amp;lang=" + WWW.EscapeURL(tweetLanguage));
        
    }

    
}
