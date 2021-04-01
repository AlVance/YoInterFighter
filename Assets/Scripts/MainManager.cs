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

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void StartGame()
    {
        player1 = characters[charP1];
        player2 = characters[charP2];
        LoadScene(2);
    }

    public void LoadScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
