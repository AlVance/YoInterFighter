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
    string fileName;
    private void Awake()
    {

//#if UNITY_ANDROID
//        pathName = "storage/emulated/0/DCIM/Screenshots/";
//#else
//        pathName = "C:/Users/" + Environment.UserName +  "/OneDrive/Imágenes" + "/YoCreatureto/";
//#endif
//        if (!System.IO.Directory.Exists(pathName))
//        {
//            System.IO.Directory.CreateDirectory(pathName);
//        }
    }
    private void Start()
    {
        sM = FindObjectOfType<ScoreboardManager>();
        puntuation = sM.lastScore.text;
        

    }
    public void ShareOnTwitter()
    {
#if UNITY_ANDROID

        //StartCoroutine(TakeScreenshotAndSave());
        pathName = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        fileName = "/YoCreatureto_LastPuntuation.png";
#else
        pathName = "C:/Users/" + Environment.UserName +  "/OneDrive/Imágenes" + "/YoCreatureto/";
       fileName = "YoCreatureto_LastPuntuation.png";

#endif

        ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(pathName, fileName));
        tweetText = "¡Mi puntuación en Yo,Creatureto es de " + puntuation + " puntos!" + "\n" + "Prueba a superarme: https://kiwiteam.itch.io/yocreatureto-dinogame " + "#HoldMyBeer" + "\n" + "[Inserta una captura :D]";
        Debug.Log(pathName + fileName);
        Application.OpenURL(twitterAdress + "?text=" + WWW.EscapeURL(tweetText) + "&amp;lang=" + WWW.EscapeURL(tweetLanguage));
        
    }

}
