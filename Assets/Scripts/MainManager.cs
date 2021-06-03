using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public int charP1;
    public int charP2;

    public GameObject[] characters;

    public GameObject player1;
    public GameObject player2;

    AnimatorManager AnimMngr;

    public bool pj1Win, pj2Win;
    private void Awake()
    {
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (AnimMngr == null)
        {
            AnimMngr = FindObjectOfType<AnimatorManager>();
        }
    }

    public void StartGame()
    {
        player1 = characters[charP1];
        player2 = characters[charP2];
        LoadScene(2);
    }

    public void GoToSelectScreen()
    {
        LoadScene(1);
        
    }

    public void GoToWinnerScreen()
    {
        LoadScene(3);
    }

    private void LoadScene(int sceneToLoad)
    {
        StartCoroutine(LoadLevel(sceneToLoad));
        AnimMngr = null;
    }

    private IEnumerator LoadLevel(int sceneToLoad)
    {
        AnimMngr.End();
        yield return new WaitForSeconds(AnimMngr.transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }

}
