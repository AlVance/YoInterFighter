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
    private string twitterAdress = "http://twitter.com/intent/tweet";
    private string tweetLanguage = "es";
    private string tweetText = "Alta puntuación me he llevado";
 
    public void ShareOnTwitter()
    {
        Application.OpenURL(twitterAdress + "?text=" + WWW.EscapeURL(tweetText) + /*"?media_data=" 
            + Convert.ToBase64String(NewBehaviourScript.CapturaFotografica()) + "?media_category="  + "tweet_image" +*/ "&amp;lang=" + WWW.EscapeURL(tweetLanguage));
        
    }

    
}
